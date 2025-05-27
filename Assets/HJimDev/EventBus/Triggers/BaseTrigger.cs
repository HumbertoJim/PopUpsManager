using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EventBus
{
    namespace Triggers
    {
        public class BaseTrigger : MonoBehaviour
        {
            [SerializeField] protected EventManager eventManager;
            [SerializeField] protected string topic;
            protected bool triggerEnabled = true;

            virtual public void Trigger(Dictionary<string, object> kwargs)
            {
                if (triggerEnabled)
                {
                    triggerEnabled = false;
                    eventManager.Publish(topic, kwargs);
                }
            }
        }
    }
}
