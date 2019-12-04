using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMath_KNMR
{    
    class MathHelper
    {
        // πの値を表します(float)
        public const float pi = (float)Math.PI;

        // 凄く０に近い数字を表します
        public const float kEpsilon = 1e-5f;

        /// <summary>
        /// 引数の値が正の数なら１、負の数なら-1を返します
        /// </summary>
        /// <param name="value">値を入力してください</param>
        /// <returns>1か-1で返します</returns>
        public static float sign(float value)
        {
            if (Math.Abs(value) < kEpsilon) return 1;

            else return (value / Math.Abs(value));
        }

        /// <summary>
        /// 値を指定された範囲内に制限します
        /// </summary>
        /// <param name="value">制限したい値を入れてください</param>
        /// <param name="min">制限する最低値を入力してください</param>
        /// <param name="max">制限する最大値を入力してください</param>
        /// <returns>制限された値を返します</returns>
        public static float clamp(float value, float min, float max)
        {
            return Math.Max(min, Math.Min(value, max));
        }

        /// <summary>
        /// 2つの値の差の絶対値を計算します
        /// </summary>
        /// <param name="value1">1個目の値を入れてください</param>
        /// <param name="value2">2個目の値を入れてください</param>
        /// <returns>2つの値の差の絶対値を返します</returns>
        public static float distance(float value1, float value2)
        {
            return Math.Abs(value1 - value2);
        }

        /// <summary>
        /// ラジアンを度に変換します。
        /// </summary>
        /// <param name="radian">角度をラジアンで入力してください</param>
        /// <returns>度数法で返却します</returns>
        public static float toDegrees(float radian)
        {
            return radian * (180.0f / pi);
        }

        /// <summary>
        /// 度をラジアンに変換します。
        /// </summary>
        /// <param name="degree">角度を度数法で入力してください</param>
        /// <returns>ラジアンで返却します</returns>
        public static float toRadians(float degree)
        {
            return degree * (pi / 180.0f);
        }

        // 引数：度数法----------------------------------------------------------------

        /// <summary>
        /// 度数法で入力した値をSinで計算してfloatで返します
        /// </summary>
        /// <param name="degree">度数法で入力してください</param>
        /// <returns></returns>
        public static float sin(float degree)
        {
            return (float)Math.Sin(toRadians(degree));
        }

        /// <summary>
        /// 度数法で入力した値をCosで計算してfloatで返します
        /// </summary>
        /// <param name="degree">度数法で入力してください</param>
        /// <returns></returns>
        public static float cos(float degree)
        {
            return (float)Math.Cos(toRadians(degree));
        }

        /// <summary>
        /// 度数法で入力した値をCosで計算してfloatで返します
        /// </summary>
        /// <param name="degree">度数法で入力してください</param>
        /// <returns></returns>
        public static float tan(float degree)
        {
            return (float)Math.Tan(toRadians(degree));
        }
        //-----------------------------------------------------------------------------

        //逆三角関数（戻り値：度数法）-------------------------------------------------

        /// <summary>
        /// 逆サイン（戻り値：度数法）
        /// </summary>
        /// <param name="s">三角関数の値で入れてください</param>
        /// <returns></returns>
        public static float asin(float s)
        {
            return toDegrees((float)Math.Asin(s));
        }

        /// <summary>
        /// 逆コサイン（戻り値：度数法）
        /// </summary>
        /// <param name="c">三角関数の値で入れてください</param>
        /// <returns></returns>
        public static float acos(float c)
        {
            return toDegrees((float)Math.Acos(c));
        }

        /// <summary>
        /// 逆タンジェント（戻り値：度数法）
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float atan(float y,float x)
        {
            return toDegrees((float)Math.Atan2(y, x));
        }

        //-----------------------------------------------------------------------------

    }
}
