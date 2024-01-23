using System;
using TMPro;
using UnityEngine;

public class Director : MonoBehaviour
{
    [SerializeField]
    private TMP_Text unityDisplay;

    [SerializeField]
    private TMP_Text win32Display;

    [SerializeField]
    private TMP_Text resultDisplay;

    [SerializeField]
    private TMP_Text highDisplay;

    [SerializeField]
    private TMP_Text lowDisplay;

    public static long UnityPressed = 0;

    public static long Win32Pressed = 0;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            UnityPressed = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        if (UnityPressed != 0)
        {
            unityDisplay.text = UnityPressed.ToString();
            win32Display.text = Win32Pressed.ToString();

            var result = Win32Pressed - UnityPressed;

            if (result > 0) 
            {
                resultDisplay.text = $"유니티가 {result}ms만큼 더 빨리 반응했습니다.";
            }
            else
            {
                resultDisplay.text = $"유니티가 {-result}ms만큼 더 느리게 반응했습니다.";
            }
        }
    }

    public void SetFrameRateUnlimited()
    {
        Application.targetFrameRate = -1;
    }
    
    public void SetFrameRate30()
    {
        Application.targetFrameRate = 30;
    }

    public void SetFrameRate60()
    {
        Application.targetFrameRate = 60;
    }

    public void SetFrameRate120()
    {
        Application.targetFrameRate = 120;
    }
}
