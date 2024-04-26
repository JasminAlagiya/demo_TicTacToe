using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DDOL : MonoBehaviour
{
    public static DDOL instance;

    public SelectionMode selection;

    public SelectionGridModa selectionGrid;

    private void Awake()
    {
        if(instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        if (PlayerPrefs.HasKey("Srossj") == false)
        {
            PlayerPrefs.SetInt("Srossj", 0);
        }
        else
        {
            PlayerPrefs.GetInt("Srossj");
        }

        if (PlayerPrefs.HasKey("Noughtj") == false)
        {
            PlayerPrefs.SetInt("Noughtj", 0);
        }
        else
        {
            PlayerPrefs.GetInt("Noughtj");
        }
    }
}

public enum SelectionMode
{
    none,
    PlayerVsPlayer,
    PlayerVsBot
}

public enum SelectionGridModa
{
    none,
    ThreeVsThree,
    FiveVsFive,
    SevenVsSeven
}

public enum TurnSelection
{
    none,
    Cross,
    Nought
}

