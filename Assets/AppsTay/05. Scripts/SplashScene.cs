using UnityEngine;
using System.Collections;

public class SplashScene : MonoBehaviour
{
    public float splashTime = 1f;
    public string sceneName = "02. Main";
    public GameObject slider;

    public bool isSlider = false;
    public float aniTime = 0;
    public float fullTime = 2f;
    public float addTime = 0.5f;

    void Awake()
    {
        slider.GetComponent<UISlider>().value = 0;
    }

    void Start()
    {
        Invoke("메인화면이동", splashTime);
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (isSlider)
        {
            aniTime += Time.deltaTime;
            slider.GetComponent<UISlider>().value = (aniTime * addTime);

            if (aniTime >= fullTime)
            {
                slider.GetComponent<UISlider>().value = 1;

                Application.LoadLevelAsync(sceneName);
            }
        }
    }

    public void 메인화면이동()
    {
        isSlider = true;
    }
}
