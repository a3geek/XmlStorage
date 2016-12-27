using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace XmlStorage.Examples {
    [AddComponentMenu("")]
    public class ExampleController : MonoBehaviour {
        public class Test {
            public string str = "TestString";
            public List<float> list1 = new List<float>() {
                0.1f, 1.1f, 10.1f
            };
        }

        public enum ExampleMode {
            Set = 0, Get
        };

        public ExampleMode mode = ExampleMode.Set;
        public SetExample set = null;
        public GetExample get = null;


        void Awake() {
            if(this.set == null) { this.set = GetComponentInChildren<SetExample>(); }
            if(this.get == null) { this.get = GetComponentInChildren<GetExample>(); }
        }

        void Start() {
            if(this.mode == ExampleMode.Set) { this.set.Execute(); }
            else { this.get.Execute(); }
        }
    }
}
