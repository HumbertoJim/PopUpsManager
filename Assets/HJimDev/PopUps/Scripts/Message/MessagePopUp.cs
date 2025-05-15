using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UI.PopUps.Alert;

namespace UI
{
    namespace PopUps
    {
        namespace Message
        {
            public class MessagePopUp : AlertPopUp
            {
                [SerializeField] TMP_Text title;

                public TMP_Text Title { get { return title; } }

                protected override bool Initialize(params object[] args)
                {
                    ValidateField(nameof(title), title);

                    bool initialized = base.Initialize(args);
                    if (initialized)
                    {
                        title.text = ValidateParameter(2, nameof(title), "");
                    }
                    return initialized;
                }

                public bool Initialize(string title, string message, ConfirmDelegate confirmDelegate)
                {
                    return Initialize((object)message, (object)confirmDelegate, (object) title);
                }
            }
        }
    }
}
