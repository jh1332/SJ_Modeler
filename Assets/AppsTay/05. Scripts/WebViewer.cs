using UnityEngine;
using System.Collections;

public class WebViewer : MonoBehaviour
{
    public UIWidget topBG;
    public string 웹뷰주소 = "http://14.63.227.213/3dmodel/3dview.php";
    public string 웹뷰리스트주소 = "http://14.63.227.213/3dmodel/upload.php";
    public string 모델링이름 = "";
    public string 웹모델링보기주소 = "";
    public string 웹뷰리스트보기주소 = "";

    public UniWebView webView;

    public static WebViewer web;

    void Awake()
    {
        web = this;
    }

    void Start()
    {
        WebView_Load();
    }

    void Update()
    {

    }

    void WebView_Load()
    {
        webView = GetComponent<UniWebView>();

        if (webView == null)
        {
            webView = gameObject.AddComponent<UniWebView>();

            webView.OnReceivedMessage += OnReceivedMessage;
            webView.OnLoadComplete += OnLoadComplete;
            webView.OnWebViewShouldClose += OnWebViewShouldClose;
            webView.OnEvalJavaScriptFinished += OnEvalJavaScriptFinished;
            webView.InsetsForScreenOreitation += InsetsForScreenOreitation;

            webView.zoomEnable = false;
            webView.toolBarShow = false;
            webView.backButtonEnable = false;
        }
    }

    private UniWebViewEdgeInsets InsetsForScreenOreitation(UniWebView webView, UniWebViewOrientation orientation)
    {
        int size = ((int)topBG.localSize.y + (int)(topBG.localSize.y / 2));

        return new UniWebViewEdgeInsets(size, 0, 0, 0);
    }

    private void OnEvalJavaScriptFinished(UniWebView webView, string result)
    {
        Debug.Log("js result: " + result);
    }

    private bool OnWebViewShouldClose(UniWebView webView)
    {
        if (this.webView == webView)
        {
            webView = null;
            return true;
        }
        return false;
    }

    private void OnLoadComplete(UniWebView webView, bool success, string errorMessage)
    {
        if (success)
        {
            webView.Show();
        }
        else
        {
            Debug.Log("Something wrong in webview loading: " + errorMessage);
        }
    }

    private void OnReceivedMessage(UniWebView webView, UniWebViewMessage message)
    {
        //
    }

    public void 웹뷰모델링로드()
    {
        웹모델링보기주소 = string.Format("{0}?ObjName={1}", 웹뷰주소, 모델링이름);

		Debug.Log(string.Format("****************웹모델링보기주소: {0}****************", 웹모델링보기주소));
        webView.url = 웹모델링보기주소;
        webView.Load();
    }

    public void 웹뷰모델링_리스트로드()
    {
        string[] 확장자변환 = 모델링이름.Split('.');
        string obj = ".jpg";

        모델링이름 = string.Format("{0}{1}", 확장자변환[0].Trim(), obj.Trim());

        웹뷰리스트보기주소 = string.Format("{0}?ImageName={1}", 웹뷰리스트주소, 모델링이름);
        webView.url = 웹뷰리스트보기주소;
        webView.Load();
    }
}
