using System;
using System.Collections.Generic;
using UnityEngine;

namespace XmlStorage.Tests
{
    [Serializable]
    public class SerializableTestModel
    {
        public int v1 = 0;
        public float[] v2 = { 1.1f, -2.2f, 3.3f, -4.4f };
        public Mode mode = Mode.One;
        
        public enum Mode
        {
            Zero = 0,
            One = 1
        };
    }

    [Serializable]
    public class NonSerializableTestModel
    {
        public List<float> v1 = new() { -1f, -2f, -3f };
        public float V2 => this.v2;

        [SerializeField]
        private float v2 = 12.345f;
    }

    [Serializable]
    public struct SerializableTestStruct
    {
        public bool v1;
        public string v2;
        public SerializableTestModel test;
        
        public SerializableTestStruct(bool v1, string v2, SerializableTestModel test)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.test = test;
        }
    }
}
