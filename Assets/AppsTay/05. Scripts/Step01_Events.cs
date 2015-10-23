using UnityEngine;
using System.Collections;

public class Step01_Events : MonoBehaviour
{
    /// <summary>
    /// 개발자용 디버그 출력 모드를 설정 합니다.
    /// </summary>
    public bool 디버그출력모드 = true;

    public GameObject Setp01_Main;
    public GameObject Setp01_Help;
    public GameObject Setp01_Simulation;
    public GameObject Setp01_Popup;

    public GameObject Back01;
    public GameObject Back01_01;

    public GameObject 시뮬레이션버튼;
    public GameObject Btn_ScreenShoot;

    public GameObject 오토버튼;
    public GameObject 수동버튼;

    public AudioSource mp3;

    public UILabel 목표각도라벨;
    public UILabel 목표각도라벨2;
    public UILabel 사진저장라벨;

    public bool 오토모드 = false;
    public int 사진저장개수 = 27;
    public bool 회전각도체크 = false;

    public static Step01_Events step01;

    void Awake()
    {
        step01 = this;
    }

    void Start()
    {
        오토모드_버튼();
        Step01_메인화면();
        AngleChecking.angle.상대각도계산();

        //DownloadModeling.modeling.모델링출력();
    }

    void Update()
    {

    }

    private void Step01_모든화면닫기()
    {
        시뮬레이션버튼.GetComponent<UIButton>().enabled = false;

        Setp01_Main.SetActive(false);
        Setp01_Help.SetActive(false);
        Setp01_Simulation.SetActive(false);
        Setp01_Popup.SetActive(false);
    }

    public void 오토모드_버튼()
    {
        MobileCamera.cam.목표각도출력();
        AngleChecking.angle.상대각도계산사용안함();

        Btn_ScreenShoot.GetComponent<UIButton>().enabled = true;
        Btn_ScreenShoot.GetComponent<UIButton>().defaultColor = Color.white;

        오토버튼.SetActive(false);
        수동버튼.SetActive(true);
        오토모드 = false;
    }

    public void 수동모드_버튼()
    {
        MobileCamera.cam.목표각도출력();
        AngleChecking.angle.상대각도계산();

        Btn_ScreenShoot.GetComponent<UIButton>().enabled = false;
        Btn_ScreenShoot.GetComponent<UIButton>().defaultColor = Color.red;

        // 오토지만 최초 수동1번 촬영을 요구할시..
        //if (MobileCamera.cam.스크린샷이미지.Count >= 1)
        //{
        //    Btn_ScreenShoot.GetComponent<UIButton>().enabled = false;
        //    Btn_ScreenShoot.GetComponent<UIButton>().defaultColor = Color.red;
        //}

        오토버튼.SetActive(true);
        수동버튼.SetActive(false);
        오토모드 = true;
    }

    public void Step01_메인화면()
    {
        Step01_모든화면닫기();

        Setp01_Main.SetActive(true);       

        //Invoke("Step01_시뮬레이션버튼", 1);
        Step01_시뮬레이션버튼();
    }

    private void Step01_시뮬레이션버튼()
    {        
        시뮬레이션버튼.GetComponent<UIButton>().enabled = true;
    }

    public void Step01_촬영모드()
    {
        Step01_모든화면닫기();

        Setp01_Simulation.SetActive(true);

        MobileCamera.cam.MobileCam_Play();
    }

    public void Step01_도움말()
    {
        Back01_01.SetActive(false);
        Back01.SetActive(true);

        Setp01_Main.SetActive(false);
        Setp01_Simulation.SetActive(false);

        Setp01_Help.SetActive(true);
    }

    public void Step01_시뮬레이션도움말()
    {
        Back01.SetActive(false);
        Back01_01.SetActive(true);

        Setp01_Help.SetActive(true);
    }

    public void Step01_뒤로가기()
    {
        Setp01_Help.SetActive(false);
    }

    public void Step02_메인화면()
    {
        MobileCamera.cam.MobileCam_Stop();

        Step01_모든화면닫기();

        Step02_Events.step02.Step02_메인화면();

        Step02_Events.step02.앨범로드();
    }

    public void Step01_스크린샷저장()
    {
        if (MobileCamera.cam.스크린샷이미지.Count < 사진저장개수)
        {
            if (MobileCamera.cam.스크린샷이미지.Count <= 0)
            {
                if (오토모드)
                {
                    Btn_ScreenShoot.GetComponent<UIButton>().enabled = false;
                    Btn_ScreenShoot.GetComponent<UIButton>().defaultColor = Color.red;
                }

                AngleChecking.angle.상대각도계산();
            }

            Btn_ScreenShoot.GetComponent<BoxCollider2D>().enabled = false;

            StartCoroutine(MobileCamera.cam.TakeSnapshot());

            mp3.Play();

            Invoke("팝업딜레이", 0.2f);
            Invoke("스크린샷버튼딜레이", 0.3f);
        }
        else
        {
            DebugShow("더 이상 사진(이미지)을를 저장할 수 없습니다.");
            return;
        } 
    }

    public void Step01_스크린샷이미지초기화()
    {
        Setp01_Popup.SetActive(false);

        MobileCamera.cam.스크린샷이미지초기화();
        DebugShow("모든 스크린샷 이미지가 초기화 되었습니다.");
    }

	public void GotoGallery()
	{
		Step01_모든화면닫기();
		Step_Gallery.Init();
	}

	private void 스크린샷버튼딜레이()
    {
        Btn_ScreenShoot.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void 팝업딜레이()
    {
        if (MobileCamera.cam.스크린샷이미지.Count == 사진저장개수)
        {
            Setp01_Popup.SetActive(true);
        }
    }

    /// <summary>
    /// 디버그 모드로 출력
    /// </summary>
    private void DebugShow(string debug)
    {
        if (디버그출력모드)
        {
            Debug.Log(debug);
        }
    }
}
