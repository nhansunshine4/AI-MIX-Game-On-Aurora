using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariablesDatabase : MonoBehaviour
{
    public static VariablesDatabase Instance;
    public string items;


    /*
     * Why ? 
     * Well , this is just a technic to save variables using PlayerPrefs , so we have created 
     * a large list to store all the CharacterImages no matter if they require with ads or no
     * and to ensure that there are enough variables for all the sprites we have created 
     * this large list
     * 
     * 
     * 
     * */
    void Awake()
    {
        Instance = this;
        if (!PlayerPrefs.HasKey("OwnedLockedCharacters"))
        {
            PlayerPrefs.SetString("OwnedLockedCharacters",
                "0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;" +
                "0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;" +
                "0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;" +
                "0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;" +
                "0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;");
        }
        if (!PlayerPrefs.HasKey("OwnedCollection"))
        {
            PlayerPrefs.SetString("OwnedCollection",
                "0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;" +
                "0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;" +
                "0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;" +
                "0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;" +
                "0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;" +
                "0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;" +
                "0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;" +
                "0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;" +
                "0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;" +
                "0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;" +
                "0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;" +
                "0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;" +
                "0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;" +
                "0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;" +
                "0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;");
        }
        if (!PlayerPrefs.HasKey("CollectionSize"))
        {
            PlayerPrefs.SetInt("CollectionSize", 0);
        }
        if (!PlayerPrefs.HasKey("SoundVolume"))
        {
            PlayerPrefs.SetFloat("SoundVolume", 1);
        }
        if (!PlayerPrefs.HasKey("Vibrate"))
        {
            PlayerPrefs.SetInt("Vibrate", 1);
        }
    }

}
