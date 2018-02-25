using System;
using System.Text;
using UnityEngine;

namespace EventBinder
{
    public static class Extensions
    {
        
        
        /**STRING*/

        public static int ParseToInt(this string target)
        {
            int returnValue = 0;
            int.TryParse (target, out returnValue);

            return returnValue;
        }
        
        public static float ParseToFloat(this string target)
        {
            float returnValue = 0;
            float.TryParse (target, out returnValue);

            return returnValue;
        }
        
        public static double ParseToDouble(this string target)
        {
            double returnValue = 0;
            double.TryParse (target, out returnValue);

            return returnValue;
        }
        
        
        public static bool CanDeserializeToVector2(this string target)
        {
            return target.Split (' ').Length == 2;
        }
        
        public static bool CanDeserializeToVector3(this string target)
        {
            return target.Split (' ').Length == 3;
        }
        
        public static bool CanDeserializeToVector4(this string target)
        {
            return target.Split (' ').Length == 4;
        }
        
        public static Vector2 DeserializeToVector2(this string target)
        {
            string[] values = target.Split(' ');
            if (values.Length != 2) throw new FormatException("component count mismatch. Expected 2 components but got " + values.Length);
            Vector2 result = new Vector2(float.Parse(values[0]), float.Parse(values[1]));
            return result;
        }
        
        public static Vector3 DeserializeToVector3(this string target)
        {
            string[] values = target.Split(' ');
            if (values.Length != 3) throw new FormatException("component count mismatch. Expected 3 components but got " + values.Length);
            Vector3 result = new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
            return result;
        }
        
        public static Vector4 DeserializeToVector4(this string target)
        {
            
            string[] values = target.Split(' ');
            if (values.Length != 4) throw new FormatException("component count mismatch. Expected 4 components but got " + values.Length);
            Vector4 result = new Vector4(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]), float.Parse(values[3]));
            return result;
        }
        
        
        
        
        
        
        /**VECTOR 2*/
        public static string SerializeToString(this Vector2 target)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append (target.x).Append (" ").Append (target.y);
            
            return sb.ToString();
        }
        
        /**VECTOR 3*/
        public static string SerializeToString(this Vector3 target)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(target.x).Append(" ").Append(target.y).Append(" ").Append(target.z);
            
            return sb.ToString();
        }
        
        /**VECTOR 4*/
        public static string SerializeToString(this Vector4 target)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(target.x).Append(" ").Append(target.y).Append(" ").Append(target.z).Append(" ").Append(target.w);
            
            return sb.ToString();
        }
        
    }
}