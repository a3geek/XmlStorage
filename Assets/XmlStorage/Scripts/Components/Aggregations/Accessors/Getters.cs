using System;
using System.Collections.Generic;

namespace XmlStorage.Components.Aggregations.Accessors
{
    public abstract class Getters : Setters
    {
        /// <summary>
        /// キーと対応する任意の型のデータを取得する
        /// </summary>
        /// <typeparam name="T">取得するデータの型</typeparam>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public T Get<T>(string key, T defaultValue = default(T))
        {
            return this.GetValue(key, defaultValue, typeof(T), obj => (T)obj);
        }

        /// <summary>
        /// キーと対応する任意の型のデータを取得する
        /// </summary>
        /// <typeparam name="T">取得するデータの型</typeparam>
        /// <param name="type">データの型情報</param>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public T Get<T>(Type type, string key, T defaultValue = default(T))
        {
            return this.GetValue(key, defaultValue, type ?? typeof(T), obj => (T)obj);
        }

        /// <summary>
        /// キーと対応する任意のList型のデータを取得する
        /// </summary>
        /// <typeparam name="T">取得するデータの型</typeparam>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public List<T> Gets<T>(string key, List<T> defaultValue = default(List<T>))
        {
            return this.GetValue(key, defaultValue, typeof(List<T>), obj => (List<T>)obj);
        }

        /// <summary>
        /// キーと対応するfloat型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public float GetFloat(string key, float defaultValue = default(float))
        {
            return this.GetValue(key, defaultValue, typeof(float), null);
        }

        /// <summary>
        /// キーと対応するList float型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public List<float> GetFloats(string key, List<float> defaultValue = default(List<float>))
        {
            return this.GetValue(key, defaultValue, typeof(List<float>), null);
        }

        /// <summary>
        /// キーと対応するint型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public int GetInt(string key, int defaultValue = default(int))
        {
            return this.GetValue(key, defaultValue, typeof(int), null);
        }

        /// <summary>
        /// キーと対応するList int型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public List<int> GetInts(string key, List<int> defaultValue = default(List<int>))
        {
            return this.GetValue(key, defaultValue, typeof(List<int>), null);
        }

        /// <summary>
        /// キーと対応するstring型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public string GetString(string key, string defaultValue = "")
        {
            return this.GetValue(key, defaultValue, typeof(string), null);
        }

        /// <summary>
        /// キーと対応するList string型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public List<string> GetStrings(string key, List<string> defaultValue = default(List<string>))
        {
            return this.GetValue(key, defaultValue, typeof(List<string>), null);
        }

        /// <summary>
        /// キーと対応するbool型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public bool GetBool(string key, bool defaultValue = default(bool))
        {
            return this.GetValue(key, defaultValue, typeof(bool), null);
        }

        /// <summary>
        /// キーと対応するList bool型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public List<bool> GetBools(string key, List<bool> defaultValue = default(List<bool>))
        {
            return this.GetValue(key, defaultValue, typeof(List<bool>), null);
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
        protected abstract T GetValue<T>(string key, T defaultValue, Type type = null, Func<object, T> converter = null);

        /// <summary>
        /// データの型と対応するキーを取得する
        /// </summary>
        /// <param name="type">データの型情報</param>
        /// <returns>データの型と対応するキー</returns>
        public abstract string [] GetKeys(Type type);

        /// <summary>
        /// データの型情報を取得する
        /// </summary>
        /// <returns>データの型情報</returns>
        public abstract Type [] GetTypes();

    }
}
