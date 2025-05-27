using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UI.Palettes;
using UI.Palettes.Adapters;

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

                    PaletteManager.DefaultManager.ApplyBlockColorPalette(new ButtonAdapter(confirmButton), new TMP_TextAdapter(confirmButtonText));
                    PaletteManager.DefaultManager.ApplySurfacePalette(background, new TMP_TextAdapter(message));
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
