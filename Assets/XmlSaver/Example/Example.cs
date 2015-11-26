using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace XmlSaver {
    [AddComponentMenu("")]
    public class Example : MonoBehaviour {
        public class AAA {
            public string str = "string-AAA";
            public List<string> list = new List<string>() {
                "A", "B", "C"
            };
        }

        
        void Start() {
            XmlSaver.SetInt("integer", 10);
            XmlSaver.SetFloat("float", 1.1f);
            XmlSaver.Set<AAA>("aaa", new AAA());

            XmlSaver.SetInt("test", 5);
            XmlSaver.SetString("test", "test");

            XmlSaver.Save();

            Debug.Log(XmlSaver.GetInt("integer"));  // 10
            Debug.Log(XmlSaver.GetFloat("float"));  // 1.1
            Debug.Log(XmlSaver.GetBool("bool", true));  // true
            Debug.Log(XmlSaver.Get<AAA>("aaa", null));  // XmlSaver.Example+AAA
            Debug.Log(XmlSaver.Get<AAA>("aaa", null).list.First()); // A

            Debug.Log("");

            Debug.Log(XmlSaver.GetString("test"));  // test
            Debug.Log(XmlSaver.GetInt("test"));     // 5

            XmlSaver.DeleteKey("test");

            Debug.Log(XmlSaver.GetString("test"));  // ""
            Debug.Log(XmlSaver.GetInt("test"));     // 0

            Debug.Log("");

            XmlSaver.SetInt("test", 5);
            XmlSaver.SetString("test", "test");
            XmlSaver.DeleteKey("test", typeof(int));

            Debug.Log(XmlSaver.GetString("test"));  // test
            Debug.Log(XmlSaver.GetInt("test"));     // 0
        }
    }
}
