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
                [SerializeField] Image background;
                [SerializeField] TMP_InputField inputField;
                [SerializeField] TMP_Text title;
                SubmitDelegate submitDelegate;
                bool allowEmpty;

                public TMP_Text Title { get { return title; } }
                public Image Background { get { return background; } }

                protected override bool Initialize(params object[] args)
                {
                    ValidateField(nameof(title), title);
                    ValidateField(nameof(background), background);
                    ValidateField(nameof(inputField), inputField);
                    ValidateField(nameof(submitButton), submitButton);

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

                public bool Initialize(string title, SubmitDelegate submitDelegate, bool allowEmpty=true)
                {
                    return Initialize((object)title, (object)submitDelegate, (object)allowEmpty);
                }

                public void Submit()
                {
                    if (!allowEmpty && inputField.text.Trim() == "")
                    {
                        PopUpsManager.DefaultManager.ShowNotification("Entrada invalida", "Por favor ingresa un texto", Notification.Level.Warning);
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
