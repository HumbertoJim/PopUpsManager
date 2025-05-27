using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UI.Palettes;
using UI.Palettes.Adapters;

namespace UI
{
    namespace PopUps
    {
        namespace Input
        {
            public delegate void SubmitDelegate(string input);

            public class InputPopUp : BasePopUp
            {
                [SerializeField] Button submitButton;
                [SerializeField] TMP_Text submitButtonText;
                [SerializeField] Image background;
                [SerializeField] TMP_InputField inputField;
                [SerializeField] TMP_Text inputFieldText;
                [SerializeField] TMP_Text inputFieldPlaceholder;
                [SerializeField] TMP_Text title;
                SubmitDelegate submitDelegate;
                bool allowEmpty;

                public Button SubmitButton { get { return submitButton; } }
                public TMP_Text SubmitButtonText { get { return submitButtonText; } }
                public Image Background { get { return background; } }
                public TMP_InputField InputField { get { return inputField; } }
                public TMP_Text InputFieldText { get { return inputFieldText; } }
                public TMP_Text InputFieldPlaceholder { get { return inputFieldPlaceholder; } }
                public TMP_Text Title { get { return title; } }

                protected override bool Initialize(params object[] args)
                {
                    bool initialized = base.Initialize(args);
                    if (initialized)
                    {
                        title.text = ValidateParameter<string>(0, nameof(title), null);
                        submitDelegate = ValidateParameter<SubmitDelegate>(1, nameof(submitDelegate), null);
                        allowEmpty = ValidateParameter(2, nameof(title), true);

                        inputField.onSubmit.AddListener(Submit);
                        submitButton.onClick.AddListener(Submit);
                    }
                    return initialized;
                }

                protected override void InitializeUIElements()
                {
                    ValidateField(nameof(submitButton), submitButton);
                    ValidateField(nameof(submitButtonText), submitButtonText);
                    ValidateField(nameof(background), background);
                    ValidateField(nameof(inputField), inputField);
                    ValidateField(nameof(inputFieldText), inputFieldText);
                    ValidateField(nameof(title), title);

                    PaletteManager.DefaultManager.ApplyBlockColorPalette(new ButtonAdapter(submitButton), new TMP_TextAdapter(submitButtonText));
                    PaletteManager.DefaultManager.ApplySurfacePalette(background, new TMP_TextAdapter(title));
                    PaletteManager.DefaultManager.ApplyBlockColorPalette(new TMP_InputFieldAdapter(inputField), PaletteManager.DefaultManager.Palette.Surface, new TMP_TextAdapter(inputFieldText), new TMP_TextAdapter(inputFieldPlaceholder));
                    inputFieldPlaceholder.color = new(
                        inputFieldPlaceholder.color.r,
                        inputFieldPlaceholder.color.g,
                        inputFieldPlaceholder.color.b,
                        inputFieldPlaceholder.color.a / 2f
                    );
                }

                public bool Initialize(string title, SubmitDelegate submitDelegate, bool allowEmpty=true)
                {
                    return Initialize((object)title, (object)submitDelegate, (object)allowEmpty);
                }

                public void Submit()
                {
                    if (!allowEmpty && inputField.text.Trim() == "")
                    {
                        PopUpsManager.DefaultManager.ShowNotification("Entrada invalida", "Por favor ingresa un texto", Notification.NotificationLevel.Warning);
                        return;
                    }
                    if (Close())
                    {
                        submitDelegate(inputField.text.Trim());
                    }
                }

                public void Submit(string text)
                {
                    Submit();
                }
            }
        }
    }
}
