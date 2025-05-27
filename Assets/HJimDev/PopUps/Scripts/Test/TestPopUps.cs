using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UI.PopUps.Select;
using UI.Palettes;
using UI.Palettes.Adapters;
using EventBus;

namespace UI
{
    namespace PopUps
    {
        namespace Tests
        {
            public class TestPopUps : MonoBehaviour
            {
                [SerializeField] PaletteScriptableObject[] palettes;
                [SerializeField] Image background;
                [SerializeField] Image topBar;
                [SerializeField] TMP_Text topBarText;
                [SerializeField] Button changePaletteButton;
                [SerializeField] TMP_Text changePaletteButtonText;
                int paletteIndex;
                Option opt1;
                Option opt2;

                private void Start()
                {
                    paletteIndex = -1;

                    EventManager.DefaultManager.Subscribe(Palettes.Events.Constants.ChangePalette, gameObject, (message) =>
                    {
                        background.color = PaletteManager.DefaultManager.Palette.Background.Color;
                        topBar.color = PaletteManager.DefaultManager.Palette.Primary.Color;
                        topBarText.color = PaletteManager.DefaultManager.Palette.Primary.TextColor;
                        PaletteManager.DefaultManager.ApplyBlockColorPalette(new ButtonAdapter(changePaletteButton), new TMP_TextAdapter(changePaletteButtonText));
                    });

                    changePaletteButton.onClick.AddListener(ChangePalette);
                    ChangePalette();

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

                void ChangePalette()
                {
                    if(palettes.Length > 0)
                    {
                        paletteIndex = (paletteIndex + 1) % palettes.Length;
                        PaletteManager.DefaultManager.Palette = palettes[paletteIndex].ToPalette();
                    }
                }
            }
        }
    }
}
