﻿using System;
using System.Collections.Generic;

namespace XmlStorage.Components.Aggregations.Accessors
{
    public abstract class Setters
    {
        /// <summary>
        /// 任意の型のデータとキーをセットする
        /// </summary>
        /// <typeparam name="T">セットするデータの型(Serializable)</typeparam>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public void Set<T>(string key, T value)
        {
            this.SetValue(key, value, typeof(T));
        }

        /// <summary>
        /// 任意の型のデータとキーをセットする
        /// </summary>
        /// <typeparam name="T">セットするデータの型(Serializable)</typeparam>
        /// <param name="type">セットするデータの型情報</param>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public void Set<T>(Type type, string key, T value)
        {
            this.SetValue(key, value, type);
        }

        /// <summary>
        /// float型のデータとキーをセットする
        /// </summary>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public void SetFloat(string key, float value)
        {
            this.SetValue(key, value, typeof(float));
        }

        /// <summary>
        /// List float型のデータとキーをセットする
        /// </summary>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public void SetFloats(string key, List<float> value)
        {
            this.SetValue(key, value, typeof(List<float>));
        }

        /// <summary>
        /// int型のデータとキーをセットする
        /// </summary>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public void SetInt(string key, int value)
        {
            this.SetValue(key, value, typeof(int));
        }

        /// <summary>
        /// List int型のデータとキーをセットする
        /// </summary>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public void SetInts(string key, List<int> value)
        {
            this.SetValue(key, value, typeof(List<int>));
        }

        /// <summary>
        /// string型のデータとキーをセットする
        /// </summary>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public void SetString(string key, string value)
        {
            this.SetValue(key, value, typeof(string));
        }

        /// <summary>
        /// List string型のデータとキーをセットする
        /// </summary>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public void SetStrings(string key, List<string> value)
        {
            this.SetValue(key, value, typeof(List<string>));
        }

        /// <summary>
        /// bool型のデータとキーをセットする
        /// </summary>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public void SetBool(string key, bool value)
        {
            this.SetValue(key, value, typeof(bool));
        }

        /// <summary>
        /// List bool型のデータとキーをセットする
        /// </summary>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public void SetBools(string key, List<bool> value)
        {
            this.SetValue(key, value, typeof(List<bool>));
        }

        /// <summary>
        /// データとキーを内部的にセットする
        /// </summary>
        /// <typeparam name="T">データの型</typeparam>
        /// <param name="key">データのキー</param>
        /// <param name="value">データ</param>
        /// <param name="type">データの型情報</param>
        protected abstract void SetValue<T>(string key, T value, Type type = null);
    }
}
