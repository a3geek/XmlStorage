using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace XmlSaver.Examples {
    [AddComponentMenu("")]
    public class GetExample : MonoBehaviour {
        void Start() {
            Debug.Log(XmlSaver.GetInt("integer"));  // 10
            Debug.Log(XmlSaver.GetFloat("float"));  // 1.1
            Debug.Log(XmlSaver.GetBool("bool", true));  // true
            Debug.Log(XmlSaver.Get<SetExample.AAA>("aaa", null));  // XmlSaver.Examples.SetExample.Example+AAA
            Debug.Log(XmlSaver.Get<SetExample.AAA>("aaa", null).list.First()); // A

            Debug.Log("");

            Debug.Log(XmlSaver.GetString("del_tes1"));  // ""
            Debug.Log(XmlSaver.GetInt("del_tes1"));     // 0
            
            Debug.Log("");

            Debug.Log(XmlSaver.GetString("del_tes2"));  // del_tes2
            Debug.Log(XmlSaver.GetInt("del_tes2"));     // 0

            Debug.Log("");

            Debug.Log(XmlSaver.Get("vec2", Vector2.zero));
        }
    }
}
