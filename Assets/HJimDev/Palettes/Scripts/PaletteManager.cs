using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EventBus;

namespace UI.Palettes
{
    public class PaletteManager : MonoBehaviour
    {
        private static PaletteManager manager;
        private Palette palette;
        bool initialized;

        public Palette Palette
        {
            get { return palette; }
            set
            {
                palette = value;
                EventManager.DefaultManager.Publish(Events.Constants.ChangePalette);
            }
        }

        public static PaletteManager DefaultManager
        {
            get
            {
                if (!manager)
                {
                    GameObject instance = new("PaletteManager");
                    DefaultManager = instance.AddComponent<PaletteManager>();
                }
                return manager;
            }
            private set
            {
                manager = value;
                manager.Initialize();
            }
        }

        private void Awake()
        {
            if (manager && manager != this)
            {
                DestroyImmediate(gameObject);
            }
            else if (!manager)
            {
                DefaultManager = this;
            }
        }

        void Initialize()
        {
            if (!initialized)
            {
                initialized = true;
                palette = new(Color.black, Color.gray, Color.white, Color.white, Color.red);
                DontDestroyOnLoad(manager.gameObject);
            }
        }

        public void ApplyBlockColorPalette(Adapters.IBlockColorUIElement element, PaletteColor color)
        {
            ColorBlock colors;
            colors = element.Colors;
            colors.normalColor = color.Color;
            colors.highlightedColor = color.Lighten2.Color;
            colors.pressedColor = color.Darken2.Color;
            colors.selectedColor = color.Color;
            colors.disabledColor = color.Darken1.Color;
            element.Colors = colors;
        }

        public void ApplyBlockColorPalette(Adapters.IBlockColorUIElement element)
        {
            ApplyBlockColorPalette(element, palette.Secondary);
        }

        public void ApplyBlockColorPalette(Adapters.IBlockColorUIElement element, PaletteColor color, params Adapters.IText[] texts)
        {
            ApplyBlockColorPalette(element, color);
            ApplyTextPalette(color, texts);
        }

        public void ApplyBlockColorPalette(Adapters.IBlockColorUIElement element, params Adapters.IText[] texts)
        {
            ApplyBlockColorPalette(element);
            ApplyTextPalette(texts);
        }

        public void ApplySurfacePalette(Image surface, PaletteColor color)
        {
            surface.color = color.Color;
        }

        public void ApplySurfacePalette(Image surface)
        {
            ApplySurfacePalette(surface, palette.Surface);
        }

        public void ApplySurfacePalette(Image surface, PaletteColor color, params Adapters.IText[] texts)
        {
            ApplySurfacePalette(surface, color);
            ApplyTextPalette(color, texts);
        }

        public void ApplySurfacePalette(Image surface, params Adapters.IText[] texts)
        {
            ApplySurfacePalette(surface);
            ApplyTextPalette(texts);
        }

        public void ApplyTextPalette(PaletteColor surfaceColor, params Adapters.IText[] texts)
        {
            foreach(Adapters.IText text in texts)
            {
                text.Color = surfaceColor.TextColor;
            }
        }

        public void ApplyTextPalette(params Adapters.IText[] texts)
        {
            ApplyTextPalette(palette.Surface, texts);
        }
    }

    namespace Events {
        public static class Constants
        {
            public const string ChangePalette = "ChangePalette";
        }
    }

    namespace Adapters
    {
        public interface IText
        {
            Color Color { get; set; }
        }

        public class TextAdapter: IText
        {
            readonly Text text;
            public Color Color { get { return text.color; } set { text.color = value; } }
            public TextAdapter(Text text) => this.text = text;
        }

        public class TMP_TextAdapter : IText
        {
            readonly TMP_Text text;
            public Color Color { get { return text.color; } set { text.color = value; } }
            public TMP_TextAdapter(TMP_Text text) => this.text = text;
        }

        public interface IBlockColorUIElement
        {
            ColorBlock Colors { get; set; }
        }

        public class ButtonAdapter: IBlockColorUIElement
        {
            readonly Button element;
            public ColorBlock Colors { get { return element.colors; } set { element.colors = value; } }
            public ButtonAdapter(Button element) => this.element = element;
        }

        public class InputFieldAdapter : IBlockColorUIElement
        {
            readonly InputField element;
            public ColorBlock Colors { get { return element.colors; } set { element.colors = value; } }
            public InputFieldAdapter(InputField element) => this.element = element;
        }

        public class TMP_InputFieldAdapter : IBlockColorUIElement
        {
            readonly TMP_InputField element;
            public ColorBlock Colors { get { return element.colors; } set { element.colors = value; } }
            public TMP_InputFieldAdapter(TMP_InputField element) => this.element = element;
        }

        public class ToggleAdapter : IBlockColorUIElement
        {
            readonly Toggle element;
            public ColorBlock Colors { get { return element.colors; } set { element.colors = value; } }
            public ToggleAdapter(Toggle element) => this.element = element;
        }
    }
}
