using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UI.PopUps.Select;

namespace UI
{
    namespace PopUps
    {
        namespace Tests
        {
            public class TestPopUps : MonoBehaviour
            {
                [SerializeField] Palettes.PaletteScriptableObject paletteScriptableObject;
                [SerializeField] Image background;
                [SerializeField] Image topBar;
                [SerializeField] TMP_Text topBarText;
                Palettes.Palette palette;
                Option opt1;
                Option opt2;

                private void Start()
                {
                    palette = paletteScriptableObject.ToPalette();
                    PopUpsManager.DefaultManager.SetPalette(palette);

                    background.color = palette.Background.Color;
                    topBar.color = palette.Primary.Color;
                    topBarText.color = palette.Primary.TextColor;

                    opt1 = new(0, "Repetir");
                    opt2 = new(1, "Finalizar");

                    ShowAlert();
                }

                void ShowAlert()
                {
                    PopUpsManager.DefaultManager.ShowAlert("Testing alert pop-up", ShowMessage);
                }

                void ShowMessage()
                {
                    PopUpsManager.DefaultManager.ShowMessage("Hello, there", "Testing message pop-up", ShowInput);
                }

                void ShowInput()
                {
                    PopUpsManager.DefaultManager.ShowInput("Enter your name", ShowSelect, false);
                }

                void ShowSelect(string name)
                {
                    PopUpsManager.DefaultManager.ShowSelect(
                        $"Welcome, {name}, select an option",
                        new() { opt1, opt2 },
                        Select
                    );
                }

                void Select(Option opt)
                {
                    if(opt == opt1)
                    {
                        PopUpsManager.DefaultManager.ShowConfirm("Estas seguro de que desas repetir?", Repete);
                    }
                }

                void Repete(bool confirm)
                {
                    if (confirm)
                    {
                        ShowAlert();
                    }
                }
            }
        }
    }
}
