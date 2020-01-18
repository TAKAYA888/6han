using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionGame
{
    public static class MyMath
    {
        public const float PI = 3.14159265359f;//円周率
        public const float Deg2Rad = PI / 180f;//度からラジアンに変換する定数

        /// <summary> 
        /// 四角形同士が重なっているか？ 
        /// </summary> 
        /// <param name="aLeft">A左端</param> 
        /// <param name="aTop">A上端</param> 
        /// <param name="aRight">A右端</param> 
        /// <param name="aBottom">A下端</param> 
        /// <param name="bLeft">B左端</param> 
        /// <param name="bTop">B上端</param> 
        /// <param name="bRight">B右端</param> 
        /// <param name="bBottom">B下端</param> 
        /// <returns>重なっていたらtrue, 重なっていなければfalse</returns> 
        public static bool RectRectIntersect(
            float aLeft, float aTop, float aRight, float aBottom,
            float bLeft, float bTop, float bRight, float bBottom)
        {
            return
                aLeft < bRight &&
                aRight > bLeft &&
                aTop < bBottom &&
                aBottom > bTop;
        }
        /// <summary>
        /// 点から点への角度（ラジアン）を求める。
        /// </summary>
        /// <param name="fromX">始点x</param>
        /// <param name="fromY">始点y</param>
        /// <param name="toX">終点x</param>
        /// <param name="toY">終点y</param>
        /// <returns></returns>
        public static float PointToPointAngle(float fromX, float fromY, float toX, float toY)
        {
            return (float)Math.Atan2(toY - fromY, toX - fromX);
        }

        /// <summary>
        /// 線形補間
        /// </summary>
        /// <param name="a">開始値</param>
        /// <param name="b">終了値</param>
        /// <param name="t">進捗率（0～1）</param>
        /// <returns></returns>
        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }
    }
}
