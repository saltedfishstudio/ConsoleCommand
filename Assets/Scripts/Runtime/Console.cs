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
				}
			}
			
			Debug.Log($"found method count: {methods.Count}");
			inputField.onValueChanged.AddListener(OnValueChanged);
			cellPrefab.gameObject.SetActive(false);
		}

		void OnValueChanged(string input)
		{
			MethodInfo[] filtered;

			if (string.IsNullOrEmpty(input))
			{
				filtered = new MethodInfo[0];
			}
			else
			{
				filtered = methods.Values
					.Where(e => e.Name.ToLower().Contains(input.ToLower()))
					.OrderBy(e => e.Name).ToArray();
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
				cell.text = method.Name;
				cell.gameObject.SetActive(true);
			}
		}

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
	}
}