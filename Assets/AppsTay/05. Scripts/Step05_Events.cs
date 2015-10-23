using UnityEngine;
using System.Collections;

public class Step05_Events : MonoBehaviour
{
    public GameObject Setp05_Main;
    public GameObject Setp05_Help;
    public GameObject Setp05_View3DModeling;

    public static Step05_Events step05;

    void Awake()
    {
        step05 = this;
    }

    void Start()
    {
        Step05_모든화면닫기();
    }

    void Update()
    {

    }

    private void Step05_모든화면닫기()
    {
        Setp05_Main.SetActive(false);
        Setp05_Help.SetActive(false);
        Setp05_View3DModeling.SetActive(false);
    }

    public void Step05_메인화면()
    {
        Step05_모든화면닫기();

        Setp05_Main.SetActive(true);
    }

    public void Step05_도움말()
    {
        Step05_모든화면닫기();

        Setp05_Help.SetActive(true);
    }

    public void Step05_디바이스저장및업로드()
    {
        Step05_모든화면닫기();

        WebViewer.web.웹뷰모델링_리스트로드();

        Setp05_View3DModeling.SetActive(true);
    }

    public void Step05_초기화후메인화면()
    {
        Step05_모든화면닫기();

        MobileCamera.cam.스크린샷이미지초기화();

        WebViewer.web.webView.Hide();

        Step01_Events.step01.Step01_메인화면();
    }
}
