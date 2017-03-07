using System;
using System.Collections.Generic;

namespace XmlStorage.Components
{
    public sealed partial class Aggregation
    {
        #region "Setters"
        /// <summary>
        /// 任意の型のデータとキーをセットする
        /// </summary>
        /// <typeparam name="T">セットするデータの型(Serializable)</typeparam>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public void Set<T>(string key, T value)
        {
            this.SetValue(key, typeof(T), value);
        }

        /// <summary>
        /// 任意の型のデータとキーをセットする
        /// </summary>
        /// <typeparam name="T">セットするデータの型(Serializable)</typeparam>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="type">セットするデータの型情報</param>
        /// <param name="value">セットするデータ</param>
        public void Set<T>(string key, Type type, T value)
        {
            this.SetValue(key, type, value);
        }

        /// <summary>
        /// float型のデータとキーをセットする
        /// </summary>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public void SetFloat(string key, float value)
        {
            this.SetValue(key, typeof(float), value);
        }

        /// <summary>
        /// int型のデータとキーをセットする
        /// </summary>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public void SetInt(string key, int value)
        {
            this.SetValue(key, typeof(int), value);
        }

        /// <summary>
        /// string型のデータとキーをセットする
        /// </summary>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public void SetString(string key, string value)
        {
            this.SetValue(key, typeof(string), value);
        }

        /// <summary>
        /// bool型のデータとキーをセットする
        /// </summary>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public void SetBool(string key, bool value)
        {
            this.SetValue(key, typeof(bool), value);
        }

        /// <summary>
        /// データとキーを内部的にセットする
        /// </summary>
        /// <typeparam name="T">データの型</typeparam>
        /// <param name="key">データのキー</param>
        /// <param name="type">データの型情報</param>
        /// <param name="value">データ</param>
        private void SetValue<T>(string key, Type type, T value)
        {
            type = type ?? typeof(T);
            if(!this.dictionary.ContainsKey(type))
            {
                dictionary[type] = new Dictionary<string, object>();
            }

            this.dictionary[type][key] = value;
        }
        #endregion

        #region "Getters"
        /// <summary>
        /// キーと対応する任意の型のデータを取得する
        /// </summary>
        /// <typeparam name="T">取得するデータの型</typeparam>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="type">データの型情報</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public T Get<T>(string key, Type type = null, T defaultValue = default(T))
        {
            return this.GetValue(key, type ?? typeof(T), defaultValue, obj => (T)obj);
        }

        /// <summary>
        /// キーと対応するfloat型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public float GetFloat(string key, float defaultValue = default(float))
        {
            return this.GetValue(key, typeof(float), defaultValue, null);
        }

        /// <summary>
        /// キーと対応するint型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public int GetInt(string key, int defaultValue = default(int))
        {
            return this.GetValue(key, typeof(int), defaultValue, null);
        }

        /// <summary>
        /// キーと対応するstring型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public string GetString(string key, string defaultValue = "")
        {
            return this.GetValue(key, typeof(string), defaultValue, null);
        }

        /// <summary>
        /// キーと対応するbool型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public bool GetBool(string key, bool defaultValue = default(bool))
        {
            return this.GetValue(key, typeof(bool), defaultValue, null);
        }

        /// <summary>
        /// キーと対応するデータを取得する
        /// </summary>
        /// <typeparam name="T">データの型</typeparam>
        /// <param name="key">データのキー</param>
        /// <param name="type">データの型情報</param>
        /// <param name="defaultValue">データが存在しなかった時の返り値</param>
        /// <param name="converter">型変換処理</param>
        /// <returns>データ</returns>
        private T GetValue<T>(string key, Type type, T defaultValue, Func<object, T> converter = null)
        {
            type = type ?? typeof(T);

            return this.HasKey(key, type) ?
                (converter == null ? (T)this.dictionary[type][key] : converter(dictionary[type][key])) :
                defaultValue;
        }
        #endregion
    }
}
