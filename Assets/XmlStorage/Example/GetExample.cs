using UnityEngine;
using System.Linq;

namespace XmlStorage.Examples {
    [AddComponentMenu("")]
    public class GetExample : MonoBehaviour {
        public void Execute() {
            /*
             * 1
             * 1.111
             * XmlStorage.Examples.ExampleController+Test
             * "TestString"
             * 0.1f
             * 10.1f
             * 
             * 0
             * ""
             * 
             * 0
             * "del_tes2"
             * 
             * "lab-interactive@team-lab.com"
             * 
             * (0.1, 0.2)
             * (1.0, 2.0, 3.0)
             * (10.0, 20.0, 30.0)
             */
            this.Log("Default Aggregatpion");
            this.GetDataFromXmlStorage();

            /*
             * 11
             * 1.111
             * XmlStorage.Examples.ExampleController+Test
             * "TestString"
             * 0.1f
             * 10.1f
             * 
             * 0
             * ""
             * 
             * 0
             * "del_tes2"
             * 
             * "lab-interactive@team-lab.com"
             * 
             * (0.1, 0.2)
             * (1.0, 2.0, 3.0)
             * (10.0, 20.0, 30.0)
             */
            this.Log("Test1 Aggregation");
            XmlStorage.ChangeAggregation("Test1");
            this.GetDataFromXmlStorage();

            /*
             * 111
             * 1.111
             * XmlStorage.Examples.ExampleController+Test
             * "TestString"
             * 0.1f
             * 10.1f
             * 
             * 0
             * ""
             * 
             * 0
             * "del_tes2"
             * 
             * "lab-interactive@team-lab.com"
             * 
             * (0.1, 0.2)
             * (1.0, 2.0, 3.0)
             * (10.0, 20.0, 30.0)
             */
            this.Log("Test2 Aggregation");
            XmlStorage.ChangeAggregation("Test2");
            this.GetDataFromXmlStorage();
        }

        private void GetDataFromXmlStorage() {
            this.Log(XmlStorage.GetInt("integer", 0));
            this.Log(XmlStorage.GetFloat("float", 0f));
            this.Log(XmlStorage.Get<ExampleController.Test>("TestClass", null));
            this.Log(XmlStorage.Get<ExampleController.Test>("TestClass", null).str);
            this.Log(XmlStorage.Get<ExampleController.Test>("TestClass", null).list1.First());
            this.Log(XmlStorage.Get<ExampleController.Test>("TestClass", null).list1.Last());

            this.Log("");

            this.Log(XmlStorage.GetInt("del_tes1"));
            this.Log(XmlStorage.GetString("del_tes1"));

            this.Log("");

            this.Log(XmlStorage.GetInt("del_tes2"));
            this.Log(XmlStorage.GetString("del_tes2"));

            this.Log("");

            this.Log(XmlStorage.Get<string>("address"));

            this.Log("");

            this.Log(XmlStorage.Get("vec2", Vector2.zero));
            this.Log(XmlStorage.Get("vec3", Vector3.zero));
            this.Log(XmlStorage.Get("qua", Quaternion.identity).eulerAngles);

            this.Log("");
            this.Log("");
        }

        private void Log(object obj) { Debug.Log(obj); }
    }
}
