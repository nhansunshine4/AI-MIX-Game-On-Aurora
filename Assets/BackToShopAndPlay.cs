using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToShopAndPlay : MonoBehaviour
{
    public void BackToShopAndPlayClick() {
        SceneManager.LoadScene("ShopAndPlay");
    }
}
