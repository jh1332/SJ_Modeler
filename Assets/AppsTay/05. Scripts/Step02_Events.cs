using UnityEngine;
using System.Collections;
using System;

public class Step02_Events : MonoBehaviour
{
    /// <summary>
    /// 개발자용 디버그 출력 모드를 설정 합니다.
    /// </summary>
    public bool 디버그출력모드 = true;

    public GameObject Setp02_Main;
    public GameObject Setp02_Help;
    public GameObject Setp02_AlbumList;
    public GameObject Setp02_AlbumSlide;
    public GameObject Setp02_AlbumMaxSize;
    public GameObject Setp02_Popup;

    //public GameObject Setp01_Main;

    public Texture BlackBG;
    public UITexture FullBG;
    public UITexture[] AlbumLists;
    public UITexture AlbumSlides;
    public GameObject Photos;

    public UILabel 사진최소최대텍스트;

    public int SliderIndex = 0;

    public static Step02_Events step02;

    void Awake()
    {
        step02 = this;
    }

    void Start()
    {
        Step02_모두닫기();
    }

    void Update()
    {

    }

    public void 이전스크린샷()
    {
        if (SliderIndex <= 0)
        {
            DebugShow("사진 최소값을 넘었음.. 리턴");
            return;
        }

        if (SliderIndex <= Step01_Events.step01.사진저장개수)
        {
            AlbumSlides.mainTexture = MobileCamera.cam.스크린샷이미지[--SliderIndex];
            사진최소최대텍스트.text = string.Format("{0} of {1}", SliderIndex + 1, Step01_Events.step01.사진저장개수);
        }

        DebugShow("이전스크린샷: " + SliderIndex.ToString());
    }

    public void 다음스크린샷()
    {
        if ((Step01_Events.step01.사진저장개수 - 1) == SliderIndex)
        {
            DebugShow("사진 최대값을 넘었음.. 리턴");
            return;
        }

        if (SliderIndex >= 0)
        {
            AlbumSlides.mainTexture = MobileCamera.cam.스크린샷이미지[++SliderIndex];
            사진최소최대텍스트.text = string.Format("{0} of {1}", SliderIndex + 1, Step01_Events.step01.사진저장개수);
        }

        DebugShow("다음스크린샷: " + SliderIndex.ToString());
    }

    public void 앨범로드()
    {
        // 썸네일 이미지 로드
        for (int i = 0; i < MobileCamera.cam.스크린샷이미지.Count; i++)
        {
            AlbumLists[i].mainTexture = MobileCamera.cam.스크린샷이미지[i];
        }

        // 슬라이드 이미지 로드
        AlbumSlides.mainTexture = MobileCamera.cam.스크린샷이미지[0];
        사진최소최대텍스트.text = string.Format("1 of {0}", Step01_Events.step01.사진저장개수);

        DebugShow("이미지 개수: " + MobileCamera.cam.스크린샷이미지.Count);
    }

    public void 앨범전체화면(GameObject obj)
    {
        int index = Convert.ToInt32(obj.name);

        if (index < MobileCamera.cam.스크린샷이미지.Count)
        {
            FullBG.mainTexture = MobileCamera.cam.스크린샷이미지[index];
            Setp02_AlbumMaxSize.SetActive(true);
        }
        else
        {
            DebugShow("스크린샷이미지의 배열 범위를 벗어났습니다.");
        }

        Photos.SetActive(false);
    }

    public void 슬라이드앨범전체화면()
    {
        FullBG.mainTexture = MobileCamera.cam.스크린샷이미지[SliderIndex];
        Setp02_AlbumMaxSize.SetActive(true);
    }

    public void Step02_앨범전체화면닫기()
    {
        Setp02_AlbumMaxSize.SetActive(false);
        FullBG.mainTexture = BlackBG;

        Photos.SetActive(true);
    }

    private void Step02_모두닫기()
    {
        Setp02_Popup.SetActive(false);
        Setp02_Help.SetActive(false);
        Setp02_AlbumList.SetActive(false);
        Setp02_AlbumSlide.SetActive(false);
        Setp02_AlbumMaxSize.SetActive(false);
        Setp02_Main.SetActive(false);
    }

    public void Step02_메인화면()
    {
        Step02_모두닫기();

        Setp02_Main.SetActive(true);
    }

    public void Step01_도움말()
    {
        Step02_모두닫기();

        Setp02_Help.SetActive(true);
    }

    public void Step01_뒤로가기()
    {
        Step02_모두닫기();

        Setp02_Main.SetActive(true);
    }

    /// <summary>
    /// 앨범에서 기본은 썸네일 모드 입니다.
    /// </summary>
    public void Step02_썸네일모드()
    {
        Step02_모두닫기();

        Setp02_AlbumList.SetActive(true);
    }

    public void Step02_슬라이드모드()
    {
        Step02_모두닫기();

        Setp02_AlbumSlide.SetActive(true);
    }

    public void Step02_스크린샷이미지초기화()
    {
        Step02_모두닫기();

        Step01_Events.step01.Step01_메인화면();

        MobileCamera.cam.스크린샷이미지초기화();
        DebugShow("모든 스크린샷 이미지가 초기화 되었습니다.");
    }

    public void Step02_완료확인팝업()
    {
        Setp02_Popup.SetActive(true);
    }

    public void Step02_앨범페이지완료()
    {
        Step02_모두닫기();

        Step03_Events.step03.Step03_메인화면();
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
