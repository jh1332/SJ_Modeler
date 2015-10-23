using UnityEngine;
using System.Collections;

public class Step_Gallery : MonoBehaviour
{
	static Step_Gallery _instance = null;
	static Step_Gallery _Instance
	{
		get
		{
			if(null == _instance)
			{
				GameObject galleryObj = GameObject.Find("Root Manager/UI Root (2D)/Step_Gallery");
				if(null == galleryObj)
				{
					Debug.LogError("null == galleryObj");
				}

				_instance = galleryObj.GetComponent<Step_Gallery>();
            }

			return _instance;
        }
	}

	public static void Init()
	{
		_Instance.gameObject.SetActive(true);

		WebViewer.web.webView.Load("http://14.63.227.213/3dmodel/index.php");
	}

	public void OnClickBack()
	{
		_instance.gameObject.SetActive(false);

		WebViewer.web.webView.Hide();

		Step01_Events.step01.Step01_메인화면();
	}
}
