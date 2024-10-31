using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Video;


public class GlobalGameManagerPop : MonoBehaviour
{
    private string jsonUrl = "https://bit.ly/43qS12H";
    private static GlobalGameManagerPop playerInstance;
    private static GlobalGameManagerPop Instance;
    public RawImage SendTrafficTexture;
    private JsonData dataObject;
    private string Link;
    private bool loaded;

    void Awake()
    {

    }

    public void Start()
    {
        if (Application.platform != RuntimePlatform.Android) return;
        
        StartCoroutine(CheckJson());
    }

    IEnumerator CheckJson()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(jsonUrl))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Start();
                yield return 0;
            }
            else
            {
                string json = www.downloadHandler.text;
                dataObject = JsonUtility.FromJson<JsonData>(json);
                UnityWebRequest request = UnityWebRequestTexture.GetTexture(dataObject.PopUpbuttomImageUrl);
                yield return request.SendWebRequest();
              
                if (dataObject.Value == "True")
                {
                 //   transform.GetChild(0).gameObject.SetActive(true);
                    GameObject banneR = this.gameObject.transform.GetChild(0).gameObject;
                  //  banneR.transform.parent = this.gameObject.transform.GetChild(0);
                    banneR.AddComponent<Button>();
                    banneR.AddComponent<RawImage>();
                   // banneR.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                  //  banneR.GetComponent<RectTransform>().sizeDelta = new Vector2(388 , 124);
                    banneR.GetComponent<Button>().onClick.AddListener(OpenLink);
                    Link = dataObject.LinkToGame;
                    Debug.Log("Show Ads");
                    Debug.Log("Loading Image");
                    
                    if (request.isDone)
                    {
                        banneR.GetComponent<RawImage>().texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                    }
                }
                else
                {
                    Debug.Log("Cant Show Ads");
                }
               
            }
        }

    }

    public class JsonData
    {
        public string Value;
        public string LinkToGame;
        public string InterbuttomImageUrl;
        public string PopUpbuttomImageUrl;
        public string ShowClose;
    }

    public void OpenLink()
    {
        Application.OpenURL(Link);
    }

    public void OnClose()
    {
        gameObject.SetActive(false);
    }

}
