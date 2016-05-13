using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace XmlStorage {
    [AddComponentMenu("")]
    public class Example : MonoBehaviour {
        public class AAA {
            public string str = "string-AAA";
            public List<string> list = new List<string>() {
                "A", "B", "C"
            };
        }
        
        void Start() {
            XmlStorage.SetInt("integer", 10);
            XmlStorage.SetFloat("float", 1.1f);
            XmlStorage.Set<AAA>("aaa", new AAA());

            XmlStorage.SetInt("test", 5);
            XmlStorage.SetString("test", "test");

            XmlStorage.Save();

            Debug.Log(XmlStorage.GetInt("integer"));  // 10
            Debug.Log(XmlStorage.GetFloat("float"));  // 1.1
            Debug.Log(XmlStorage.GetBool("bool", true));  // true
            Debug.Log(XmlStorage.Get<AAA>("aaa", null));  // XmlSaver.Example+AAA
            Debug.Log(XmlStorage.Get<AAA>("aaa", null).list.First()); // A

            Debug.Log("");

            Debug.Log(XmlStorage.GetString("test"));  // test
            Debug.Log(XmlStorage.GetInt("test"));     // 5

            XmlStorage.DeleteKey("test");

            Debug.Log(XmlStorage.GetString("test"));  // ""
            Debug.Log(XmlStorage.GetInt("test"));     // 0

            Debug.Log("");

            XmlStorage.SetInt("test", 5);
            XmlStorage.SetString("test", "test");
            XmlStorage.DeleteKey("test", typeof(int));

            Debug.Log(XmlStorage.GetString("test"));  // test
            Debug.Log(XmlStorage.GetInt("test"));     // 0
        }
    }
}
