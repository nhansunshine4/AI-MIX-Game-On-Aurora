using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBoost : MonoBehaviour
{
    // Singleton instance
    public static ResourceBoost Instance { get; private set; }

    // Variable to store the resource boost value
    public int pandaDragon = 0;
    public int dragon = 0;
    public int shark = 0;
    public int lion = 0;
    public int parrot = 0;
    public int lizard = 0;
    public int hippo = 0;
    public int monkey = 0;
    public int bull = 0;
    public int catTiger = 0;
    public int pandaParrot = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetBoostValue()
    {
        pandaDragon = 0;
        dragon = 0;
        shark = 0;
        lion = 0;
        parrot = 0;
        lizard = 0;
        hippo = 0;
        monkey = 0;
        bull = 0;
        catTiger = 0;
        pandaParrot = 0;
    }
}