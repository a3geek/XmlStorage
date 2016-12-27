using UnityEngine;

namespace XmlStorage.Examples {
    [AddComponentMenu("")]
    public class SetExample : MonoBehaviour {
        private ExampleController.Test test = new ExampleController.Test();
        private Vector2 vec2 = new Vector2(0.1f, 0.2f);
        private Vector3 vec3 = new Vector3(1f, 2f, 3f);
        private Quaternion qua = new Quaternion();


        public void Execute() {
            this.qua.eulerAngles = new Vector3(10f, 20f, 30f);

            this.SetData2XmlStorage(1);

            XmlStorage.ChangeAggregation("Test1");
            this.SetData2XmlStorage(11);

            XmlStorage.ChangeAggregation("Test2");
            XmlStorage.FileName = "Test2";
            this.SetData2XmlStorage(111);

            XmlStorage.Save();
            Debug.Log("Finish");
        }

        private void SetData2XmlStorage(int value) {
            XmlStorage.SetInt("integer", value);
            XmlStorage.SetFloat("float", 1.111f);
            XmlStorage.Set("TestClass", this.test);

            XmlStorage.SetInt("del_tes1", 2);
            XmlStorage.SetString("del_tes1", "del_tes1");
            XmlStorage.DeleteKey("del_tes1");

            XmlStorage.SetInt("del_tes2", 5);
            XmlStorage.SetString("del_tes2", "del_tes2");
            XmlStorage.DeleteKey("del_tes2", typeof(int));

            var address = "lab-interactive@team-lab.com";
            XmlStorage.SetString("address", address);

            XmlStorage.Set("vec2", this.vec2);
            XmlStorage.Set("vec3", this.vec3);
            XmlStorage.Set("qua", this.qua);
        }
    }
}
