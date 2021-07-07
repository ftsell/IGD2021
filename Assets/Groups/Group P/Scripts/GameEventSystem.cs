using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GroupP
{
    public class GameEventSystem : MonoBehaviour
    {
        public static GameEventSystem current;
        public event Action onHit;
        public event Action onBeat;
        public event Action onStartDance;
        public event Action onStopDance;

        private void Awake()
        {
            current = this;
        }

        public void Hit()
        {
            onHit?.Invoke();
        }

        public void Beat()
        {
            onBeat?.Invoke();
        }
        public void StartDance()
        {
            onStartDance?.Invoke();
        }

        public void StopDance()
        {
            onStopDance?.Invoke();
        }
    }

}

