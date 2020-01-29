using UnityEngine;

namespace MyNamespace.A
{
	public class PlayerController
	{
		public void Initialize(string tag, int index)
		{
			Debug.Log($"Tag : {tag}, Index : {index}");
		}
	}
}