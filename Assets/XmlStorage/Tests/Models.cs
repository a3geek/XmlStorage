using System;
using System.Collections.Generic;
using UnityEngine;

namespace XmlStorage.Tests
{
    public enum Mode
    {
        Zero = 0,
        One = 1,
        Two = 2
    };

    [Serializable]
    public class TestModel
    {
        public int Int1;
        public float[] Floats;
        public List<string> Strings;
        public Mode Mode;
        public int Int2 => this.int2;

        [SerializeField]
        private int int2;


        public TestModel() { }

        public TestModel(int int1, float[] floats, List<string> strings, Mode mode, int int2)
        {
            this.Int1 = int1;
            this.Floats = floats;
            this.Strings = strings;
            this.Mode = mode;
            this.int2 = int2;
        }
    }

    [Serializable]
    public class NestedTestModel
    {
        public bool Bool;
        public string Str;
        public TestModel Test;
    }
}
