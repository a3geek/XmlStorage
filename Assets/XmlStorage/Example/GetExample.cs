using UnityEngine;
using System.Linq;

namespace XmlStorage.Examples
{
    /// <summary>
    /// 値のゲットのテスト
    /// </summary>
    [AddComponentMenu("")]
    public class GetExample : MonoBehaviour
    {
        /// <summary>
        /// 値をゲットしてログに出力する
        /// </summary>
        public void Execute()
        {
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
            Debug.Log("Default Aggregatpion");
            Debug.Log("Directory Path : " + XmlStorage.DirectoryPath);
            Debug.Log("File Name : " + XmlStorage.FileName);
            this.GetDataFromXmlStorage();

            if(Application.isEditor)
            {
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
                Debug.Log("Test1 Aggregation");
                XmlStorage.ChangeAggregation("Test1");
                Debug.Log("Directory Path : " + XmlStorage.DirectoryPath);
                Debug.Log("File Name : " + XmlStorage.FileName);
                this.GetDataFromXmlStorage();
            }

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
            Debug.Log("Test2 Aggregation");
            XmlStorage.ChangeAggregation("Test2");
            Debug.Log("Directory Path : " + XmlStorage.DirectoryPath);
            Debug.Log("File Name : " + XmlStorage.FileName);
            this.GetDataFromXmlStorage();
        }

        /// <summary>
        /// <see cref="XmlStorage"/>から値を取得してログに出力する
        /// </summary>
        private void GetDataFromXmlStorage()
        {
            Debug.Log(XmlStorage.GetInt("integer", 0));
            Debug.Log(XmlStorage.GetFloat("float", 0f));
            Debug.Log(XmlStorage.Get<ExampleController.Test>("TestClass", null));
            Debug.Log(XmlStorage.Get<ExampleController.Test>("TestClass", null).str);
            Debug.Log(XmlStorage.Get<ExampleController.Test>("TestClass", null).list1.First());
            Debug.Log(XmlStorage.Get<ExampleController.Test>("TestClass", null).list1.Last());

            Debug.Log("");

            Debug.Log(XmlStorage.GetInt("del_tes1"));
            Debug.Log(XmlStorage.GetString("del_tes1"));

            Debug.Log("");

            Debug.Log(XmlStorage.GetInt("del_tes2"));
            Debug.Log(XmlStorage.GetString("del_tes2"));

            Debug.Log("");

            Debug.Log(XmlStorage.Get<string>("address"));

            Debug.Log("");

            Debug.Log(XmlStorage.Get("vec2", Vector2.zero));
            Debug.Log(XmlStorage.Get("vec3", Vector3.zero));
            Debug.Log(XmlStorage.Get("qua", Quaternion.identity).eulerAngles);

            Debug.Log("");
            Debug.Log("");
        }
    }
}
