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

            XmlSaver.Save();

            Debug.Log(XmlSaver.GetInt("integer"));
            Debug.Log(XmlSaver.GetFloat("float"));
            Debug.Log(XmlSaver.GetBool("bool", false));
            Debug.Log(XmlSaver.Get<AAA>("aaa", null).list.First());
        }
    }
}
