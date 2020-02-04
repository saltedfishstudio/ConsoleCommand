using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace SFStudio.ScriptConsole
{
	public class Console : MonoBehaviour
	{
		[SerializeField] string m_namespace = "Console.Command";
		[SerializeField] InputField inputField = default;
		[SerializeField] Text cellPrefab = default;
		[SerializeField] RectTransform root = default;

		readonly List<Text> cells = new List<Text>();
		readonly Dictionary<string, MethodInfo> methods = new Dictionary<string, MethodInfo>();
		readonly Dictionary<string, Type> methodOwnerMap = new Dictionary<string, Type>();

		MethodInfo selected = default;
		
		void Awake()
		{
			List<Type> list = Assembly.GetExecutingAssembly()
				.GetTypes()
				.Where(t => t.Namespace == m_namespace)
				.ToList();

			foreach (Type type in list)
			{
				var methodInfos = type.GetMethods()
					.Where(m => m.DeclaringType == type);

				foreach (MethodInfo methodInfo in methodInfos)
				{
					methods[methodInfo.Name] = methodInfo;
					methodOwnerMap[methodInfo.Name] = type;
				}
			}
			
			inputField.onValueChanged.AddListener(OnValueChanged);
			inputField.onEndEdit.AddListener(OnEndEdit);
			cellPrefab.gameObject.SetActive(false);
		}

		void Update()
		{
			if (inputField.isFocused)
			{
				if (Input.GetKeyDown(KeyCode.Tab))
				{
					if (selected != null)
					{
						inputField.text = selected.Name;
						inputField.caretPosition = inputField.text.Length;
						inputField.Select();
						inputField.ActivateInputField();
					}
				}
			}
		}

		void OnValueChanged(string input)
		{
			MethodInfo[] filtered;
			string methodName = input.Split(' ').FirstOrDefault();

			if (string.IsNullOrEmpty(methodName))
			{
				filtered = new MethodInfo[0];
			}
			else
			{
				filtered = methods.Values
					.Where(e => e.Name.ToLower().Contains(methodName.ToLower()))
					.OrderBy(e => e, new MethodComparer(methodName)).ToArray();
			}

			int count = filtered.Length;
			for (int i = count; i < cells.Count; i++)
			{
				var cell = GetCell(i);
				cell.gameObject.SetActive(false);
			}

			for (int i = 0; i < count; i++)
			{
				var cell = GetCell(i);
				var method = filtered[i];

				string displayText = $"{method.Name}(";
				var parameters = method.GetParameters();
				for (int index = 0; index < parameters.Length; index++)
				{
					var parameter = parameters[index];
					displayText += $"{parameter.ParameterType.Name} {parameter.Name}";
					if (index != parameters.Length - 1)
					{
						displayText += ", ";
					}
				}

				displayText += ")";

				cell.text = displayText;
				cell.gameObject.SetActive(true);
			}

			selected = filtered.FirstOrDefault();
		}

		void OnEndEdit(string input)
		{
			if (selected == null)
				return;
			
			object classInstance = Activator.CreateInstance(methodOwnerMap[selected.Name], null);

			string[] blocks = input.Split(' ');
			int inputParameterCount = blocks.Length - 1;
			ParameterInfo[] parameters = selected.GetParameters();

			// compatible
			if (inputParameterCount == parameters.Length)
			{
				try
				{
					object[] convertedParameters = new object[inputParameterCount];
					for (int i = 0; i < inputParameterCount; i++)
					{
						var parameter = parameters[i];
						var inputValue = blocks[i + 1];

						// convert to types
						var converted = Convert.ChangeType(inputValue, parameter.ParameterType);
						convertedParameters[i] = converted;
					}

					selected.Invoke(classInstance, convertedParameters);
				}
				catch (Exception e)
				{
					Debug.LogError(e);
				}
			}
			else
			{
				Debug.LogError("The number of parameter is not compatible.");
			}

			inputField.text = null;
			inputField.Select();
			inputField.ActivateInputField();
		}

		[NonSerialized] public int a;

		Text GetCell(int index)
		{
			Text instance;
			if (cells.Count <= index)
			{
				instance = Instantiate(cellPrefab, root);
				cells.Add(instance);
			}
			else
			{
				instance = cells[index];
			}

			return instance;
		}
		
		// @todo function
		// select previous method by keyboard arrow
	}
}