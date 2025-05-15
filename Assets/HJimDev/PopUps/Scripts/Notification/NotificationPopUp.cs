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
        namespace Notification
        {
            public enum Level { Info, Success, Warning, Error }

            public class NotificationPopUp : BasePopUp
            {
                [SerializeField] Image background;
                [SerializeField] TMP_Text title;
                [SerializeField] TMP_Text message;
                [SerializeField] float defaultLifeTime = 5;
                float lifeTime;
                Level level;

                public TMP_Text Title { get { return title; } }
                public TMP_Text Message { get { return message; } }
                public Image Background { get { return background; } }

                protected override bool Initialize(params object[] args)
                {
                    ValidateField(nameof(title), title);
                    ValidateField(nameof(message), message);
                    ValidateField(nameof(background), background);

                    bool initialized = base.Initialize(args);
                    if (initialized)
                    {
                        title.text = ValidateParameter<string>(0, nameof(title), null);
                        message.text = ValidateParameter<string>(1, nameof(message), null);
                        level = ValidateParameter(2, nameof(level), Level.Info);
                        lifeTime = ValidateParameter(3, nameof(lifeTime), defaultLifeTime);

                        background.color = level switch
                        {
                            Level.Success => new Color32(40, 167, 69, 255),
                            Level.Warning => new Color32(255, 193, 7, 255),
                            Level.Error => new Color32(220, 53, 69, 255),
                            _ => new Color32(0, 123, 255, 255),
                        };

                        Destroy(gameObject, lifeTime);
                    }
                    return initialized;
                }

                public bool Initialize(string title, string message, Level level=Level.Info, float lifeTime=5)
                {
                    return Initialize((object)title, (object)message, (object)level, (object)lifeTime);
                }
            }
        }
    }
}
