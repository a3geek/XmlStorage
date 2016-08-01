using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace XmlSaver.Examples {
    [AddComponentMenu("")]
    public class SetExample : MonoBehaviour {
        public class AAA {
            public string str = "string-AAA";
            public List<string> list = new List<string>() {
                "A", "B", "C"
            };
        }
        public Vector2 Vec2 = new Vector2(1.5f, 2.5f);

        
        void Start() {
            XmlSaver.SetInt("integer", 10);
            XmlSaver.SetFloat("float", 1.1f);
            XmlSaver.Set("aaa", new AAA());


            XmlSaver.SetInt("del_tes1", 2);
            XmlSaver.SetString("del_tes1", "del_tes1");
            XmlSaver.DeleteKey("del_tes1");

            XmlSaver.SetInt("del_tes2", 5);
            XmlSaver.SetString("del_tes2", "del_tes2");
            XmlSaver.DeleteKey("del_tes2", typeof(int));

            XmlSaver.Set("vec2", this.Vec2);

            XmlSaver.Save();
        }
    }
}
