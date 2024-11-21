using System;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace GaMe.ExMesh
{
    public static class ExGizmosUtility
    {
        /// <summary>
        /// 指定のクラスを継承したサブクラスのタイプ配列を取得
        /// </summary>
        /// <typeparam name="T">classのタイプ</typeparam>
        /// <param name="target_">拡張メソッドとして呼ぶタイプターゲット</param>
        /// <returns>タイプ配列</returns>
        public static Type[] GetSubClassTypes<T>(this T target_) where T : class
        {
            var _type = typeof(T);
            return Assembly.GetAssembly(_type)
                .GetTypes()
                .Where(t_ =>
                    t_.IsSubclassOf(_type) &&
                    !t_.IsAbstract
                )
                .ToArray();
        }

        /// <summary>
        /// 指定のクラスを継承したサブクラスのインスタンス配列を取得
        /// </summary>
        /// <typeparam name="T">classのタイプ</typeparam>
        /// <param name="target_">拡張メソッドとして呼ぶタイプターゲット</param>
        /// <returns>インスタンス配列</returns>
        public static T[] GetSubClassInstances<T>(this T target_) where T : class
        {
            var _type = typeof(T);
            return Assembly.GetAssembly (_type)
                .GetTypes()
                .Where(t_ =>
                    t_.IsSubclassOf(_type) &&
                    !t_.IsAbstract
                )
                .Select(t_ => Activator.CreateInstance(t_) as T)
                .ToArray();
        }

        /// <summary>
        /// 指定のインターフェースを継承したタイプ配列をを取得する
        /// </summary>
        /// <typeparam name="T">interfaceのタイプ</typeparam>
        /// <returns>タイプ配列</returns>
        /// 参考 https://qiita.com/Namagomi_Guria/items/dbba0cb5c97cdf090a0e
        public static Type[] GetInterfaceDerivedTypes<T>()
        {
            var _type = typeof(T);
            return Assembly.GetAssembly( _type)
                .GetTypes()
                .Where(t_ =>
                    _type.IsAssignableFrom(t_) &&
                    !t_.IsAbstract &&
                    !t_.IsInterface
                )
                .ToArray();
        }

        /// <summary>
        /// 指定のインターフェースを継承したタイプ配列を取得する　クラスのみ版
        /// </summary>
        /// <typeparam name="T">interfaceのタイプ</typeparam>
        /// <returns>タイプ配列　クラスのみ</returns>
        public static Type[] GetInterfaceDerivedClassTypes<T>()
        {
            var _type = typeof(T);
            return Assembly.GetAssembly(_type)
                .GetTypes()
                .Where(t_ =>
                    _type.IsAssignableFrom(t_) &&
                    !t_.IsAbstract &&
                    !t_.IsInterface &&
                    t_.IsClass
                )
                .ToArray();
        }
    }
}