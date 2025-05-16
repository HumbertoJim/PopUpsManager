using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    namespace PopUps
    {
        namespace Select
        {
            public class Option
            {
                public readonly int id;
                public readonly string text;

                public Option(int id, string text)
                {
                    this.id = id;
                    this.text = text;
                }
            }

            public class OptionController : MonoBehaviour
            {
                [SerializeField] Toggle toggle;
                [SerializeField] TMP_Text label;
                bool isFixed;

                public Option Option { get; private set; }
                public Toggle Toggle { get { return toggle; } }
                public TMP_Text Label { get { return label; } }

                public void SetInformation(Option option, ToggleGroup group)
                {
                    if (!isFixed)
                    {
                        isFixed = true;
                        Option = option;
                        toggle.group = group;
                        label.text = option.text;
                    }
                }

                public void Select()
                {
                    toggle.isOn = true;
                }
            }
        }
    }
}
