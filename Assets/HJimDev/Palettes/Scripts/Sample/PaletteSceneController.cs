using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI {
    namespace Palettes
    {
        namespace Sample
        {
            public class PaletteSceneController : MonoBehaviour
            {
                [SerializeField] Image topBar;
                [SerializeField] TMP_Text topBarTitle;
                [SerializeField] Image background;
                [SerializeField] Image sliderBackground;
                [SerializeField] Image sliderBackgroundFill;
                [SerializeField] Image sliderHandler;
                [SerializeField] Button button;
                [SerializeField] TMP_Text buttonText;
                [SerializeField] Image card;
                [SerializeField] TMP_Text cardText;
                [SerializeField] Image errorCard;
                [SerializeField] TMP_Text errorCardText;
                [SerializeField] TMP_Dropdown dropdown;
                [SerializeField] TMP_Text dropdownText;
                [SerializeField] Image dropdownTemplate;
                [SerializeField] Image dropdownTemplateIcon;
                [SerializeField] TMP_Text dropdownTemplateIconText;

                [SerializeField] List<PaletteScriptableObject> palettes;

                int paletteIndex;

                private void Awake()
                {
                    paletteIndex = -1;
                    NextPalette();
                }

                public void SetPalette(Palette palette)
                {
                    ColorBlock colorBlock;
                    topBar.color = palette.Primary.Color;
                    topBarTitle.color = palette.Primary.TextColor;
                    background.color = palette.Background.Color;
                    sliderBackground.color = palette.Secondary.Darken1.Color;
                    sliderBackgroundFill.color = palette.Secondary.Color;
                    sliderHandler.color = palette.Secondary.Darken2.Color;
                    colorBlock = button.colors;
                    colorBlock.normalColor = palette.Secondary.Color;
                    colorBlock.highlightedColor = palette.Secondary.Lighten2.Color;
                    colorBlock.pressedColor = palette.Secondary.Darken2.Color;
                    colorBlock.selectedColor = palette.Secondary.Color;
                    colorBlock.disabledColor = palette.Secondary.Lighten3.Color;
                    button.colors = colorBlock;
                    buttonText.color = palette.Secondary.TextColor;
                    card.color = palette.Surface.Color;
                    cardText.color = palette.Surface.TextColor;
                    errorCard.color = palette.Error.Color;
                    errorCardText.color = palette.Error.TextColor;
                    colorBlock = dropdown.colors;
                    colorBlock.normalColor = palette.Secondary.Color;
                    colorBlock.highlightedColor = palette.Secondary.Lighten2.Color;
                    colorBlock.pressedColor = palette.Secondary.Darken1.Color;
                    colorBlock.selectedColor = palette.Secondary.Color;
                    colorBlock.disabledColor = palette.Secondary.Lighten3.Color;
                    dropdown.colors = colorBlock;
                    dropdownText.color = palette.Secondary.TextColor;
                    dropdownTemplate.color = palette.Secondary.Darken1.Color;
                    dropdownTemplateIcon.color = palette.Secondary.Lighten1.Color;
                    dropdownTemplateIconText.color = palette.Secondary.Lighten1.TextColor;
                }
                
                public void NextPalette()
                {
                    if(palettes.Count > 0)
                    {
                        paletteIndex = (paletteIndex + 1) % palettes.Count;
                        SetPalette(palettes[paletteIndex].ToPalette());
                    }
                }
            }
        }
    }
}
