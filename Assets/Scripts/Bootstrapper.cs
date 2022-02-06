using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Bootstrapper : MonoBehaviour
    {
        private App _app;

        public void Start()
        {
            DontDestroyOnLoad(this);
            _app = new App();
        }
    }
}