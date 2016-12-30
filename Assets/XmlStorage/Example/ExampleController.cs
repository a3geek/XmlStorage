using UnityEngine;
using System.Collections.Generic;

namespace XmlStorage.Examples
{
    /// <summary>
    /// サンプル用コントローラー
    /// </summary>
    [AddComponentMenu("")]
    public class ExampleController : MonoBehaviour
    {
        /// <summary>
        /// クラスのインスタンス保存テスト用
        /// </summary>
        public class Test
        {
            /// <summary><see cref="int"/>型の保存テスト</summary>
            public int integer = 1;
            /// <summary>文字列保存テスト</summary>
            public string str = "TestString";
            /// <summary>リスト保存テスト</summary>
            public List<float> list1 = new List<float>() {
                0.1f, 1.1f, 10.1f
            };
        }

        /// <summary>
        /// 動作設定用enum
        /// </summary>
        public enum ExampleMode
        {
            Set = 0, Get
        };

        /// <summary>
        /// 値をセットするかゲットするかの動作を選択
        /// </summary>
        public ExampleMode mode = ExampleMode.Set;
        /// <summary>データセット用コンポーネント</summary>
        public SetExample set = null;
        /// <summary>データゲット用コンポーネント</summary>
        public GetExample get = null;


        void Awake()
        {
            if(this.set == null)
            {
                this.set = GetComponentInChildren<SetExample>();
            }
            if(this.get == null)
            {
                this.get = GetComponentInChildren<GetExample>();
            }
        }

        void Start()
        {
            if(this.mode == ExampleMode.Set)
            {
                this.set.Execute();
            }
            else
            {
                this.get.Execute();
            }
        }
    }
}
