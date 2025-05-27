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
        namespace Notification
        {
            public enum NotificationLevel { Info, Success, Warning, Error }

            public class NotificationPopUp : BasePopUp
            {
                [SerializeField] Image background;
                [SerializeField] TMP_Text title;
                [SerializeField] TMP_Text message;
                [SerializeField] float defaultLifeTime = 5;
                float lifeTime;
                NotificationLevel level;

                public TMP_Text Title { get { return title; } }
                public TMP_Text Message { get { return message; } }
                public Image Background { get { return background; } }
                public NotificationLevel Level { get { return level; } }

                protected override bool Initialize(params object[] args)
                {
                    bool initialized = base.Initialize(args);
                    if (initialized)
                    {
                        title.text = ValidateParameter<string>(0, nameof(title), null);
                        message.text = ValidateParameter<string>(1, nameof(message), null);
                        level = ValidateParameter(2, nameof(level), NotificationLevel.Info);
                        lifeTime = ValidateParameter(3, nameof(lifeTime), defaultLifeTime);

                        Destroy(gameObject, lifeTime);
                    }
                    return initialized;
                }

                protected override void InitializeUIElements()
                {
                    ValidateField(nameof(title), title);
                    ValidateField(nameof(message), message);
                    ValidateField(nameof(background), background);

                    level = ValidateParameter(2, nameof(level), NotificationLevel.Info);

                    PaletteColor paletteColor = level switch
                    {
                        NotificationLevel.Info => PaletteManager.DefaultManager.Palette.Secondary.Darken3,
                        NotificationLevel.Success => PaletteManager.DefaultManager.Palette.Secondary,
                        NotificationLevel.Warning => PaletteManager.DefaultManager.Palette.Error.Lighten3,
                        NotificationLevel.Error => PaletteManager.DefaultManager.Palette.Error,
                        _ => PaletteManager.DefaultManager.Palette.Secondary.Darken3
                    };
                    PaletteManager.DefaultManager.ApplySurfacePalette(background, paletteColor, new TMP_TextAdapter(title), new TMP_TextAdapter(message));
                }

                public bool Initialize(string title, string message, NotificationLevel level = NotificationLevel.Info, float lifeTime=5)
                {
                    return Initialize((object)title, (object)message, (object)level, (object)lifeTime);
                }
            }
        }
    }
}
