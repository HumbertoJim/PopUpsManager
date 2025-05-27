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
        namespace Select
        {
            public delegate void SelectDelegate(Option option);

            public class SelectPopUp : BasePopUp
            {
                [SerializeField] Button selectButton;
                [SerializeField] TMP_Text selectButtonText;
                [SerializeField] Image background;
                [SerializeField] TMP_Text title;
                [SerializeField] RectTransform optionsContainer;
                [SerializeField] ToggleGroup toggleGroup;
                [SerializeField] GameObject optionPrefab;
                SelectDelegate selectDelegate;

                public Button SelectButton { get { return selectButton; } }
                public TMP_Text SelectButtonText { get { return selectButtonText; } }
                public Image Background { get { return background; } }
                public TMP_Text Title { get { return title; } }
                public List<OptionController> Options { get; private set; }

                protected override bool Initialize(params object[] args)
                {
                    bool initialized = base.Initialize(args);
                    if (initialized)
                    {
                        title.text = ValidateParameter<string>(0, nameof(title), null);
                        List<Option> options = ValidateParameter<List<Option>>(1, nameof(Options), null);
                        selectDelegate = ValidateParameter<SelectDelegate>(2, nameof(selectDelegate), null);

                        Options = new();
                        foreach (Option option in options)
                        {
                            GameObject inst = Instantiate(optionPrefab, optionsContainer);
                            OptionController opt = inst.GetComponent<OptionController>();
                            opt.SetInformation(option, toggleGroup);
                            Options.Add(opt);
                        }
                        optionsContainer.sizeDelta = new(
                            optionsContainer.sizeDelta.x,
                            Options.Count > 4 ? 40 + 60 * Options.Count + 20 * (Options.Count - 1) : 350
                        );
                        if (Options.Count > 0) Options[0].Select();

                        selectButton.onClick.AddListener(Select);
                    }
                    return initialized;
                }

                protected override void InitializeUIElements()
                {
                    ValidateField(nameof(selectButton), selectButton);
                    ValidateField(nameof(selectButtonText), selectButtonText);
                    ValidateField(nameof(background), background);
                    ValidateField(nameof(title), title);
                    ValidateField(nameof(optionsContainer), optionsContainer);
                    ValidateField(nameof(toggleGroup), toggleGroup);
                    ValidateField(nameof(optionPrefab), optionPrefab);

                    OptionController opt = optionPrefab.GetComponent<OptionController>();

                    PaletteManager.DefaultManager.ApplyBlockColorPalette(new ButtonAdapter(selectButton), new TMP_TextAdapter(selectButtonText));
                    PaletteManager.DefaultManager.ApplyBlockColorPalette(new ToggleAdapter(opt.Toggle), PaletteManager.DefaultManager.Palette.Secondary.Lighten1, new TMP_TextAdapter(opt.Label));
                    PaletteManager.DefaultManager.ApplySurfacePalette(background, new TMP_TextAdapter(title));
                }

                public bool Initialize(string title, List<Option> options, SelectDelegate selectDelegate)
                {
                    return Initialize((object)title, (object)options, (object)selectDelegate);
                }

                public void Select()
                {
                    if (Close())
                    {
                        foreach (OptionController option in Options)
                        {
                            if (option.Toggle.isOn)
                            {
                                selectDelegate(option.Option);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
