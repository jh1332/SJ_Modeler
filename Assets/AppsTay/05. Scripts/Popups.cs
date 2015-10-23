using UnityEngine;
using System.Collections;

public class Popups : MonoBehaviour
{
    public GameObject 프로그램종료팝업;
    public GameObject 다운로드오류팝업;

    public static Popups popup;

    void Awake()
    {
        popup = this;
    }

    void Start()
    {
        프로그램종료팝업.SetActive(false);
        다운로드오류팝업.SetActive(false);
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        //안드로이드: 뒤로가기 버튼을 눌렀을경우
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            프로그램종료팝업.SetActive(true);
        }
    }

    public void 종료팝업_확인()
    {
        프로그램종료팝업.SetActive(false);
        Application.Quit();
    }

    public void 종료팝업_취소()
    {
        프로그램종료팝업.SetActive(false);
    }

    public void 다운로드팝업_오류()
    {
        다운로드오류팝업.SetActive(true);
    }

    private void 다운로드팝업_취소()
    {
        다운로드오류팝업.SetActive(false);
    }

    public void 다운로드팝업오류_확인()
    {
        MobileCamera.cam.스크린샷이미지초기화();

        //Step04_Events.step04.Step04_모든화면닫기();
        Step03_Events.step03.Step03_모든화면닫기();
        Step03_Events.step03.초기화();

        Step01_Events.step01.Step01_메인화면();

        다운로드오류팝업.SetActive(false);
    }
}
