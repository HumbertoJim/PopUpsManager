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
        namespace Confirm
        {
            public delegate void ConfirmDelegate(bool confirm);

            public class ConfirmPopUp : BasePopUp
            {
                [SerializeField] Button agreeButton;
                [SerializeField] TMP_Text agreeButtonText;
                [SerializeField] Button disagreeButton;
                [SerializeField] TMP_Text disagreeButtonText;
                [SerializeField] Image background;
                [SerializeField] TMP_Text message;
                ConfirmDelegate confirmDelegate;

                public Button AgreeButton { get { return agreeButton; } }
                public TMP_Text AgreeButtonText { get { return agreeButtonText; } }
                public Button DisagreeButton { get { return disagreeButton; } }
                public TMP_Text DisagreeButtonText { get { return disagreeButtonText; } }
                public Image Background { get { return background; } }
                public TMP_Text Message { get { return message; } }

                protected override bool Initialize(params object[] args)
                {
                    bool initialized = base.Initialize(args);
                    if (initialized)
                    {
                        message.text = ValidateParameter<string>(0, nameof(message), null);
                        confirmDelegate = ValidateParameter<ConfirmDelegate>(1, nameof(confirmDelegate), null);

                        agreeButton.onClick.AddListener(Agree);
                        disagreeButton.onClick.AddListener(Disagree);
                    }
                    return initialized;
                }

                protected override void InitializeUIElements()
                {
                    ValidateField(nameof(agreeButton), agreeButton);
                    ValidateField(nameof(agreeButtonText), agreeButtonText);
                    ValidateField(nameof(disagreeButton), disagreeButton);
                    ValidateField(nameof(disagreeButtonText), disagreeButtonText);
                    ValidateField(nameof(background), background);
                    ValidateField(nameof(message), message);

                    PaletteManager.DefaultManager.ApplyBlockColorPalette(new ButtonAdapter(agreeButton), new TMP_TextAdapter(agreeButtonText));
                    PaletteManager.DefaultManager.ApplyBlockColorPalette(new ButtonAdapter(disagreeButton), new TMP_TextAdapter(disagreeButtonText));
                    PaletteManager.DefaultManager.ApplySurfacePalette(background, new TMP_TextAdapter(message));
                }

                public bool Initialize(string message, ConfirmDelegate confirmDelegate)
                {
                    return Initialize((object)message, (object)confirmDelegate);
                }

                public void Agree() { if (Close()) { confirmDelegate(true); } }

                public void Disagree() { if (Close()) { confirmDelegate(false); } }
            }
        }
    }
}
