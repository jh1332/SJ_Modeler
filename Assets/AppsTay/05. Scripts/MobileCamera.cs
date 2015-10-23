using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// PC 웹캠 및 안드로이드 카메라 클래스
/// </summary>
public class MobileCamera : MonoBehaviour
{
    /// <summary>
    /// 개발자용 디버그 출력 모드를 설정 합니다.
    /// </summary>
    public bool 디버그출력모드 = true;

    // 캠 기본정보
    private int 캠넓이 = 1280; // 1280
    private int 캠높이 = 720; // 720
    private int 프레임 = 30; // 30

    public GameObject PC캠텍스쳐;
    public GameObject 모바일캠텍스쳐;
    public GameObject 스크린샷;

    public UIRoot root;

    public Camera 캡쳐카메라;

    private WebCamTexture webcamTexture;

    public List<Texture2D> 스크린샷이미지 = new List<Texture2D>();

    public static MobileCamera cam; // 다른 클래스 접근용

    void Awake()
    {
        // 화면 꺼짐 방지
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        캠화면모두닫기();
        MobileCamDevices();

        cam = this; // 다른 클래스 접근용
    }

    void Start()
    {
        DebugShow("해상도 width: " + Screen.width + " / 해상도 height: " + Screen.height);

        // 에디터 모드일 경우
        if ((Application.platform == RuntimePlatform.WindowsEditor))
        {
            PC캠텍스쳐.SetActive(true);
            PC캠텍스쳐.GetComponent<UIWidget>().width = Screen.width;
            PC캠텍스쳐.GetComponent<UIWidget>().height = Screen.height;
        }
        // 안드로이드 모드일 경우
        else if ((Application.platform == RuntimePlatform.Android))
        {
            모바일캠텍스쳐.SetActive(true);
            //모바일캠텍스쳐.GetComponent<UIWidget>().width = root.manualHeight;
            //모바일캠텍스쳐.GetComponent<UIWidget>().height = root.manualWidth;
            모바일캠텍스쳐.GetComponent<UIWidget>().width = Screen.width;
            모바일캠텍스쳐.GetComponent<UIWidget>().height = Screen.height;
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        Application.Quit();
    }

    //IEnumerator Start()
    //{
    //    yield return Application.RequestUserAuthorization(UserAuthorization.WebCam | UserAuthorization.Microphone);

    //    if (Application.HasUserAuthorization(UserAuthorization.WebCam | UserAuthorization.Microphone))
    //    {
    //        DebugShow("해상도 width: " + Screen.width + " / 해상도 height: " + Screen.height);

    //        // 에디터 모드일 경우
    //        if ((Application.platform == RuntimePlatform.WindowsEditor))
    //        {
    //            PC캠텍스쳐.SetActive(true);
    //            PC캠텍스쳐.GetComponent<UIWidget>().width = Screen.width;
    //            PC캠텍스쳐.GetComponent<UIWidget>().height = Screen.height;
    //        }
    //        // 안드로이드 모드일 경우
    //        else if ((Application.platform == RuntimePlatform.Android))
    //        {
    //            모바일캠텍스쳐.SetActive(true);
    //            //모바일캠텍스쳐.GetComponent<UIWidget>().width = root.manualHeight;
    //            //모바일캠텍스쳐.GetComponent<UIWidget>().height = root.manualWidth;
    //            모바일캠텍스쳐.GetComponent<UIWidget>().width = Screen.width;
    //            모바일캠텍스쳐.GetComponent<UIWidget>().height = Screen.height;
    //        }
    //    }
    //}

    private void 캠화면모두닫기()
    {
        PC캠텍스쳐.SetActive(false);
        모바일캠텍스쳐.SetActive(false);
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        
    }

    /// <summary>
    /// 현재 화면을 찍어 저장 합니다.
    /// </summary>
    public IEnumerator TakeSnapshot()
    {
        yield return new WaitForEndOfFrame();

        스크린샷.GetComponent<UITexture>().mainTexture = null;

        Texture2D screenshot = new Texture2D(720, 1280, TextureFormat.RGB24, false);
        RenderTexture renderTex = new RenderTexture(720, 1280, 24);
        캡쳐카메라.targetTexture = renderTex;
        캡쳐카메라.Render();
        RenderTexture.active = renderTex;
        screenshot.ReadPixels(new Rect(0, 0, 720, 1280), 0, 0);
        screenshot.Apply(false);
        캡쳐카메라.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTex);

        스크린샷.GetComponent<UITexture>().mainTexture = screenshot;

        스크린샷이미지.Add(screenshot);

        DebugShow("이미지 저장 개수: " + 스크린샷이미지.Count);
        Step01_Events.step01.사진저장라벨.text = string.Format("{0}", 스크린샷이미지.Count);        

        //StartCoroutine(이미지저장(screenshot));

        목표각도출력();
    }

    public IEnumerator 이미지저장(Texture2D img, string path)
    {
        yield return new WaitForEndOfFrame();

        byte[] byteImg = img.EncodeToJPG();

        FileStream fs = new FileStream(path + ".jpg", FileMode.Create, FileAccess.Write);
        BinaryWriter bw = new BinaryWriter(fs);
        bw.Write(byteImg);

        bw.Close();
        fs.Close();
    }

    public void 목표각도출력()
    {
        switch (스크린샷이미지.Count)
        {
            case 1: Step01_Events.step01.목표각도라벨.text = "30"; break;
            case 2: Step01_Events.step01.목표각도라벨.text = "60"; break;
            case 3: Step01_Events.step01.목표각도라벨.text = "90"; break;
            case 4: Step01_Events.step01.목표각도라벨.text = "120"; break;
            case 5: Step01_Events.step01.목표각도라벨.text = "150"; break;
            case 6: Step01_Events.step01.목표각도라벨.text = "180"; break;
            case 7: Step01_Events.step01.목표각도라벨.text = "210"; break;
            case 8: Step01_Events.step01.목표각도라벨.text = "240"; break;
            case 9: Step01_Events.step01.목표각도라벨.text = "270"; break;
            case 10: Step01_Events.step01.목표각도라벨.text = "300"; break;
            case 11: Step01_Events.step01.목표각도라벨.text = "330"; break;

            case 12: Step01_Events.step01.목표각도라벨.text = "15"; break;
            case 13: Step01_Events.step01.목표각도라벨.text = "45"; break;
            case 14: Step01_Events.step01.목표각도라벨.text = "75"; break;
            case 15: Step01_Events.step01.목표각도라벨.text = "105"; break;
            case 16: Step01_Events.step01.목표각도라벨.text = "135"; break;
            case 17: Step01_Events.step01.목표각도라벨.text = "165"; break;
            case 18: Step01_Events.step01.목표각도라벨.text = "195"; break;
            case 19: Step01_Events.step01.목표각도라벨.text = "225"; break;
            case 20: Step01_Events.step01.목표각도라벨.text = "255"; break;
            case 21: Step01_Events.step01.목표각도라벨.text = "285"; break;
            case 22: Step01_Events.step01.목표각도라벨.text = "315"; break;
            case 23: Step01_Events.step01.목표각도라벨.text = "345"; break;

            case 24: Step01_Events.step01.목표각도라벨.text = "0"; break;
            case 25: Step01_Events.step01.목표각도라벨.text = "120"; break;
            case 26: Step01_Events.step01.목표각도라벨.text = "240"; break;


            default: Step01_Events.step01.목표각도라벨.text = "0"; break;
        }

        switch (스크린샷이미지.Count)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
                Step01_Events.step01.목표각도라벨2.text = "20";
                break;

            case 12:
            case 13:
            case 14:
            case 15:
            case 16:
            case 17:
            case 18:
            case 19:
            case 20:
            case 21:
            case 22:
            case 23:
                Step01_Events.step01.목표각도라벨2.text = "40";
                break;

            case 24:
            case 25:
            case 26:
                Step01_Events.step01.목표각도라벨2.text = "60";
                break;

            default: 
                Step01_Events.step01.목표각도라벨2.text = "0";
                break;
        }
    }

    public void 스크린샷이미지초기화()
    {
        AngleChecking.angle.타겟포인트_모두비활성화();
        Step01_Events.step01.오토모드_버튼();

        Step01_Events.step01.사진저장라벨.text = string.Empty;
        Step01_Events.step01.목표각도라벨.text = "0";
        AngleChecking.angle.각도사용Y = false;

        AngleChecking.angle.자동스샷 = 0;

        스크린샷이미지.Clear();
    }

    /// <summary>
    /// 모바일 장치의 카메라 정보를 읽어 옵니다.
    /// </summary>
    private void MobileCamDevices()
    {
        root.manualWidth = Screen.width;        

        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length > 0)
        {
            for (var i = 0; i < devices.Length; i++)
            {
                DebugShow("카메라 장치: " + devices[i].name + " / 카메라 인덱스: " + i);
            }

            webcamTexture = new WebCamTexture(캠넓이, 캠높이, 프레임);
        }
        else
        {
            Debug.Log("카메라 장치 초기화 오류");
        }
    }

    /// <summary>
    /// 후면 카메라를 실행해서 플레이 합니다.
    /// </summary>
    public void MobileCam_Play()
    {
        if ((Application.platform == RuntimePlatform.WindowsEditor))
        {
            PC캠텍스쳐.GetComponent<UITexture>().mainTexture = webcamTexture; 
        }
        else if ((Application.platform == RuntimePlatform.Android))
        {
            모바일캠텍스쳐.GetComponent<UITexture>().mainTexture = webcamTexture;
        }

        webcamTexture.Play();
    }

    /// <summary>
    /// 후면 카메라를 일시정지 합니다.
    /// </summary>
    public void MobileCam_Pause()
    {
        if (webcamTexture.isPlaying)
        {
            webcamTexture.Pause();
        }
        else
        {
            webcamTexture.Play();
        }
    }

    /// <summary>
    /// 후면 카메라를 중지 합니다.
    /// </summary>
    public void MobileCam_Stop()
    {
        if (webcamTexture.isPlaying)
        {
            AngleChecking.angle.각도사용Y = false;
            webcamTexture.Stop();
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
