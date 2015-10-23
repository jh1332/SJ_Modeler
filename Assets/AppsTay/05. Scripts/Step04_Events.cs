using UnityEngine;
using System.Collections;
using System.Text;

public class Step04_Events : MonoBehaviour
{
    public GameObject Setp04_Main;
    public GameObject Setp04_Help;
    public GameObject Setp04_View3DModeling;

    public static Step04_Events step04;


    void Awake()
    {
        step04 = this;
    }

    void Start()
    {
        Step04_모든화면닫기();
    }

    void Update()
    {

    }

    public void Step04_모든화면닫기()
    {
        Setp04_Main.SetActive(false);
        Setp04_Help.SetActive(false);
        Setp04_View3DModeling.SetActive(false);
    }

    public void Step04_메인화면()
    {
        Step04_모든화면닫기();

        Setp04_Main.SetActive(true);
    }

    public void Step04_도움말()
    {
        Step04_모든화면닫기();

        Setp04_Help.SetActive(true);
    }

    public void Step04_모델링보기화면()
    {
        Step04_모든화면닫기();

        Setp04_View3DModeling.SetActive(true);

        WebViewer.web.웹뷰모델링로드();
        //웹뷰모델링로드();
    }

    public void Step05_페이지로이동()
    {
        Step04_모든화면닫기();

        WebViewer.web.webView.Hide();

        Step05_Events.step05.Step05_메인화면();
    }
}
