using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace XmlSaver {
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

            Debug.Log(XmlSaver.GetInt("integer"));
            Debug.Log(XmlSaver.GetFloat("float"));
            Debug.Log(XmlSaver.GetBool("bool", true));
            Debug.Log(XmlSaver.Get<AAA>("aaa", null));
            Debug.Log(XmlSaver.Get<AAA>("aaa", null).list.First());

            Debug.Log("");

            Debug.Log(XmlSaver.GetString("test"));
            Debug.Log(XmlSaver.GetInt("test"));

            XmlSaver.DeleteKey("test");

            Debug.Log(XmlSaver.GetString("test"));
            Debug.Log(XmlSaver.GetInt("test"));

            Debug.Log("");

            XmlSaver.SetInt("test", 5);
            XmlSaver.SetString("test", "test");
            XmlSaver.DeleteKey("test", typeof(int));

            Debug.Log(XmlSaver.GetString("test"));
            Debug.Log(XmlSaver.GetInt("test"));
        }
    }
}
