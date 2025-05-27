using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventBus
{
    public delegate void MessageDelegate(Dictionary<string, object> message);

    public struct Subscription
    {
        public readonly GameObject owner;
        public readonly MessageDelegate action;

        public Subscription(GameObject owner, MessageDelegate action)
        {
            this.owner = owner;
            this.action = action;
        }
    }

    public class EventManager : MonoBehaviour
    {
        private static EventManager manager;
        Dictionary<string, List<Subscription>> topics;
        bool initialized;

        public static EventManager DefaultManager
        {
            get
            {
                if (!manager)
                {
                    GameObject instance = new("EventManager");
                    DefaultManager = instance.AddComponent<EventManager>();
                }
                return manager;
            }
            private set
            {
                manager = value;
                manager.Initialize();
            }
        }

        private void Awake()
        {
            if (manager && manager != this)
            {
                DestroyImmediate(gameObject);
            }
            else if (!manager)
            {
                DefaultManager = this;
            }
        }

        void Initialize()
        {
            if (!initialized)
            {
                initialized = true;
                manager.topics = new();
                DontDestroyOnLoad(manager.gameObject);
            }
        }

        public void Subscribe(string topic, Subscription subscription)
        {
            if (!topics.ContainsKey(topic)) topics.Add(topic, new());
            topics[topic].Add(subscription);
        }

        public void Subscribe(string topic, GameObject owner, MessageDelegate action)
        {
            Subscribe(topic, new Subscription(owner, action));
        }

        public void Publish(string topic, Dictionary<string, object> message = null)
        {
            if (!topics.TryGetValue(topic, out var subscriptions)) return;
            if (message == null) message = new();
            for(int i=subscriptions.Count-1; i>=0; i--)
            {
                if(subscriptions[i].owner == null || subscriptions[i].action == null)
                {
                    subscriptions.RemoveAt(i);
                }
                else
                {
                    subscriptions[i].action?.Invoke(message);
                }
            }
        }

        public void Unsubscribe(string topic, GameObject owner, MessageDelegate action)
        {
            if (!topics.TryGetValue(topic, out var subscriptions)) return;
            for (int i = subscriptions.Count - 1; i >= 0; i--)
            {
                if (subscriptions[i].owner == owner && subscriptions[i].action == action)
                {
                    subscriptions.RemoveAt(i);
                }
            }
        }

        public void Unsubscribe(string topic, GameObject owner)
        {
            if (!topics.TryGetValue(topic, out var subscriptions)) return;
            for (int i = subscriptions.Count - 1; i >= 0; i--)
            {
                if (subscriptions[i].owner == owner)
                {
                    subscriptions.RemoveAt(i);
                }
            }
        }
    }
}
