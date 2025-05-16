using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

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

                    ColorBlock colors;

                    colors = submitButton.colors;
                    colors.normalColor = PopUpsManager.DefaultManager.Palette.Secondary.Color;
                    colors.highlightedColor = PopUpsManager.DefaultManager.Palette.Secondary.Lighten2.Color;
                    colors.pressedColor = PopUpsManager.DefaultManager.Palette.Secondary.Darken2.Color;
                    colors.selectedColor = PopUpsManager.DefaultManager.Palette.Secondary.Color;
                    colors.disabledColor = PopUpsManager.DefaultManager.Palette.Secondary.Darken1.Color;
                    submitButton.colors = colors;

                    submitButtonText.color = PopUpsManager.DefaultManager.Palette.Secondary.TextColor;

                    background.color = PopUpsManager.DefaultManager.Palette.Surface.Color;

                    colors = inputField.colors;
                    colors.normalColor = PopUpsManager.DefaultManager.Palette.Surface.Color;
                    colors.highlightedColor = PopUpsManager.DefaultManager.Palette.Surface.Lighten2.Color;
                    colors.pressedColor = PopUpsManager.DefaultManager.Palette.Surface.Darken2.Color;
                    colors.selectedColor = PopUpsManager.DefaultManager.Palette.Surface.Color;
                    colors.disabledColor = PopUpsManager.DefaultManager.Palette.Surface.Darken1.Color;
                    inputField.colors = colors;

                    inputFieldText.color = PopUpsManager.DefaultManager.Palette.Surface.TextColor;

                    inputFieldPlaceholder.color = new(
                        PopUpsManager.DefaultManager.Palette.Surface.TextColor.r,
                        PopUpsManager.DefaultManager.Palette.Surface.TextColor.g,
                        PopUpsManager.DefaultManager.Palette.Surface.TextColor.b,
                        PopUpsManager.DefaultManager.Palette.Surface.TextColor.a / 2f
                    );

                    title.color = PopUpsManager.DefaultManager.Palette.Surface.TextColor;
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
