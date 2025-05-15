using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.PopUps.Select;

namespace UI
{
    namespace PopUps
    {
        namespace Tests
        {
            public class TestPopUps : MonoBehaviour
            {
                Option opt1;
                Option opt2;

                private void Start()
                {
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
