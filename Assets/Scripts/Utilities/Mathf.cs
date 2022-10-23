using System;

namespace TenTen.Utilities
{
    public static class Mathf
    {
        public static int RoundToInt(float value, MidpointRounding rounding)
        {
            return (int) Math.Round(value, rounding);
        }
    }
}