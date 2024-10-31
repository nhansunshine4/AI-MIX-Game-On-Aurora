using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ads : MonoBehaviour
{
    public GameObject InternetAlert;
    void Start()
    {
        Advertisements.Instance.SetUserConsent(true);
        Advertisements.Instance.Initialize();
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
            InternetAlert.SetActive(true);
            //No internet
        }
        else
        {
            InternetAlert.SetActive(false);
            //Connected
        }
    }
}
