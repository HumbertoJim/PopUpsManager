using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    namespace PopUps
    {
        namespace Alert
        {
            public delegate void ConfirmDelegate();

            public class AlertPopUp : BasePopUp
            {
                [SerializeField] Button confirmButton;
                [SerializeField] TMP_Text confirmButtonText;
                [SerializeField] Image background;
                [SerializeField] TMP_Text message;
                ConfirmDelegate confirmDelegate;

                public Button ConfirmButton { get { return confirmButton; } }
                public TMP_Text ConfirmButtonText { get { return confirmButtonText; } }
                public Image Background { get { return background; } }
                public TMP_Text Message { get { return message; } }

                protected override bool Initialize(params object[] args)
                {
                    bool initialized = base.Initialize(args);
                    if (initialized)
                    {
                        message.text = ValidateParameter<string>(0, nameof(message), null);
                        confirmDelegate = ValidateParameter<ConfirmDelegate>(1, nameof(confirmDelegate), null);

                        confirmButton.onClick.AddListener(Confirm);
                    }
                    return initialized;
                }

                protected override void InitializeUIElements()
                {
                    ValidateField(nameof(confirmButton), confirmButton);
                    ValidateField(nameof(confirmButtonText), confirmButtonText);
                    ValidateField(nameof(background), background);
                    ValidateField(nameof(message), message);

                    ColorBlock colors = confirmButton.colors;
                    colors.normalColor = PopUpsManager.DefaultManager.Palette.Secondary.Color;
                    colors.highlightedColor = PopUpsManager.DefaultManager.Palette.Secondary.Lighten2.Color;
                    colors.pressedColor = PopUpsManager.DefaultManager.Palette.Secondary.Darken2.Color;
                    colors.selectedColor = PopUpsManager.DefaultManager.Palette.Secondary.Color;
                    colors.disabledColor = PopUpsManager.DefaultManager.Palette.Secondary.Darken1.Color;
                    confirmButton.colors = colors;

                    confirmButtonText.color = PopUpsManager.DefaultManager.Palette.Secondary.TextColor;
                    background.color = PopUpsManager.DefaultManager.Palette.Surface.Color;
                    message.color = PopUpsManager.DefaultManager.Palette.Surface.TextColor;
                }

                public bool Initialize(string message, ConfirmDelegate confirmDelegate)
                {
                    return Initialize((object)message, (object)confirmDelegate);
                }

                public void Confirm()
                {
                    if (Close())
                    {
                        confirmDelegate();
                    }
                }
            }
        }
    }
}
