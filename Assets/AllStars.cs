using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllStars : MonoBehaviour
{
    public Color ActiveColor;
    public Color DisabledColor;
    public int ite = -1;
    Star[] star;
    public int SelectedStars;
    public string PackageName;
    public GameObject Parent;
    void Start()
    {
        star = GetComponentsInChildren<Star>();
        foreach(Star stars in star)
        {
            stars.gameObject.GetComponent<Image>().color = DisabledColor;
            stars.index = ite += 1;
        }
    }

    public void SelectStar(Star str)
    {
        SelectedStars = str.index;
        foreach (Star stars in star)
        {
            stars.gameObject.GetComponent<Image>().color = DisabledColor;
        }
        for (int i = 0; i <= str.index; i++)
        {
            star[i].gameObject.GetComponent<Image>().color = ActiveColor;
        }
    }

    public void Rate()
    {
        if(SelectedStars >= 2)
        {
            Application.OpenURL("https://play.google.com/store/apps/details?id=" + PackageName);
            Parent.SetActive(false);
        }
    }
    [System.Serializable]
    public class PackageInfo
    {
        public string name;
    }


}
