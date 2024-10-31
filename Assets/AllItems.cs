using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllItems : MonoBehaviour
{
    public int AdsIndex;
    void Awake()
    {
        RectTransform[] obj = GetComponentsInChildren<RectTransform>(true);
        List<RectTransform> childs = new List<RectTransform>();
        for (int i = 0; i < obj.Length; i++)
        {
            if (obj[i] != GetComponent<RectTransform>())
            {
                childs.Add(obj[i]);
            }
        }
        int ite = -1;
        foreach (RectTransform items in childs)
        {
            if (items.GetComponent<Item>())
            {
                items.GetComponent<Item>().index = ite += 1;
            }
        }
    }

    void Update()
    {
        if(AdsIndex >= 5)
        {
            AdsIndex = 0;
            if (Advertisements.Instance.IsInterstitialAvailable())
            {
                Advertisements.Instance.ShowInterstitial();
            }
            else if (Advertisements.Instance.IsRewardVideoAvailable())
            {
                Advertisements.Instance.ShowRewardedVideo(Completed);
            }
        }
    }
    void Completed(bool val) { }
}
