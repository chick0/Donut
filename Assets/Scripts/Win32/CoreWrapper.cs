using UnityEngine;

namespace Win32
{
    class CoreWrapper : MonoBehaviour
    {
        public Core core;

        private void Awake()
        {
            core = new Core();
        }

        private void OnApplicationQuit()
        {
            print("Unhook from wrapper");
            core.Unhook();
        }
    }
}
