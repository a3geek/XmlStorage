using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Linq;


namespace XmlStorage {
    using Components;

    /// <summary>セットしたデータ群をXML形式で保存する</summary>
    public static partial class XmlStorage {
        #region "Setters"
        /// <summary>
        /// 任意の型のデータとキーをセットする
        /// </summary>
        /// <typeparam name="T">セットするデータの型(Serializable)</typeparam>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public static void Set<T>(string key, T value, string aggregationName = null) where T : new() {
            Action4ChosenAggregation(aggregationName, agg => agg.Set(key, value));
        }
        
        /// <summary>
        /// float型のデータとキーをセットする
        /// </summary>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public static void SetFloat(string key, float value, string aggregationName = null) {
            Action4ChosenAggregation(aggregationName, agg => agg.SetFloat(key, value));
        }

        /// <summary>
        /// int型のデータとキーをセットする
        /// </summary>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public static void SetInt(string key, int value, string aggregationName = null) {
            Action4ChosenAggregation(aggregationName, agg => agg.SetInt(key, value));
        }

        /// <summary>
        /// string型のデータとキーをセットする
        /// </summary>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public static void SetString(string key, string value, string aggregationName = null) {
            Action4ChosenAggregation(aggregationName, agg => agg.SetString(key, value));
        }

        /// <summary>
        /// bool型のデータとキーをセットする
        /// </summary>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public static void SetBool(string key, bool value, string aggregationName = null) {
            Action4ChosenAggregation(aggregationName, agg => agg.SetBool(key, value));
        }
        #endregion

        #region "Getters"
        /// /// <summary>
        /// 引数に渡したキーと対応する任意の型のデータを取得する
        /// </summary>
        /// <typeparam name="T">取得するデータの型</typeparam>
        /// <param name="key">取得するデータのキー</param>
        /// /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public static T Get<T>(string key, T defaultValue = default(T), string aggregationName = null) {
            return Action4ChosenAggregation(aggregationName, agg => agg.Get(key, defaultValue));
        }
        
        /// <summary>
        /// 引数に渡したキーと対応するfloat型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public static float GetFloat(string key, float defaultValue = default(float), string aggregationName = null) {
            return Action4ChosenAggregation(aggregationName, agg => agg.GetFloat(key, defaultValue));
        }

        /// <summary>
        /// 引数に渡したキーと対応するint型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public static int GetInt(string key, int defaultValue = default(int), string aggregationName = null) {
            return Action4ChosenAggregation(aggregationName, agg => agg.GetInt(key, defaultValue));
        }

        /// <summary>
        /// 引数に渡したキーと対応するstring型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public static string GetString(string key, string defaultValue = "", string aggregationName = null) {
            return Action4ChosenAggregation(aggregationName, agg => agg.GetString(key, defaultValue));
        }

        /// <summary>
        /// 引数に渡したキーと対応するbool型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public static bool GetBool(string key, bool defaultValue = default(bool), string aggregationName = null) {
            return Action4ChosenAggregation(aggregationName, agg => agg.GetBool(key, defaultValue));
        }
        #endregion
    }
}
