using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
                [SerializeField] Button disagreeButton;
                [SerializeField] Image background;
                [SerializeField] TMP_Text message;
                ConfirmDelegate confirmDelegate;

                public Image Background { get { return background; } }
                public TMP_Text Message { get { return message; } }

                protected override bool Initialize(params object[] args)
                {
                    ValidateField(nameof(message), message);
                    ValidateField(nameof(background), background);
                    ValidateField(nameof(agreeButton), agreeButton);
                    ValidateField(nameof(disagreeButton), disagreeButton);

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
