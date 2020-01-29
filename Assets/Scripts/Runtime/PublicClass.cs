using UnityEngine;

namespace MyNamespace.A
{
	public class PublicClass : MonoBehaviour
	{
		public void PublicMethod()
		{
			Debug.Log("PublicClass.PublicMethod()");            
		}

		public void PublicIntMethod(int i)
		{
			Debug.Log($"PublicClass.PublicMethod({i})");
		}

		void PrivateMethod()
		{
			Debug.Log("PublicClass.PrivateMethod()");
		}

		void PrivateMethod(int i)
		{
			Debug.Log($"PublicClass.PrivateMethod({i})");
		}
        
		internal void InternalMethod()
		{
			Debug.Log("PublicClass.InternalMethod()");
		}

		internal void InternalMethod(int i)
		{
			Debug.Log($"PublicClass.InternalMethod({i})");
		}
        
		protected void ProtectedMethod()
		{
			Debug.Log("PublicClass.ProtectedMethod()");
		}

		protected void ProtectedMethod(int i)
		{
			Debug.Log($"PublicClass.ProtectedMethod({i})");
		}
	}
}