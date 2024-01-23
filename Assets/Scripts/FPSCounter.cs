using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField]
    private TMP_Text display;

    [SerializeField]
    private float interval = 0.5f;

    float accum = 0.0f;
    int frames = 0;
    float timeleft;
    float fps;
    
    void Start()
    {
        timeleft = interval;
    }

    void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;

        ++frames;

        if (timeleft <= 0.0)
        {
            fps = (accum / frames);
            timeleft = interval;
            accum = 0.0f;
            frames = 0;
        }

        display.text = fps.ToString("#") + "fps";
    }
}
