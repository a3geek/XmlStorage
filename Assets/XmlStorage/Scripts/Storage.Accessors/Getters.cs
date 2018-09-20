using System;
using System.Collections.Generic;

namespace XmlStorage
{
    public static partial class Storage
    {
        /// <summary>
        /// キーと対応する任意の型のデータを取得する
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <typeparam name="T">取得するデータの型</typeparam>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        /// <returns>キーに対応するデータ</returns>
        public static T Get<T>(string key, T defaultValue = default(T), string aggregationName = null)
        {
            return Func(aggregationName, agg => agg.Get(key, defaultValue));
        }

        /// /// <summary>
        /// キーと対応する任意の型のデータを取得する
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <typeparam name="T">取得するデータの型</typeparam>
        /// <param name="type">取得するデータの型情報</param>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        /// <returns>キーに対応するデータ</returns>
        public static T Get<T>(Type type, string key, T defaultValue = default(T), string aggregationName = null)
        {
            return Func(aggregationName, agg => agg.Get(type, key, defaultValue));
        }

        /// <summary>
        /// キーと対応する任意のList型のデータを取得する
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <typeparam name="T">取得するデータの型</typeparam>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        /// <returns>キーに対応するデータ</returns>
        public static List<T> Gets<T>(string key, List<T> defaultValue = default(List<T>), string aggregationName = null)
        {
            return Func(aggregationName, agg => agg.Gets(key, defaultValue));
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
            return Func(aggregationName, agg => agg.GetFloat(key, defaultValue));
        }

        /// <summary>
        /// キーと対応するList float型のデータを取得する
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        /// <returns>キーに対応するデータ</returns>
        public static List<float> GetFloats(string key, List<float> defaultValue = default(List<float>), string aggregationName = null)
        {
            return Func(aggregationName, agg => agg.GetFloats(key, defaultValue));
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
            return Func(aggregationName, agg => agg.GetInt(key, defaultValue));
        }

        /// <summary>
        /// キーと対応するList int型のデータを取得する
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        /// <returns>キーに対応するデータ</returns>
        public static List<int> GetInts(string key, List<int> defaultValue = default(List<int>), string aggregationName = null)
        {
            return Func(aggregationName, agg => agg.GetInts(key, defaultValue));
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
            return Func(aggregationName, agg => agg.GetString(key, defaultValue));
        }

        /// <summary>
        /// キーと対応するList string型のデータを取得する
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        /// <returns>キーに対応するデータ</returns>
        public static List<string> GetStrings(string key, List<string> defaultValue = default(List<string>), string aggregationName = null)
        {
            return Func(aggregationName, agg => agg.GetStrings(key, defaultValue));
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
            return Func(aggregationName, agg => agg.GetBool(key, defaultValue));
        }

        /// <summary>
        /// キーと対応するList bool型のデータを取得する
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        /// <returns>キーに対応するデータ</returns>
        public static List<bool> GetBools(string key, List<bool> defaultValue = default(List<bool>), string aggregationName = null)
        {
            return Func(aggregationName, agg => agg.GetBools(key, defaultValue));
        }
        
        /// <summary>
        /// データの型と対応するキーを取得する
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="type">データの型情報</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        /// <returns>データの型と対応するキー</returns>
        public static string [] GetKeys(Type type, string aggregationName = null)
        {
            return Func(aggregationName, agg => agg.GetKeys(type));
        }

        /// <summary>
        /// データの型情報を取得する
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="aggregationName">データが所属する集団名</param>
        /// <returns>データの型情報</returns>
        public static Type[] GetTypes(string aggregationName = null)
        {
            return Func(aggregationName, agg => agg.GetTypes());
        }
    }
}
