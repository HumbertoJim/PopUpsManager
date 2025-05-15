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
                [SerializeField] Image background;
                [SerializeField] TMP_Text message;
                ConfirmDelegate confirmDelegate;

                public TMP_Text Message { get { return message; } }
                public Image Background { get { return background; } }

                protected override bool Initialize(params object[] args)
                {
                    ValidateField(nameof(message), message);
                    ValidateField(nameof(background), background);
                    ValidateField(nameof(confirmButton), confirmButton);

                    bool initialized = base.Initialize(args);
                    if (initialized)
                    {
                        message.text = ValidateParameter<string>(0, nameof(message), null);
                        confirmDelegate = ValidateParameter<ConfirmDelegate>(1, nameof(confirmDelegate), null);

                        confirmButton.onClick.AddListener(Confirm);
                    }
                    return initialized;
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
