using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
public class Item : MonoBehaviour
{
    public GameObject LockedImage;
    public bool OpenWitchAds;
    public bool Owned;
    public Image CharacterImage;
    public int index;
    public int indexValue;
    void Awake()
    {
        
    }



    void Start()
    {
        if (!OpenWitchAds) return;
        string nm = PlayerPrefs.GetString("OwnedLockedCharacters");
        StringBuilder stringbuilder = new StringBuilder(nm);
        //if (index != 0) 
        //{
        //    if (stringbuilder[index + 1] == '1')
        //    {
        //        OpenWitchAds = false;
        //    }
        //    else
        //    {
        //        OpenWitchAds = true;
        //    }
        //}
        //else
        //{
        //    if (stringbuilder[0] == '1')
        //    {
        //        OpenWitchAds = false;
        //    }
        //    else
        //    {
        //        OpenWitchAds = true;
        //    }
        //}



        if (indexValue == 1 && ResourceBoost.Instance.pandaDragon == 1)
        {
            OpenWitchAds = false;
        }
        else if (indexValue == 2 && ResourceBoost.Instance.dragon == 1)
        {
            OpenWitchAds = false;
        }
        else if (indexValue == 3 && ResourceBoost.Instance.shark == 1)
        {
            OpenWitchAds = false;
        }
        else if (indexValue == 4 && ResourceBoost.Instance.lion == 1)
        {
            OpenWitchAds = false;
        }
        else if (indexValue == 5 && ResourceBoost.Instance.parrot == 1)
        {
            OpenWitchAds = false;
        }
        else if (indexValue == 6 && ResourceBoost.Instance.lizard == 1)
        {
            OpenWitchAds = false;
        }
        else if (indexValue == 7 && ResourceBoost.Instance.hippo == 1)
        {
            OpenWitchAds = false;
        }
        else if (indexValue == 8 && ResourceBoost.Instance.monkey == 1)
        {
            OpenWitchAds = false;
        }
        else if (indexValue == 9 && ResourceBoost.Instance.bull == 1)
        {
            OpenWitchAds = false;
        }
        else if (indexValue == 10 && ResourceBoost.Instance.catTiger == 1)
        {
            OpenWitchAds = false;
        }
        else if (indexValue == 11 && ResourceBoost.Instance.pandaParrot == 1)
        {
            OpenWitchAds = false;
        }
        else
        {
            OpenWitchAds = true;
        }

        //OpenWitchAds = false;

        if (OpenWitchAds)
            LockedImage.SetActive(true);
        else
            LockedImage.SetActive(false);
    }

    public void SelectCharacter()
    {
        GetComponentInParent<AllItems>().AdsIndex++;
        if (OpenWitchAds) 
        {
            if(Advertisements.Instance.IsInterstitialAvailable())
            {
                Advertisements.Instance.ShowInterstitial(InterstitialClosed);
            }
            else if(Advertisements.Instance.IsRewardVideoAvailable())
            {
                Advertisements.Instance.ShowRewardedVideo(CompleteMethod);
            }
        }
        else
        {
            GetComponentInParent<GameManager>().SelectCharacter(CharacterImage);
        }
    }

    void GetCharacter()
    {
        string newstring = PlayerPrefs.GetString("OwnedLockedCharacters");
        StringBuilder sb = new StringBuilder(newstring);
        if (index != 0)
            sb[index + 1] = '1';
        else
            sb[0] = '1';
        PlayerPrefs.SetString("OwnedLockedCharacters", sb.ToString());
        GetComponentInParent<GameManager>().SelectCharacter(CharacterImage);
        LockedImage.SetActive(false);
        OpenWitchAds = false;
    }

    void CompleteMethod(bool val)
    {
        if (val)
        {
            GetCharacter();
        }
    }

    void InterstitialClosed()
    {
        GetCharacter();
    }
}
