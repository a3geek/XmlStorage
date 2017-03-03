using System;

namespace XmlStorage
{
    public static partial class XmlStorage
    {
        #region "Setters"
        /// <summary>
        /// 任意の型のデータとキーをセットする
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <typeparam name="T">セットするデータの型(Serializable)</typeparam>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        public static void Set<T>(string key, T value, string aggregationName = null)
        {
            Action4ChosenAggregation(aggregationName, agg => agg.Set(key, value));
        }

        /// <summary>
        /// 任意の型のデータとキーをセットする
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <typeparam name="T">セットするデータの型(Serializable)</typeparam>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        /// <param name="type">セットするデータの型情報</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        public static void Set<T>(string key, T value, Type type, string aggregationName = null)
        {
            Action4ChosenAggregation(aggregationName, agg => agg.Set(key, value, type));
        }

        /// <summary>
        /// float型のデータとキーをセットする
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        public static void SetFloat(string key, float value, string aggregationName = null)
        {
            Action4ChosenAggregation(aggregationName, agg => agg.SetFloat(key, value));
        }

        /// <summary>
        /// int型のデータとキーをセットする
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        public static void SetInt(string key, int value, string aggregationName = null)
        {
            Action4ChosenAggregation(aggregationName, agg => agg.SetInt(key, value));
        }

        /// <summary>
        /// string型のデータとキーをセットする
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        public static void SetString(string key, string value, string aggregationName = null)
        {
            Action4ChosenAggregation(aggregationName, agg => agg.SetString(key, value));
        }

        /// <summary>
        /// bool型のデータとキーをセットする
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        public static void SetBool(string key, bool value, string aggregationName = null)
        {
            Action4ChosenAggregation(aggregationName, agg => agg.SetBool(key, value));
        }
        #endregion

        #region "Getters"
        /// /// <summary>
        /// キーと対応する任意の型のデータを取得する
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <typeparam name="T">取得するデータの型</typeparam>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <param name="type">取得するデータの型情報</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        /// <returns>キーに対応するデータ</returns>
        public static T Get<T>(string key, T defaultValue = default(T), Type type = null, string aggregationName = null)
        {
            return Action4ChosenAggregation(aggregationName, agg => agg.Get(key, defaultValue, type ?? typeof(T)));
        }

        /// <summary>
        /// キーと対応するfloat型のデータを取得する
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        /// <returns>キーに対応するデータ</returns>
        public static float GetFloat(string key, float defaultValue = default(float), string aggregationName = null)
        {
            return Action4ChosenAggregation(aggregationName, agg => agg.GetFloat(key, defaultValue));
        }

        /// <summary>
        /// キーと対応するint型のデータを取得する
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        /// <returns>キーに対応するデータ</returns>
        public static int GetInt(string key, int defaultValue = default(int), string aggregationName = null)
        {
            return Action4ChosenAggregation(aggregationName, agg => agg.GetInt(key, defaultValue));
        }

        /// <summary>
        /// キーと対応するstring型のデータを取得する
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        /// <returns>キーに対応するデータ</returns>
        public static string GetString(string key, string defaultValue = "", string aggregationName = null)
        {
            return Action4ChosenAggregation(aggregationName, agg => agg.GetString(key, defaultValue));
        }

        /// <summary>
        /// キーと対応するbool型のデータを取得する
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        /// <returns>キーに対応するデータ</returns>
        public static bool GetBool(string key, bool defaultValue = default(bool), string aggregationName = null)
        {
            return Action4ChosenAggregation(aggregationName, agg => agg.GetBool(key, defaultValue));
        }
        #endregion
    }
}
