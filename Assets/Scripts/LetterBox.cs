using UnityEngine;

public class LetterBox : MonoBehaviour
{
    private Vector2 resolution;
    private float bucket;

    private void Awake()
    {
        resolution = new Vector2(Screen.width, Screen.height);
        bucket = 0;
    }

    private void Start()
    {
        CalcLetterBox();
    }

    private void CalcLetterBox()
    {
        Camera camera = GetComponent<Camera>();

        //Rect rect = camera.rect;
        Rect rect = new(0, 0, 1, 1);

        float scaleheight = (float)Screen.width / Screen.height / ((float)16 / 9); // (가로 / 세로)
        float scalewidth = 1f / scaleheight;

        if (scaleheight < 1)
        {
            rect.height = scaleheight;
            rect.y = (1f - scaleheight) / 2f;
        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth) / 2f;
        }

        camera.rect = rect;
    }

    private void OnPreCull()
    {
        GL.Clear(true, true, Color.black);
    }

    private void Update()
    {
        if (bucket >= 0.1f)
        {
            bucket = 0;
        }
        else
        {
            bucket += Time.deltaTime;
            return;
        }

        if (resolution.x != Screen.width || resolution.y != Screen.height)
        {
            print("[LetterBox] 화면 크기가 변경되었습니다.");
            CalcLetterBox();

            resolution.x = Screen.width;
            resolution.y = Screen.height;
        }
    }
}
