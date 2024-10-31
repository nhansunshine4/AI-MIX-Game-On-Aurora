using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public Image LoadingImage;
    void Start()
    {
        LoadingImage.fillAmount = 0;
        StartCoroutine(Load());
    }
    IEnumerator Load()
    {
        while (LoadingImage.fillAmount <= 0.9f)
        {
            LoadingImage.fillAmount += 0.05f;
            yield return new WaitForSeconds(0.05f);
            yield return null;
        }
        SceneManager.LoadScene("Home");
    }
}
