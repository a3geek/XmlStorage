using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using UnityEngine;

#pragma warning disable 0414

namespace XmlStorage.Examples
{
    [Serializable]
    public class ExampleController : MonoBehaviour
    {
        ///// <summary>
        ///// クラスのインスタンス保存テスト用
        ///// </summary>
        //[Serializable]
        //public class Test1
        //{
        //    /// <summary><see cref="int"/>型の保存テスト</summary>
        //    public int integer = 1;
        //    /// <summary>文字列保存テスト</summary>
        //    public string str = "TestString";
        //    /// <summary>リスト保存テスト</summary>
        //    public List<float> list1 = new List<float>() {
        //        0.1f, 1.1f, 10.1f
        //    };

        //    /// <summary>XmlIgnoreのテスト</summary>
        //    [XmlIgnore]
        //    public float ff = 1f;


        //    public void Log()
        //    {
        //        var s = " _ ";
        //        Debug.Log(this.integer + s + this.str + s + this.list1[0] + s + this.list1[1] + s + this.list1[2] + s + this.ff);
        //    }
        //}

        ///// <summary>
        ///// クラスのインスタンス保存テスト用
        ///// </summary>
        //public class Test2
        //{
        //    /// <summary><see cref="Vector2"/>型の保存テスト</summary>
        //    public Vector2 vec2 = new Vector2(1f, 2f);
        //    /// <summary><see cref="Vector3"/>型の保存テスト</summary>
        //    public Vector3 vec3 = new Vector3(1f, 2f, 3f);

        //    /// <summary>XmlIgnoreのテスト</summary>
        //    [XmlIgnore]
        //    public Vector4 vec4 = new Vector4(1f, 2f, 3f, 4f);


        //    public void Log()
        //    {
        //        Debug.Log(this.vec2);
        //        Debug.Log(this.vec3);
        //        Debug.Log(this.vec4);
        //    }
        //}

        ///// <summary>
        ///// 動作設定用enum
        ///// </summary>
        //public enum ExampleMode
        //{
        //    Set = 1, Get
        //};

        //public static string SecondaryFolder = "";

        ///// <summary>
        ///// 値をセットするかゲットするかの動作を選択
        ///// </summary>
        //public ExampleMode mode = ExampleMode.Set;
        ///// <summary>データセット用コンポーネント</summary>
        //public SetExample set = null;
        ///// <summary>データゲット用コンポーネント</summary>
        //public GetExample get = null;

        [Serializable]
        public class Test
        {
            public int v1 = 0;
            public float v2 = 0f;
            public Vector3[] v3 = Array.Empty<Vector3>();

            [SerializeField]
            private int v4 = 0;
        }

        [SerializeField]
        private Test test = new();
        [SerializeField]
        private Test test2 = new();
        [SerializeField]
        private Test test3 = new();
        

        void Awake()
        {
            //Storage.Test();
            //Storage.Save();
            
            Storage.Set(nameof(Test), this.test);
            var test = Storage.Get<Test>(nameof(Test));
            this.test2 = test;
            Storage.Save();

            Storage.Load();
            this.test3 = Storage.Get<Test>(nameof(Test));

            //    SecondaryFolder =
            //        Directory.GetCurrentDirectory() + XmlStorageConsts.Separator + "Saves2" + XmlStorageConsts.Separator;

            //    if(this.set == null)
            //    {
            //        this.set = this.GetComponentInChildren<SetExample>();
            //    }
            //    if(this.get == null)
            //    {
            //        this.get = this.GetComponentInChildren<GetExample>();
            //    }
        }

        //void Start()
        //{
        //    if(this.mode == ExampleMode.Set)
        //    {
        //        this.set.Execute();
        //    }
        //    else
        //    {
        //        this.get.Execute();
        //    }
        //}
    }
}

#pragma warning restore 0414
