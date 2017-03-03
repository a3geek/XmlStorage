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
            this.SetValue(key, value, typeof(T));
        }

        /// <summary>
        /// 任意の型のデータとキーをセットする
        /// </summary>
        /// <typeparam name="T">セットするデータの型(Serializable)</typeparam>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        /// <param name="type">セットするデータの型情報</param>
        public void Set<T>(string key, T value, Type type)
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
        /// int型のデータとキーをセットする
        /// </summary>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public void SetInt(string key, int value)
        {
            this.SetValue(key, value, typeof(int));
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
        /// bool型のデータとキーをセットする
        /// </summary>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public void SetBool(string key, bool value)
        {
            this.SetValue(key, value, typeof(bool));
        }

        /// <summary>
        /// データとキーを内部的にセットする
        /// </summary>
        /// <typeparam name="T">データの型</typeparam>
        /// <param name="key">データのキー</param>
        /// <param name="value">データ</param>
        /// <param name="type">データの型情報</param>
        private void SetValue<T>(string key, T value, Type type = null)
        {
            type = (type == null ? typeof(T) : type);
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
        public T Get<T>(string key, T defaultValue = default(T), Type type = null)
        {
            return this.GetValue(key, defaultValue, obj => (T)obj, type ?? typeof(T));
        }

        /// <summary>
        /// キーと対応するfloat型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public float GetFloat(string key, float defaultValue = default(float))
        {
            return this.GetValue(key, defaultValue, null, typeof(float));
        }

        /// <summary>
        /// キーと対応するint型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public int GetInt(string key, int defaultValue = default(int))
        {
            return this.GetValue(key, defaultValue, null, typeof(int));
        }

        /// <summary>
        /// キーと対応するstring型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public string GetString(string key, string defaultValue = "")
        {
            return this.GetValue(key, defaultValue, null, typeof(string));
        }

        /// <summary>
        /// キーと対応するbool型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public bool GetBool(string key, bool defaultValue = default(bool))
        {
            return this.GetValue(key, defaultValue, null, typeof(bool));
        }

        /// <summary>
        /// キーと対応するデータを取得する
        /// </summary>
        /// <typeparam name="T">データの型</typeparam>
        /// <param name="key">データのキー</param>
        /// <param name="defaultValue">データが存在しなかった時の返り値</param>
        /// <param name="converter">型変換処理</param>
        /// <param name="type">データの型情報</param>
        /// <returns>データ</returns>
        private T GetValue<T>(string key, T defaultValue, Func<object, T> converter = null, Type type = null)
        {
            type = (type == null ? typeof(T) : type);

            return this.HasKey(key, type) ?
                (converter == null ? (T)this.dictionary[type][key] : converter(dictionary[type][key])) :
                defaultValue;
        }
        #endregion
    }
}
