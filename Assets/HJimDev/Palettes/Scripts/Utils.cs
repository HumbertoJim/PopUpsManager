using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    namespace Palettes
    {
        public static class Utils
        {
            private static double Luminance(Color color)
            {
                double r = Adjust(color.r);
                double g = Adjust(color.g);
                double b = Adjust(color.b);
                return 0.2126 * r + 0.7152 * g + 0.0722 * b;
            }

            private static double Adjust(double c)
            {
                return (c <= 0.03928) ? (c / 12.92) : Mathf.Pow((float)((c + 0.055) / 1.055), 2.4f);
            }

            private static double ContrastRatio(double L1, double L2)
            {
                return (L1 + 0.05) / (L2 + 0.05);
            }

            public static Color GetBestTextColorForBackground(Color background)
            {
                double luminance = Luminance(background);
                double blackContrast = ContrastRatio(luminance, 0);
                double whiteContrast = ContrastRatio(1, luminance);
                return whiteContrast > blackContrast ? Color.white : Color.black;
            }

            public static Color AdjustedBrightness(Color color, float factor)
            {
                factor = factor > 1 ? 1 : (factor < -1 ? -1 : factor);
                float r = Mathf.Clamp01(color.r + factor);
                float g = Mathf.Clamp01(color.g + factor);
                float b = Mathf.Clamp01(color.b + factor);
                return new(r, g, b, color.a);
            }
        }
    }
}
