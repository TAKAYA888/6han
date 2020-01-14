using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMath_KNMR
{
    public struct Vector2
    {
        public float x, y;

        public Vector2(float x = 0.0f,float y = 0.0f)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2(float value)
        {
            this.x = value;
            this.y = value;
        }

        //public static float length(Vector2 value)
        //{
        //    return Math.Sqrt(dot(value,));
        //}

        /// <summary>
        /// 2つのベクトルの内積を計算します
        /// </summary>
        /// <param name="number1">1個目の値をVector2で入力してください</param>
        /// <param name="number2">2個目の値をVector2で入力してください</param>
        /// <returns>計算結果を返します</returns>
        public static float dot(Vector2 number1,Vector2 number2)
        {
            return (number1.x * number2.x) + (number1.y * number2.y);
        }

        /// <summary>
        /// 引数の行列を使って移動させる
        /// </summary>
        /// <param name="vector">Vector2を使って入力してください</param>
        /// <param name="matrix3">Matrix3を使って入力してください</param>
        /// <returns>計算後の値を入力します</returns>
        public static Vector2 transform(Vector2 vector,Matrix3 matrix3)
        {
            float w = vector.x * matrix3.M13 + vector.y * matrix3.M23 + matrix3.M33;

            return new Vector2(
                (vector.x * matrix3.M11 + vector.y + matrix3.M21 + matrix3.M31) / w,
                (vector.x * matrix3.M12 + vector.y * matrix3.M22 + matrix3.M32) / w);
        }
        //public static float normalize(Vector2 value)
        //{
        //    return Vector2()
        //}

        //public static Vector2 normalized()
        //{

        //}        

        //演算子オーバーロード
        //--------------------------------------------------------------------------------------------------------------------------------
        public static Vector2 operator + (Vector2 vector2_1,Vector2 vector2_2)
        {
            return new Vector2(vector2_1.x + vector2_2.x, vector2_1.y + vector2_2.y);
        }

        public static Vector2 operator -(Vector2 vector2_1, Vector2 vector2_2)
        {
            return new Vector2(vector2_1.x - vector2_2.x, vector2_1.y - vector2_2.y);
        }

        public static Vector2 operator *(Vector2 vector2_1, Vector2 vector2_2)
        {
            return new Vector2(vector2_1.x * vector2_2.x, vector2_1.y * vector2_2.y);
        }

        public static Vector2 operator /(Vector2 vector2_1, Vector2 vector2_2)
        {
            return new Vector2(vector2_1.x / vector2_2.x, vector2_1.y / vector2_2.y);
        }
        //--------------------------------------------------------------------------------------------------------------------------------
        public static Vector2 operator +(Vector2 vector2, float Value)
        {
            return new Vector2(vector2.x + Value, vector2.y + Value);
        }
        public static Vector2 operator -(Vector2 vector2, float Value)
        {
            return new Vector2(vector2.x - Value, vector2.y - Value);
        }
        public static Vector2 operator *(Vector2 vector2, float Value)
        {
            return new Vector2(vector2.x * Value, vector2.y * Value);
        }
        public static Vector2 operator /(Vector2 vector2, float Value)
        {
            return new Vector2(vector2.x / Value, vector2.y / Value);
        }
        //--------------------------------------------------------------------------------------------------------------------------------        


        public static bool operator ==(Vector2 vector2_1, Vector2 vector2_2)
        {
            return (vector2_1.x == vector2_2.x) && (vector2_1.y == vector2_2.y);
        }

        public static bool operator !=(Vector2 vector2_1, Vector2 vector2_2)
        {
            return (vector2_1.x != vector2_2.x) && (vector2_1.y != vector2_2.y);
        }

        //--------------------------------------------------------------------------------------------------------------------------------
        public static Vector2 operator * (Vector2 vector2,Matrix3 matrix3)
        {
            return transform(vector2, matrix3);
        }
        //--------------------------------------------------------------------------------------------------------------------------------
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}


