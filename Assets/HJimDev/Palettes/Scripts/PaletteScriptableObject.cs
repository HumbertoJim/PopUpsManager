using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    namespace Palettes
    {
        [CreateAssetMenu(fileName = "Palette", menuName = "UI/Palette")]
        public class PaletteScriptableObject : ScriptableObject
        {
            public Color primary = Color.black;
            public Color secondary = Color.white;
            public Color background = Color.white;
            public Color surface = Color.white;
            public Color error = new(0.6901961f, 0, 0.1254902f, 1);

            public Palette ToPalette()
            {
                return new(primary, secondary, background, surface, error);
            }
        }
    }
}