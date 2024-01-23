using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClapPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private AudioClip clip;

    [SerializeField]
    private TMP_Text realInterval;

    [SerializeField]
    private Image donut;

    private float interval = 1f;

    public float Bucket { get { return bucket; } }

    private float bucket;

    public long HitTime {  get; private set; }

    private void Start()
    {
        bucket = 0;
        HitTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }

    void Update()
    {
        if (bucket >= interval)
        {
            bucket = 0;
            source.PlayOneShot(clip);

            var before = HitTime;
            HitTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            realInterval.text = (HitTime - before).ToString();
        }

        bucket += Time.deltaTime;
        donut.fillAmount = bucket / interval;
    }
}
