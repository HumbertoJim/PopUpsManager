using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    namespace Palettes
    {
        public enum BrightnessLevel
        {
            Base, Darken1, Darken2, Darken3, Darken4, Darken5, Lighten1, Lighten2, Lighten3, Lighten4, Lighten5
        }

        public struct PaletteColor
        {
            public Color Color { get; }

            public PaletteColor Darken1 { get { return GetAdjustedColor(BrightnessLevel.Darken1); } }
            public PaletteColor Darken2 { get { return GetAdjustedColor(BrightnessLevel.Darken2); } }
            public PaletteColor Darken3 { get { return GetAdjustedColor(BrightnessLevel.Darken3); } }
            public PaletteColor Darken4 { get { return GetAdjustedColor(BrightnessLevel.Darken4); } }
            public PaletteColor Darken5 { get { return GetAdjustedColor(BrightnessLevel.Darken5); } }

            public PaletteColor Lighten1 { get { return GetAdjustedColor(BrightnessLevel.Lighten1); } }
            public PaletteColor Lighten2 { get { return GetAdjustedColor(BrightnessLevel.Lighten2); } }
            public PaletteColor Lighten3 { get { return GetAdjustedColor(BrightnessLevel.Lighten3); } }
            public PaletteColor Lighten4 { get { return GetAdjustedColor(BrightnessLevel.Lighten4); } }
            public PaletteColor Lighten5 { get { return GetAdjustedColor(BrightnessLevel.Lighten5); } }

            public Color TextColor { get { return Utils.GetBestTextColorForBackground(Color); } }

            public PaletteColor(Color color)
            {
                Color = color;
            }

            public PaletteColor GetAdjustedColor(BrightnessLevel brightness)
            {
                float brightnessFactor = brightness switch
                {
                    BrightnessLevel.Darken1 => -0.1f,
                    BrightnessLevel.Darken2 => -0.2f,
                    BrightnessLevel.Darken3 => -0.3f,
                    BrightnessLevel.Darken4 => -0.4f,
                    BrightnessLevel.Darken5 => -0.5f,
                    BrightnessLevel.Lighten1 => 0.1f,
                    BrightnessLevel.Lighten2 => 0.2f,
                    BrightnessLevel.Lighten3 => 0.3f,
                    BrightnessLevel.Lighten4 => 0.4f,
                    BrightnessLevel.Lighten5 => 0.5f,
                    _ => 0
                };
                return GetAdjustedColor(brightnessFactor);
            }

            public PaletteColor GetAdjustedColor(float brightnessFactor)
            {
                return new(Utils.AdjustedBrightness(Color, brightnessFactor));
            }
        }

        public struct Palette
        {
            public PaletteColor Primary { get; }
            public PaletteColor Secondary { get; }
            public PaletteColor Background { get; }
            public PaletteColor Surface { get; }
            public PaletteColor Error { get; }

            public Palette(PaletteColor primary, PaletteColor secondary, PaletteColor background, PaletteColor surface, PaletteColor error)
            {
                Primary = primary;
                Secondary = secondary;
                Background = background;
                Surface = surface;
                Error = error;
            }

            public Palette(Color primary, Color secondary, Color background, Color surface, Color error)
            {
                Primary = new(primary);
                Secondary = new(secondary);
                Background = new(background);
                Surface = new(surface);
                Error = new(error);
            }
        }
    }
}