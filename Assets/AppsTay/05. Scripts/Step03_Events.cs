using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using System.IO;

public class Step03_Events : MonoBehaviour
{
    /// <summary>
    /// 개발자용 디버그 출력 모드를 설정 합니다.
    /// </summary>
    public bool 디버그출력모드 = true;

    public GameObject Setp03_Main;
    public GameObject Setp03_Help;
    public GameObject Setp03_RanderingSystem;

    public GameObject check_icon01;
    public GameObject check_icon02;
    public GameObject check_icon03;

    public UISlider slider01;
    //public UISlider slider02;
    public UISlider slider03;

    public GameObject 랜더링루프;
    public GameObject 랜더링완료;

    public bool 랜더링체크 = false;
    public bool 업로드체크 = false;

    public float 타임 = 0;
    public float 랜더링타임 = 10.0f;

    public int 사진인덱스 = 0;
    public float 업로드타임 = 0;
    public float 업로드시간간격 = 0.5f;

    public string url = "http://14.63.227.213/server/UploadImage.php";
    public string 이미지이름주소 = "http://14.63.227.213/server/FileMove.php";
    public string CheckingUrl = "http://14.63.227.213/server/ObjCheck.php";
    public string postData = "";
    public string 모델링다운로드경로 = "";
    public string 년도 = "";
    public string 월일 = "";

    public string 서버이미지이름 = "";
    public string 이미지이름저장;

    private int 이미지이름카운트 = 1;

    private float 프로그래스값 = 0;

    public static Step03_Events step03;

    private const string 경로 = "http://14.63.227.213/server/obj/";

    void Awake()
    {
        step03 = this;
    }

    void Start()
    {
        Step03_모든화면닫기();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (업로드체크)
        {
            업로드타임 += Time.deltaTime;

            if (업로드타임 >= 업로드시간간격)
            {                
                데이터포스트(MobileCamera.cam.스크린샷이미지[사진인덱스++].EncodeToJPG());

                slider01.value = ((float)사진인덱스 / (float)MobileCamera.cam.스크린샷이미지.Count);

                업로드타임 = 0;

                if (사진인덱스 == MobileCamera.cam.스크린샷이미지.Count)
                {
                    업로드체크 = false;
                    사진인덱스 = 0;

                    slider01.value = 1;
                    check_icon01.SetActive(true);
                    check_icon02.SetActive(false);
                    check_icon03.SetActive(false);

                    랜더링루프.SetActive(true);
                    렌더링체크();

                    랜더링체크 = true;
                }
            }
        }

        if (랜더링체크)
        {
            타임 += Time.deltaTime;

            if (타임 >= 랜더링타임)
            {
                타임 = 0;                
                렌더링체크();
            }
        }
    }

    public void 이미지이름보내기()
    {
        WWWForm form = new WWWForm();

        form.AddField("FileName", 이미지이름저장);

        Debug.Log("이미지이름저장2: " + 이미지이름저장);

        WWW www = new WWW(이미지이름주소, form);

        StartCoroutine(이미지이름보내기아웃풋(www));
    }

    private IEnumerator 이미지이름보내기아웃풋(WWW www)
    {
        yield return www;

        if (www.error == null)
        {
            Debug.Log("이미지이름보내기아웃풋 WWW Ok!: [" + www.text + "]");
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }

    public void 데이터포스트(byte[] data)
    {
        WWWForm form = new WWWForm();

        form.AddBinaryData("upload_file", data);
        form.AddField("Flag", "0");

        WWW www = new WWW(url, form);

        StartCoroutine(데이터포스트아웃풋(www));
    }

    private IEnumerator 데이터포스트아웃풋(WWW www)
    {
        yield return www;

        if (www.error == null)
        {
            서버이미지이름 = www.text;
            이미지이름저장 += string.Format("{0},", 서버이미지이름);

            Debug.Log("WWW Ok!: " + www.text);            

            if (이미지이름카운트 == MobileCamera.cam.스크린샷이미지.Count)
            {
                이미지이름저장 = 이미지이름저장.TrimEnd(',');
                Debug.Log("이미지이름저장: " + 이미지이름저장);
                이미지이름보내기();
            }

            Debug.Log("이미지이름카운트: " + 이미지이름카운트++);
        }
        else
        {
            //Debug.Log("WWW Error: " + www.error);
        }
    }

    public void 렌더링체크()
    {
        WWWForm form = new WWWForm();

        string[] 확장자변환 = 서버이미지이름.Split('.');
        string obj = ".obj";

        서버이미지이름 = string.Format("{0}{1}", 확장자변환[0].Trim(), obj.Trim());

        form.AddField("Cache-Control", "no-cache");
        form.AddField("ImageName", 서버이미지이름);
        form.AddField("Flag", "0");

        WWW www = new WWW(CheckingUrl, form);

        StartCoroutine(랜더링아웃풋(www));        
    }

    private IEnumerator 랜더링아웃풋(WWW www)
    {
        yield return www;

        Debug.Log("랜더링 아웃풋 메시지1: [" + www.text + "]");

        if (www.error == null)
        {
            string 아웃풋 = www.text.Trim();

            if (아웃풋.CompareTo("OK") == 0)
            {
                check_icon02.SetActive(true);
                랜더링루프.SetActive(false);
                랜더링완료.SetActive(true);
                랜더링체크 = false;

                //파일다운로드();
                Invoke("파일다운로드", 1);

                Debug.Log("다운로드 진행...");
            }
            else
            {
                랜더링루프.SetActive(true);
                Debug.Log("다운로드 실패...");
            }
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }


    public void 파일다운로드()
    {
        년도 = 서버이미지이름.Substring(0, 4).Trim();
        월일 = 서버이미지이름.Substring(4, 4).Trim();
        모델링다운로드경로 = string.Format("{0}{1}/{2}/{3}", 경로, 년도, 월일, 서버이미지이름);

        Debug.Log("모델링다운로드경로: " + 모델링다운로드경로);

        //string dir = Application.persistentDataPath + "/" + 년도 + "/" + 월일;

        //if (!Directory.Exists(dir))
        //{
        //    Directory.CreateDirectory(dir);
        //}
        //else
        //{
        //    for (int i = 0; i < MobileCamera.cam.스크린샷이미지.Count; i++)
        //    {
        //        MobileCamera.cam.이미지저장(MobileCamera.cam.스크린샷이미지[i], (dir + i.ToString()));
        //    }
        //}

        StartCoroutine(모델링다운로드());
    }

    IEnumerator 모델링다운로드()
    {
        WWW www = new WWW(모델링다운로드경로);

        // 다운로드 진행률을 표시
        while (!www.isDone)
        {
            slider03.value = (float)www.progress;            
            yield return null;
        }

        // 다운로드 오류가 발생
        if (!string.IsNullOrEmpty(www.error))
        {            
            Debug.Log("www.error: " + www.error + " / byte: " + www.bytes.Length);
        }
        else // 다운로드 성공적으로 완료
        {
            check_icon03.SetActive(true);

            slider03.value = 1;
            check_icon03.SetActive(true);

            string dir = Application.persistentDataPath + "/Modeling";

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            File.WriteAllBytes(dir + "/" + 서버이미지이름, www.bytes);

            WebViewer.web.모델링이름 = 서버이미지이름; // 웹뷰에서볼 파일 경로

            // 0 kb 이면 오류처리
            if (www.bytes.Length <= 0)
            {
                Popups.popup.다운로드팝업_오류();
            }
            else
            {
                // 4페이지로 이동
                Step03_다운로드완료();
            }
        }
    }

    public void Step03_이미지업로드()
    {
        업로드체크 = true;
    }

    public void Step03_다운로드완료()
    {
        Step03_모든화면닫기();

        Step04_Events.step04.Step04_메인화면();
    }

    public void Step03_모든화면닫기()
    {
        랜더링루프.SetActive(false);
        랜더링완료.SetActive(false);

        Setp03_Main.SetActive(false);
        Setp03_Help.SetActive(false);
        Setp03_RanderingSystem.SetActive(false);
    }

    public void Step03_메인화면()
    {
        Step03_모든화면닫기();

        Setp03_Main.SetActive(true);
    }

    public void Step03_도움말()
    {
        Step03_모든화면닫기();

        Setp03_Help.SetActive(true);
    }

    public void 초기화()
    {
        이미지이름카운트 = 1;

        이미지이름저장 = "";
        서버이미지이름 = "";

        // 문자열 초기화
        모델링다운로드경로 = 경로;
        년도 = "";
        월일 = "";

        // 프로그래스바 초기화
        slider01.value = 0;
        slider03.value = 0;
    }

    public void Step03_랜더링_업로드_다운로드()
    {
        초기화();

        check_icon01.SetActive(false);
        check_icon02.SetActive(false);
        check_icon03.SetActive(false);

        Step03_모든화면닫기();

        Setp03_RanderingSystem.SetActive(true);

        Step03_이미지업로드();
    }
}
