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
		[SerializeField] string m_namespace = "UnityEngine.Console";
		List<MethodInfo> methods = new List<MethodInfo>();

		[SerializeField] InputField inputField = default;
		
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
				
				methods.AddRange(methodInfos);
			}
			
			Debug.Log($"found method count: {methods.Count}");
			inputField.onValueChanged.AddListener(OnValueChanged);
		}

		void OnValueChanged(string input)
		{
			var filtered = methods.Where(e => e.Name.ToLower().Contains(input.ToLower()));
			foreach (MethodInfo info in filtered)
			{
				Debug.Log(info.Name);
			}
		}
	}
}