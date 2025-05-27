using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI.PopUps.Alert;
using UI.PopUps.Message;
using UI.PopUps.Input;
using UI.PopUps.Select;
using UI.PopUps.Confirm;
using UI.PopUps.Notification;
using UI.Palettes;
using EventBus;


namespace UI
{
    namespace PopUps
    {
        public abstract class BasePopUp: MonoBehaviour
        {
            protected object[] args;
            protected bool initialized;
            private bool closed;

            protected virtual bool Initialize(params object[] args)
            {
                if (!initialized)
                {
                    initialized = true;
                    EventManager.DefaultManager.Subscribe(Palettes.Events.Constants.ChangePalette, gameObject, (message) => { InitializeUIElements(); });
                    this.args = args;
                    closed = false;
                    InitializeUIElements();
                    return true;
                }
                return false;
            }

            protected virtual void InitializeUIElements() { }

            protected virtual bool Close()
            {
                if (!closed)
                {
                    closed = true;
                    EventManager.DefaultManager.Unsubscribe(Palettes.Events.Constants.ChangePalette, gameObject);
                    Destroy(gameObject);
                    return true;
                }
                return false;
            }

            protected void ValidateField<T>(string name, T value)
            {
                if (value == null)
                {
                    throw new InvalidOperationException($"The {typeof(T).Name} {name} field is mandatory and must be assigned.");
                }
            }

            protected T ValidateParameter<T>(int index, string name, T defaultValue)
            {
                if (args.Length > index)
                {
                    if (args[index] is T parsed)
                    {
                        return parsed;
                    }
                    else
                    {
                        throw new ArgumentException($"{name} parameter must be of type {typeof(T).Name}.");
                    }
                }
                else if (defaultValue == null)
                {
                    throw new ArgumentException($"{name} parameter is mandatory.");
                }
                else
                {
                    return defaultValue;
                }
            }
        }

        public class PopUpsManager : MonoBehaviour
        {
            private static PopUpsManager manager;
            private Canvas canvas;
            Dictionary<string, GameObject> prefabs;

            public static PopUpsManager DefaultManager
            {
                get
                {
                    if (!manager)
                    {
                        GameObject instance = new("PopUpsManager");
                        manager = instance.AddComponent<PopUpsManager>();
                        manager.prefabs = new();
                        DontDestroyOnLoad(manager);
                    }
                    return manager;
                }
            }

            private Canvas GetCanvas()
            {
                if (canvas == null) canvas = GameObject.FindFirstObjectByType<Canvas>();
                if (canvas == null) throw new Exceptions.PopUpException("Unable to find a canvas");
                return canvas;
            }

            private GameObject LoadPopUpPrefab(string name)
            {
                if (!prefabs.ContainsKey(name))
                {
                    GameObject prefab = Resources.Load<GameObject>($"UI/PopUps/{name}");
                    if (prefab == null) throw new Exceptions.PopUpException($"Unable to find the {name} prefab");
                    prefabs.Add(name, prefab);
                }
                return prefabs[name];
            }

            private T LoadPopUp<T>(string name) where T : BasePopUp
            {
                GameObject prefab = LoadPopUpPrefab(name);
                GameObject obj = Instantiate(prefab, GetCanvas().transform);
                T component = obj.GetComponent<T>();
                if (component == null) throw new Exceptions.PopUpException($"Unable to get the {typeof(T).Name} component from the {name} prefab");
                return component;
            }

            public AlertPopUp ShowAlert(string message, Alert.ConfirmDelegate confirmDelegate)
            {
                AlertPopUp popup = LoadPopUp<AlertPopUp>("Alert");
                popup.Initialize(message, confirmDelegate);
                return popup;
            }

            public MessagePopUp ShowMessage(string title, string message, Alert.ConfirmDelegate confirmDelegate)
            {
                MessagePopUp popup = LoadPopUp<MessagePopUp>("Message");
                popup.Initialize(title, message, confirmDelegate);
                return popup;
            }

            public InputPopUp ShowInput(string title, SubmitDelegate submitDelegate, bool allowEmpty = true)
            {
                InputPopUp popup = LoadPopUp<InputPopUp>("Input");
                popup.Initialize(title, submitDelegate, allowEmpty);
                return popup;
            }

            public SelectPopUp ShowSelect(string title, List<Option> options, SelectDelegate selectDelegate)
            {
                SelectPopUp popup = LoadPopUp<SelectPopUp>("Select");
                popup.Initialize(title, options, selectDelegate);
                return popup;
            }

            public ConfirmPopUp ShowConfirm(string message, Confirm.ConfirmDelegate confirmDelegate)
            {
                ConfirmPopUp popup = LoadPopUp<ConfirmPopUp>("Confirm");
                popup.Initialize(message, confirmDelegate);
                return popup;
            }

            public NotificationPopUp ShowNotification(string title, string message, NotificationLevel level = NotificationLevel.Info, float lifeTime = 5)
            {
                NotificationPopUp popup = LoadPopUp<NotificationPopUp>("Notification");
                popup.Initialize(title, message, level, lifeTime);
                return popup;
            }
        }
    }
}
