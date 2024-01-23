using AOT;
using System;
using System.Runtime.InteropServices;

namespace Win32
{
    class Core
    {
        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, KeyboardHookProc callback, IntPtr hInstance, uint threadId);

        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        [DllImport("user32.dll")]
        static extern int CallNextHookEx(IntPtr idHook, int nCode, int wParam, ref KeyboardHookStruct IParam);

        [DllImport("user32.dll")]
        static extern short GetKeyState(int nCode);

        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string IpFileName);

        // callback Delegate
        public delegate int KeyboardHookProc(int code, int wParam, ref KeyboardHookStruct IParam);

        public struct KeyboardHookStruct
        {
            /// <summary>
            /// 가상 키 코드 (1 ~ 254)
            /// </summary>
            public int vkCode;
            /// <summary>
            /// 하드웨어 스캔 코드
            /// </summary>
            public int scanCode;
            /// <summary>
            /// 확장 키 플래그, 이벤트 주입 플래그, 컨텍스트 코드 및 전환 상태 플래그  
            /// </summary>
            public int flags;
            /// <summary>
            /// 타임스탬프
            /// </summary>
            public int time;
            /// <summary>
            /// 추가 정보
            /// </summary>
            public int dwExtraInfo;
        }

        // 좌우 윈도우 키
        const int VK_LWIN = 0x5B;
        const int VK_RWIN = 0x5C;

        // 이벤트 코드
        const int WH_KEYBOARD_LL = 13;
        const int WM_KEYDOWN = 0x100;
        const int WM_KEYUP = 0x101;
        const int WM_SYSKEYDOWN = 0x104;
        const int WM_SYSKEYUP = 0x105;

        // 이 이벤트가 삽입되었는가
        const int LLKHF_INJECTED = 0x00000010;

        private KeyboardHookProc khp;
        private static IntPtr hHookId = IntPtr.Zero;

        public delegate void KeyBoardEventCallback(VirtualKey key);
        public static KeyBoardEventCallback keyDownCallback;
        public static KeyBoardEventCallback keyUpCallback;

        public Core()
        {
            khp = new KeyboardHookProc(HookProc);
        }

        /// <summary>
        /// 입력 감지 시작
        /// </summary>
        public void Hook()
        {
            IntPtr hInstance = LoadLibrary("User32");
            hHookId = SetWindowsHookEx(WH_KEYBOARD_LL, khp, hInstance, 0);

#if UNITY_EDITOR
            UnityEngine.Debug.Log($"Hook:{hHookId}");
#endif
        }

        public void Unhook()
        {
            UnhookWindowsHookEx(hHookId);

#if UNITY_EDITOR
            UnityEngine.Debug.Log("Unhook");
#endif
        }

        /// <summary>
        /// 입력 처리
        /// </summary>
        [MonoPInvokeCallback(typeof(KeyboardHookProc))]
        private static int HookProc(int code, int wParam, ref KeyboardHookStruct IParam)
        {
            // code가 0보다 작으면 후크 절차는 추가 처리 없이 메시지를
            // CallNextHookEx 함수로 전달해야 하며 CallNextHookEx에서 반환된 값을 반환해야 합니다.
            // https://learn.microsoft.com/en-us/windows/win32/winmsg/keyboardproc#code-in
            if (code >= 0)
            {
                if (wParam == WM_KEYDOWN)
                {
                    VirtualKey vk = (VirtualKey)IParam.vkCode;
#if UNITY_EDITOR
                    UnityEngine.Debug.Log("WM_KEYDOWN");
#endif
                    keyDownCallback?.Invoke(vk);
                }
                if (wParam == WM_KEYUP)
                {
                    VirtualKey vk = (VirtualKey)IParam.vkCode;
#if UNITY_EDITOR
                    UnityEngine.Debug.Log("WM_KEYUP");
#endif
                    keyUpCallback?.Invoke(vk);
                }
            }

            return CallNextHookEx(hHookId, code, wParam, ref IParam);
        }
    }
}
