using System;
using System.IO;
using UnityEngine;

namespace XmlStorage.Example
{
    public class SetAndGet : MonoBehaviour
    {
        [SerializeField]
        private Test test1 = new(int.MaxValue, float.MaxValue, new[] { Vector3.one, Vector3.zero });
        [SerializeField]
        private Test test2 = new(int.MinValue, float.MinValue, new[] { Vector3.right, Vector3.left });
        [SerializeField]
        private Test test3 = new(0, float.Epsilon, new[] { Vector3.forward, Vector3.back, Vector3.up, Vector3.down });


        private void Awake()
        {
            Storage.DirectoryPath = Path.Combine(Storage.GetDefaultDirectoryPath(), "Prefs");
            Storage.CurrentDataGroupName = "Prefs";

            Get();
            this.Set();
            Get();
        }

        private void Set()
        {
            Storage.Set("int", 10);
            Storage.Set("float", 1.2345f);
            Storage.Set("bool", true);
            Storage.Set("string", "Test");
            Storage.Set("test1", this.test1);
            Storage.Set("test2", this.test2);
            Storage.Set("test3", this.test3);
            Storage.Save();
        }

        private static void Get()
        {
            Storage.Load();

            Debug.Log("Expect: 10, Result: " + Storage.Get<int>("int"));
            Debug.Log("Expect: 1.2345f, Result: " + Storage.Get<float>("float"));
            Debug.Log("Expect: true, Result: " + Storage.Get<bool>("bool"));
            Debug.Log("Expect: Test, Result: " + Storage.Get<string>("string"));
            Debug.Log(Storage.Get<Test>("test1"));
            Debug.Log(Storage.Get<Test>("test2"));
            Debug.Log(Storage.Get<Test>("test3"));
            Debug.Log("");
        }


        [Serializable]
        public class Test
        {
            public int v1;
            public float v2;
            public Vector3[] v3;


            public Test() { }

            public Test(int v1, float v2, Vector3[] v3)
            {
                this.v1 = v1;
                this.v2 = v2;
                this.v3 = v3;
            }

            public override string ToString()
            {
                var v3 = "(";
                foreach (var e in this.v3)
                {
                    v3 += e + ", ";
                }
                v3 += ")";

                return $"v1 = {this.v1}, v2 = {this.v2}, v3 = {v3}";
            }
        }
    }
}
