using UnityEngine;
using System;

namespace Win32
{
    public class InputDetector : MonoBehaviour
    {
        private CoreWrapper cw;
        private bool flag;

        void Start()
        {
            Core.keyDownCallback = OnKeyDown;
            Core.keyUpCallback = OnKeyUp;

            flag = true;

            cw = GetComponent<CoreWrapper>();
            cw.core.Hook();
        }

        private void OnApplicationQuit()
        {
            cw.core.Unhook();
        }

        private void OnDestroy()
        {
            cw.core.Unhook();
        }

        private void OnKeyDown(VirtualKey key)
        {
            if (flag && key == VirtualKey.K_A)
            {
                Director.Win32Pressed = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                flag = false;
            }
        }

        private void OnKeyUp(VirtualKey key)
        {
            flag = true;
        }
    }
}
