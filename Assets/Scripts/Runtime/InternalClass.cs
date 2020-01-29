using UnityEngine;

namespace MyNamespace.A
{
    class InternalClass : MonoBehaviour
    {
        public void PublicMethod()
        {
            Debug.Log("InternalClass.PublicMethod()");            
        }

        public void PublicIntMethod(int i)
        {
            Debug.Log($"InternalClass.PublicMethod({i})");
        }

        void PrivateMethod()
        {
            Debug.Log("InternalClass.PrivateMethod()");
        }

        void PrivateMethod(int i)
        {
            Debug.Log($"InternalClass.PrivateMethod({i})");
        }
        
        internal void InternalMethod()
        {
            Debug.Log("InternalClass.InternalMethod()");
        }

        internal void InternalMethod(int i)
        {
            Debug.Log($"InternalClass.InternalMethod({i})");
        }
        
        protected void ProtectedMethod()
        {
            Debug.Log("InternalClass.ProtectedMethod()");
        }

        protected void ProtectedMethod(int i)
        {
            Debug.Log($"InternalClass.ProtectedMethod({i})");
        }
    }
}