using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Text;

public class GameManager : MonoBehaviour
{
    public List<Image> sprites;
    public List<string> spritesnames;
    public GameObject CollectionItemPrefab;
    public Transform CollectionItemHandler;
    public Transform StarsHandler;
    [Header("Characters Images : ")]
    [Space]
    public GameObject CharacterImage_1_Handler;
    public GameObject CharacterImage_2_Handler;

    public int SelectCharacterIndex;
    public bool AlreadyChoosenCharacters;

    public Image CharacterImage_1_Sprite;
    public Image CharacterImage_2_Sprite;
    public Image Character_Collection_Sprite;
    public Image FinalResult_Sprite;

    public Transform CharacterImage_1_InitialPosition;
    public Transform CharacterImage_2_InitialPosition;

    public Transform CharacterImage_1_SecondPosition;
    public Transform CharacterImage_2_SecondPosition;


    public ParticleSystem Create_New_Character_ParticleSystem;

    public GameObject SpriteEvents;
    public GameObject AllCharactersIconsHandler;


    public GameObject Next_Button;
    public GameObject Create_Button;


    public Image MainMenuCharacterImage;

    public Sprite[] AllSprites;
    public Sprite[] SpecialSprites;
    public Sprite[] UniqueMergeSprites;
    private int MainMenuCharacterImageIterate = 0;

    [Header("Panels : ")]
    [Space]

    public GameObject CreatePanel;
    public GameObject FinaleResultsPanel;


    [Header("Audio : ")]
    [Space(3)]

    public AudioSource MainAudioSource;
    public AudioClip CreateAudioClip;
    public AudioClip SelectSpriteAudioClip;
    public AudioClip ConfirmSelectionAudioClip;
    public AudioClip ClickUIAudioClip;


    [Header("Settings UI : ")]
    [Space]
    public Slider SoundVolumeSlider;
    public Toggle Vibrate;
    

    private bool ConfirmCharacterSelected;


    void Start()
    {
        SoundVolumeSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        StartCoroutine(AnimateMainMenuCharacterImage());
        StartCoroutine(CharacterSelectionAndBehaviour());
        StartCoroutine(SpritesHandler());
        Transform[] allcharacterssprites = AllCharactersIconsHandler.GetComponentsInChildren<Transform>();
        sprites = new List<Image>();
        foreach (Transform child in allcharacterssprites)
        {
            if (child.name == "Icon")
            {
                sprites.Add(child.GetComponent<Image>());
            }
        }
        for (int i = 0; i < spritesnames.Count - 1; i++)
        {
            if (spritesnames[i] == "" || spritesnames[i] == " ")
            {
                UniqueMergeSprites[i] = AllSprites[i];
            }
        }
        InitializeCollection();
        /*
        int ite = 0;
        for (int i = 0; i < sprites.Count; i++)
        {
            for(int i2 = ite; i2 < UniqueMergeSprites.Length; i2++)
            {
                if (UniqueMergeSprites[i2])
                {
                    Debug.Log("assign " + sprites[i].sprite + " to : " + UniqueMergeSprites[i2]);
                    sprites[i].sprite = UniqueMergeSprites[i2];
                    ite = i2 + 1;
                    break;
                }
            }   
        }
        */
        /*
          for (int i = 0; i < AllSprites.Length - 1; i++)
           {
               for(int i2 = 0;i2 < AllSprites[i].name.Length - 1; i2++)
               {
                   if(IsOneWordSpriteName(AllSprites[i].name))
                   {
                       UniqueMergeSprites[i] = AllSprites[i];
                   }
                   else
                   {

                   }
               }
           }
        */

        // InitializeNames();

    }

    /*
        void InitializeNames()
        {
            string name = "";
            int ite = 0;
            bool willadd = false;

            for (int i = 0; i < AllSprites.Length; i++)
            {
                for (int i2 = 0; i2 < AllSprites[i].name.Length - 1; i2++)
                {
                    if (IsAnyAlphabet(AllSprites[i].name[i2]))
                    {
                        name += AllSprites[i].name[i2];
                    }
                    if (AllSprites[i].name[i2] == ' ')
                    {
                        name += " ";
                        if (i2 != 3 && i2 != 2) willadd = true;
                    }
                    if (AllSprites[i].name[i2] == '_')
                    {
                        name += "_";
                        if (i2 == 3 || i2 == 2) willadd = true;
                    }
                }
                if (willadd) spritesnames[i] = name;
                name = "";
                willadd = false;
            }
        }
    */

    void InitializeCollection()
    {
        Sprite spr;
        for (int i = 0; i <= PlayerPrefs.GetInt("CollectionSize"); i++)
        {
            for (int i2 = 0; i2 < AllSprites.Length - 1; i2++)
            {
                if (i2 != 0) {
                    if (PlayerPrefs.GetString("OwnedCollection")[i2 + 1] == '1')
                    {
                        spr = AllSprites[i2];
                        GameObject obj = Instantiate(CollectionItemPrefab);
                        obj.transform.parent = CollectionItemHandler;
                        obj.transform.localScale = new Vector3(1, 1, 1);
                        obj.GetComponent<ItemCollection>().CharacterImage.sprite = spr;
                    }
                }
                else if(i2 == 0)
                {
                    if (PlayerPrefs.GetString("OwnedCollection")[0] == '1')
                    {
                        spr = AllSprites[i2];
                        GameObject obj = Instantiate(CollectionItemPrefab);
                        obj.transform.parent = CollectionItemHandler;
                        obj.transform.localScale = new Vector3(1, 1, 1);
                        obj.GetComponent<ItemCollection>().CharacterImage.sprite = spr;
                    }
                }
            }
        }
    }

    public void Update()
    {
        PlayerPrefs.SetFloat("SoundVolume", SoundVolumeSlider.value);
        AudioListener.volume = PlayerPrefs.GetFloat("SoundVolume");
    }

    public void EnterCreatePanel()
    {
        CreatePanel.SetActive(true);
    }

    bool loop = false;
    bool isAnimating = false;
    Image g_img;

    public void SelectCharacter(Image img)
    {     
        MainAudioSource.PlayOneShot(SelectSpriteAudioClip);
        if (!AlreadyChoosenCharacters && !isAnimating)
        {
            g_img = img;
            loop = true;
            isAnimating = true;
        }
    }

    public void SelectCollectionCharacter(Image img)
    {
        MainAudioSource.PlayOneShot(SelectSpriteAudioClip);
        Character_Collection_Sprite.sprite = img.sprite;
    }

    bool confirm = true;

    public void ConfirmSelectCharacter()
    {
        if (g_img == null || !confirm) return;
        //Handheld.Vibrate();
        MainAudioSource.PlayOneShot(ConfirmSelectionAudioClip);
        if (SelectCharacterIndex != 3 && !ConfirmCharacterSelected)
        {
            ConfirmCharacterSelected = true;
            loop = true;
        }
        else if(SelectCharacterIndex == 3)
        {
            StartCoroutine(Animate_Character_1_To_Vector2());
            StartCoroutine(Animate_Character_2_To_Vector2());
            StartCoroutine(Animate_Finish());
            confirm = false;
        }
    }

    IEnumerator CharacterSelectionAndBehaviour()
    {
        while (true)
        {
            while (loop)
            {
                if (SelectCharacterIndex == 0)
                {
                    CharacterImage_1_Sprite.sprite = g_img.sprite;
                    if (ConfirmCharacterSelected)
                    {
                        Vector2 disVec = CharacterImage_1_SecondPosition.gameObject.GetComponent<RectTransform>().anchoredPosition - CharacterImage_1_Handler.GetComponent<RectTransform>().anchoredPosition;
                        float dis = Vector2.Distance(CharacterImage_1_SecondPosition.gameObject.GetComponent<RectTransform>().anchoredPosition, CharacterImage_1_Handler.GetComponent<RectTransform>().anchoredPosition);
                        for (Vector2 pos = CharacterImage_1_Handler.GetComponent<RectTransform>().anchoredPosition; dis > 10f; pos += disVec * Time.deltaTime)
                        {
                            dis = Vector2.Distance(CharacterImage_1_SecondPosition.gameObject.GetComponent<RectTransform>().anchoredPosition, CharacterImage_1_Handler.GetComponent<RectTransform>().anchoredPosition);
                            CharacterImage_1_Handler.GetComponent<RectTransform>().anchoredPosition = pos;
                            if (CharacterImage_1_Handler.transform.localScale.magnitude >= 0.85f)
                            {
                                CharacterImage_1_Handler.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
                            }
                            yield return null;
                        }
                        yield return new WaitForSeconds(1f);
                        CharacterImage_2_Handler.SetActive(true);
                        SelectCharacterIndex = 1;
                        ConfirmCharacterSelected = false;                                               
                    }
                }
                else if (SelectCharacterIndex == 1)
                {
                    CharacterImage_2_Sprite.sprite = g_img.sprite;
                    if (ConfirmCharacterSelected)
                    {
                        Vector2 disVec = CharacterImage_2_SecondPosition.gameObject.GetComponent<RectTransform>().anchoredPosition - CharacterImage_2_Handler.GetComponent<RectTransform>().anchoredPosition;
                        float dis = Vector2.Distance(CharacterImage_2_SecondPosition.gameObject.GetComponent<RectTransform>().anchoredPosition, CharacterImage_2_Handler.GetComponent<RectTransform>().anchoredPosition);
                        for (Vector2 pos = CharacterImage_2_Handler.GetComponent<RectTransform>().anchoredPosition; dis > 10f; pos += disVec * Time.deltaTime)
                        {
                            dis = Vector2.Distance(CharacterImage_2_SecondPosition.gameObject.GetComponent<RectTransform>().anchoredPosition, CharacterImage_2_Handler.GetComponent<RectTransform>().anchoredPosition);
                            CharacterImage_2_Handler.GetComponent<RectTransform>().anchoredPosition = pos;
                            if (CharacterImage_2_Handler.transform.localScale.magnitude >= 0.85f)
                            {
                                CharacterImage_2_Handler.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
                            }
                            yield return null;
                        }
                        yield return new WaitForSeconds(1f);
                        //CharacterImage_2_Handler.SetActive(true);
                        ConfirmCharacterSelected = false;
                        Next_Button.SetActive(false);
                        Create_Button.SetActive(true);
                        AlreadyChoosenCharacters = true;
                        SelectCharacterIndex = 3;
                    }                    
                }

                isAnimating = false;
                loop = false;
                yield return null;
            }
            yield return null;
        }
    }

    IEnumerator Animate_Character_1_To_Vector2()
    {
        Vector2 finalepos = Vector2.zero;
        Vector2 disVec = finalepos - CharacterImage_1_Handler.GetComponent<RectTransform>().anchoredPosition;
        float dis = Vector2.Distance(finalepos, CharacterImage_1_Handler.GetComponent<RectTransform>().anchoredPosition);
        for (Vector2 pos = CharacterImage_1_Handler.GetComponent<RectTransform>().anchoredPosition; dis > 10f; pos += disVec * Time.deltaTime)
        {
            dis = Vector2.Distance(finalepos, CharacterImage_1_Handler.GetComponent<RectTransform>().anchoredPosition);
            CharacterImage_1_Handler.GetComponent<RectTransform>().anchoredPosition = pos;
            if (CharacterImage_1_Handler.transform.localScale.magnitude >= 0.85f)
            {
                //    CharacterImage_1_Handler.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            }
            if (Vector2.Distance(finalepos, CharacterImage_1_Handler.GetComponent<RectTransform>().anchoredPosition) >= 500)
            {
                break;
            }
            yield return null;
        }
    }

    IEnumerator Animate_Character_2_To_Vector2()
    {
        Vector2 finalepos = Vector2.zero;

        Vector2 disVec = finalepos - CharacterImage_2_Handler.GetComponent<RectTransform>().anchoredPosition;
        float dis = Vector2.Distance(finalepos, CharacterImage_2_Handler.GetComponent<RectTransform>().anchoredPosition);
        for (Vector2 pos = CharacterImage_2_Handler.GetComponent<RectTransform>().anchoredPosition; dis > 10f; pos += disVec * Time.deltaTime)
        {
            dis = Vector2.Distance(finalepos, CharacterImage_2_Handler.GetComponent<RectTransform>().anchoredPosition);
            CharacterImage_2_Handler.GetComponent<RectTransform>().anchoredPosition = pos;
            if (CharacterImage_2_Handler.transform.localScale.magnitude >= 0.85f)
            {
                //  CharacterImage_2_Handler.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            }
            if (Vector2.Distance(finalepos, CharacterImage_2_Handler.GetComponent<RectTransform>().anchoredPosition) >= 500)
            {
                break;
            }
            yield return null;
        }
    }

    IEnumerator Animate_Finish()
    {
        Create_New_Character_ParticleSystem.Play();
        yield return new WaitForSeconds(1.5f);
        CharacterImage_1_Handler.SetActive(false);
        CharacterImage_2_Handler.SetActive(false);
        yield return new WaitForSeconds(3f);
        MainAudioSource.PlayOneShot(CreateAudioClip);
        FinaleResultsPanel.SetActive(true);
        Sprite finalsprite = FinalResultSpriteAI();
        FinalResult_Sprite.sprite = finalsprite;
        StarsSet();
    }
    public List<RectTransform> starsobject;
    void StarsSet()
    {
        
        RectTransform[] detectstars = StarsHandler.GetComponentsInChildren<RectTransform>(true);
        
        foreach (RectTransform ite in detectstars)
        {
            if(ite.gameObject.name == "Star")
            {
                starsobject.Add(ite);
            }
        }
        foreach(RectTransform obj in starsobject)
        {
            if(obj != StarsHandler)
            {
                if(obj.gameObject.name =="Star") obj.gameObject.SetActive(false);
            }
        }
        int stars = Random.Range(1,4);
        for(int i = 0; i <= stars; i++)
        {
            if (starsobject[i] != StarsHandler)
            {
                if (starsobject[i].gameObject.name == "Star")
                {
                    starsobject[i].gameObject.SetActive(true);
                }
            }
        }
    }
    public void SaveCharacter()
    {
        //if (Advertisements.Instance.IsInterstitialAvailable())
        //{
        //    Advertisements.Instance.ShowInterstitial();
        //}
        //else if (Advertisements.Instance.IsRewardVideoAvailable())
        //{
        //    Advertisements.Instance.ShowRewardedVideo(completed);
        //}
        UserWatchedAdsToSave();
    }
    void completed(bool val) { }
    void InterstitialClosed()
    {
        UserWatchedAdsToSave();
    }

    void CompleteMethod(bool val)
    {
        if (!val) return;
        UserWatchedAdsToSave();
    }

    void UserWatchedAdsToSave()
    {
        Sprite finalsprite = FinalResultSpriteAI();
        Transform[] allcharacterssprites = CollectionItemHandler.GetComponentsInChildren<Transform>();
        GameObject obj = Instantiate(CollectionItemPrefab);
        obj.transform.parent = CollectionItemHandler;
        obj.transform.localScale = new Vector3(1, 1, 1);
        obj.GetComponent<ItemCollection>().CharacterImage.sprite = finalsprite;
        confirm = true;
        RestartAgain(false);
        foreach (Transform child in allcharacterssprites)
        {
            if (child.name == "Image")
            {
                if (child.GetComponent<Image>().sprite.name == finalsprite.name)
                {
                    Destroy(obj);
                    Debug.Log("AlreadySaved");
                    return;
                    break;
                }
            }
        }
        Debug.Log("Save");
        for (int i = 0; i < AllSprites.Length - 1; i++)
        {
            if (i != 0)
            {
                if (finalsprite == AllSprites[i])
                {
                    string name = PlayerPrefs.GetString("OwnedCollection");
                    StringBuilder stringbuilder = new StringBuilder(name);
                    stringbuilder[i + 1] = '1';
                    PlayerPrefs.SetString("OwnedCollection", stringbuilder.ToString());
                    break;
                }
            }
            else if (i == 0)
            {
                if (finalsprite == AllSprites[i])
                {
                    string name = PlayerPrefs.GetString("OwnedCollection");
                    StringBuilder stringbuilder = new StringBuilder(name);
                    stringbuilder[i + 1] = '1';
                    PlayerPrefs.SetString("OwnedCollection", stringbuilder.ToString());
                    break;
                }
            }
        }
        
    }

    IEnumerator SpritesHandler()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            Instantiate(SpriteEvents);
        }
        yield return 0;
    }

    public void RestartAgain(bool showad)
    {
        if (showad)
        {
            if (Advertisements.Instance.IsInterstitialAvailable())
            {
                Advertisements.Instance.ShowInterstitial();
            }
            else if (Advertisements.Instance.IsRewardVideoAvailable())
            {
                Advertisements.Instance.ShowRewardedVideo(completed);
            }
        }
        StopCoroutine(Animate_Character_1_To_Vector2());
        StopCoroutine(Animate_Character_2_To_Vector2());
        ConfirmCharacterSelected = false;
        AlreadyChoosenCharacters = false;
        SelectCharacterIndex = 0;
        Next_Button.SetActive(true);
        Create_Button.SetActive(false);
        loop = false;
        confirm = true;
        CharacterImage_1_Handler.SetActive(true);

        CharacterImage_1_Handler.transform.localScale = new Vector3(1, 1, 1);
        CharacterImage_2_Handler.transform.localScale = new Vector3(1, 1, 1);

        CharacterImage_1_Handler.GetComponent<RectTransform>().anchoredPosition
            =
            CharacterImage_1_InitialPosition.gameObject.GetComponent<RectTransform>().anchoredPosition;

        CharacterImage_2_Handler.GetComponent<RectTransform>().anchoredPosition
            =
            CharacterImage_2_InitialPosition.gameObject.GetComponent<RectTransform>().anchoredPosition;

        FinaleResultsPanel.SetActive(false);

        CreatePanel.SetActive(true);
    }


    public void CreateButton()
    {
        AlreadyChoosenCharacters = true;
    }


    public Sprite FinalResultSpriteAI()
    {
        string character_1_name = CharacterImage_1_Sprite.sprite.name;
        string character_2_name = CharacterImage_2_Sprite.sprite.name;
        string character_1_new_name = "";
        string character_2_new_name = "";
        string match_name = "";
        Debug.Log("character 1 name : " + CharacterImage_1_Sprite.sprite.name);
        Debug.Log("character 2 name : " + CharacterImage_2_Sprite.sprite.name);

        for (int i = 0 ; i < CharacterImage_1_Sprite.sprite.name.Length; i++)
        {
            if (IsAnyAlphabet(CharacterImage_1_Sprite.sprite.name[i]))
            {
                character_1_new_name += CharacterImage_1_Sprite.sprite.name[i];
            }
        }

        for (int i = 0; i < CharacterImage_2_Sprite.sprite.name.Length; i++)
        {
            if (IsAnyAlphabet(CharacterImage_2_Sprite.sprite.name[i]))
            {
                character_2_new_name += CharacterImage_2_Sprite.sprite.name[i];
            }
        }

        string[,] divided_names = new string[spritesnames.Count, 2];
        string name_1_in_array = "";
        string name_2_in_array = "";
        bool firstname = true;

        for (int i = 0; i < spritesnames.Count; i++)
        {
            for(int i2 = 1; i2 < spritesnames[i].Length; i2++)
            {
                if(spritesnames[i][i2] != ' ')
                {
                    if (firstname)
                    {
                        name_1_in_array += spritesnames[i][i2];
                    }
                    else
                    {
                        name_2_in_array += spritesnames[i][i2];
                    }
                }
                else
                {
                    firstname = false;
                }
            }
            divided_names[i,0] = name_1_in_array;
            divided_names[i,1] = name_2_in_array;
            name_1_in_array = "";
            name_2_in_array = "";
            firstname = true;
        }

      //  Debug.Log("character 1 new name : " + character_1_new_name);
     //   Debug.Log("character 2 new name : " + character_2_new_name);
     //   Debug.Log("/////////////////////////");
     //   Debug.Log("divided_names_Length : " + divided_names.Length);

        for (int i = 0; i < divided_names.Length / 2; i++)
        {
            if ((divided_names[i,0] == character_1_new_name || divided_names[i,1] == character_1_new_name)
                &&(divided_names[i, 0] == character_2_new_name || divided_names[i, 1] == character_2_new_name)
                )
            {
             //   Debug.Log("Match Found : " + divided_names[i, 0]);
              //  Debug.Log("Match Found : " + divided_names[i, 1]);
                match_name = divided_names[i, 0] + " " + divided_names[i, 1];
            }
        }
        for(int i = 0; i < spritesnames.Count; i++)
        {
            if(spritesnames[i] == " " + match_name)
            {
        //        Debug.Log("Match Found at : " + i);
                return AllSprites[i];
                break;
            }
        }
      //  Debug.Log("Noting Found !");
        return SpecialSprites[Random.Range(0, SpecialSprites.Length - 1)];
    }


    public static bool IsAnyAlphabet(char character)
    {
        return (character >= 'A' && character <= 'Z') || (character >= 'a' && character <= 'z');
    }


    public void ClickUISound()
    {
        MainAudioSource.PlayOneShot(ClickUIAudioClip);
    }

    public void OpenHome()
    {
        CreatePanel.SetActive(false);
        FinaleResultsPanel.SetActive(false);
    }

    IEnumerator AnimateMainMenuCharacterImage()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.5f);
            if (MainMenuCharacterImageIterate < AllSprites.Length - 2)
            {
                MainMenuCharacterImage.sprite = AllSprites[MainMenuCharacterImageIterate];
                MainMenuCharacterImageIterate++;
            }
            else
            {
                MainMenuCharacterImageIterate = 0;
            }
            yield return null;
        }
    }
   
}
