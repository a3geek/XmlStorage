using UnityEngine;

namespace XmlStorage.Examples
{
    /// <summary>
    /// 値のセットと保存のテスト
    /// </summary>
    [AddComponentMenu("")]
    public class SetExample : MonoBehaviour
    {
        /// <summary>インスタンスの保存テスト</summary>
        private ExampleController.Test test = new ExampleController.Test();
        /// <summary><see cref="Vector2"/>の保存テスト</summary>
        private Vector2 vec2 = new Vector2(0.1f, 0.2f);
        /// <summary><see cref="Vector3"/>の保存テスト</summary>
        private Vector3 vec3 = new Vector3(1f, 2f, 3f);
        /// <summary><see cref="Quaternion"/>の保存テスト</summary>
        private Quaternion quaternion = new Quaternion();


        /// <summary>
        /// 値をセットして保存する
        /// </summary>
        public void Execute()
        {
            this.quaternion.eulerAngles = new Vector3(10f, 20f, 30f);

            this.SetData2XmlStorage(1);

            XmlStorage.ChangeAggregation("Test1");
            this.SetData2XmlStorage(11);

            XmlStorage.ChangeAggregation("Test2");
            XmlStorage.FileName = "Test2";
            this.SetData2XmlStorage(111);

            XmlStorage.Save();
            Debug.Log("Finish");
        }

        /// <summary>
        /// <see cref="XmlStorage"/>に値をセットする
        /// </summary>
        /// <param name="value"><see cref="int"/>型の保存テスト</param>
        private void SetData2XmlStorage(int value)
        {
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
            XmlStorage.Set("qua", this.quaternion);
        }
    }
}
