using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EventBus
{
    public interface IEvenListener
    {
        void SetupEventListener();
        void RemoveEventListener();
    }
}
