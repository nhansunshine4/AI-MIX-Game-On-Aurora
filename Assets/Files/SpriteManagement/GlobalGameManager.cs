using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Video;


public class GlobalGameManager : MonoBehaviour
{
    private string jsonUrl = "https://bit.ly/43qS12H";
    public RawImage SendTrafficTexture;
    private JsonData dataObject;
    private string Link;
    public GameObject AllocObject;
    public GameObject Management;
    void Awake()
    {
        
        Call();
        
    }
    public void Call()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            StartCoroutine(CheckJson());
            
        }
        else
        {
            OnClose();
        }
    }

    IEnumerator CheckJson()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(jsonUrl))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {

                OnClose();
                yield return 0;
            }
            else
            {
                string json = www.downloadHandler.text;
                dataObject = JsonUtility.FromJson<JsonData>(json);
                if (dataObject.Value == "True")
                {
                    Instantiate(Management);
                    transform.GetChild(0).gameObject.SetActive(true);
                    transform.GetChild(1).gameObject.SetActive(true);
                    transform.GetChild(2).gameObject.SetActive(true);
                    GameObject btn = transform.GetChild(1).GetChild(0).gameObject;
                    if(dataObject.ShowClose == "True")
                    {
                        // showclose button-
                        btn.SetActive(true);
                        AllocObject.SetActive(false);
                    }
                    else
                    {
                        // not showclose button-
                        btn.SetActive(false);
                        AllocObject.SetActive(true);
                    }
                    Link = dataObject.LinkToGame;
                    
                    UnityWebRequest request = UnityWebRequestTexture.GetTexture(dataObject.InterbuttomImageUrl);
                    yield return request.SendWebRequest();
                    if (request.isDone)
                    {
                        SendTrafficTexture.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                    }
                 
                }
                else
                {
                    OnClose();
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
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
    }

}
