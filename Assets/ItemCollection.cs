using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollection : MonoBehaviour
{
    public Image CharacterImage;
    public void SelectCharacter()
    {
        GetComponentInParent<GameManager>().SelectCollectionCharacter(CharacterImage);
    }
}
