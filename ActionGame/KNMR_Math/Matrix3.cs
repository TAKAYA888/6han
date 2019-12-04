using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMath_KNMR
{
    public struct Matrix3
    {
        public float M11;
        public float M33;
        public float M31;
        public float M32;
        public float M23;
        public float M22;
        public float M21;
        public float M13;
        public float M12;


        public Matrix3(
            float Im11, float Im12, float Im13,
            float Im21, float Im22, float Im23,
            float Im31, float Im32, float Im33)
        {
            M11 = Im11; M12 = Im12; M13 = Im13;
            M21 = Im21; M22 = Im22; M23 = Im23;
            M31 = Im31; M32 = Im32; M33 = Im33;
        }

        public Matrix3(
            float Value)
        {
            M11 = Value; M12 = Value; M13 = Value;
            M21 = Value; M22 = Value; M23 = Value;
            M31 = Value; M32 = Value; M33 = Value;
        }

        /// <summary>
        /// 移動させます・・・たぶん
        /// </summary>
        /// <param name="translation">移動させたい座標をVector2で指定してください</param>
        /// <returns></returns>
        public static Matrix3 createTranslation(Vector2 translation)
        {
            return new Matrix3(
                1.0f, 0.0f, 0.0f,
                0.0f, 1.0f, 0.0f,
                translation.x, translation.y, 1.0f
                );
        }

        public static Matrix3 createRotation(float angle)
        {
            float sinValue = MathHelper.sin(angle);
            float cosValue = MathHelper.cos(angle);


            return new Matrix3(
                cosValue, sinValue, 0.0f,
                -sinValue, cosValue, 0.0f,
                0.0f, 0.0f, 1.0f
                );
        }
    }
}
