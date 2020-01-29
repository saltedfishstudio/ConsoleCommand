using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class ReflectionRunner {
#if UNITY_EDITOR
	[UnityEditor.MenuItem("Tests/Run")]
#endif
	public static void Run()
	{
		var assembly = Assembly.GetExecutingAssembly();
		var types = assembly
			.GetTypes();
		
		var list = types
			.Where(t => t.Namespace == "MyNamespace.A")
			.ToList();

		foreach (Type type in list)
		{
			MethodInfo[] methodInfos = type.GetMethods();
			List<MethodInfo> methods = methodInfos.Where(m => m.DeclaringType == type).ToList();
			
			object classInstance = Activator.CreateInstance(type, null);

			foreach (MethodInfo method in methods)
			{
				ParameterInfo[] parameters = method.GetParameters();
				if (parameters.Length == 0)
				{
					method.Invoke(classInstance, null);
				}
				else
				{
					object[] parameterArray = new object[] {3};
					method.Invoke(classInstance, parameterArray);
				}
			}
		}
	}
	
#if UNITY_EDITOR
	[UnityEditor.MenuItem("Tests/Test")]
#endif
	static void Func()
	{
		var list = Assembly.GetExecutingAssembly()
			.GetTypes()
			.Where(t => t.Namespace == "MyNamespace.A")
			.ToList();

		foreach (Type type in list)
		{
			MethodInfo[] methodInfos = type.GetMethods();
			foreach (MethodInfo method in methodInfos)
			{
				if (method.Name == "Initialize")
				{
					object classInstance = Activator.CreateInstance(type, null);

					ParameterInfo[] parameters = method.GetParameters();
					if (parameters.Length == 0)
					{
						// method.Invoke(classInstance, null);
					}
					else
					{
						object[] parameterArray = new object[] {"New Player", 3};
						method.Invoke(classInstance, parameterArray);
					}
				}
				else
				{
					Debug.LogError(method.Name);
				}
			}
		}
	}
}