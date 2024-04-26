using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UiManeger : MonoBehaviour
{
    public GameObject HomePanel, SelectionPanel, GridPanel, SettingPanel;

    public Slider MusicSlider, SoundSlider;

    public Image MusicImg, SoundImd;

    public Sprite MusicOnSprite,MusicOffSprite,SoundOnSprite,SoundOffSprite;

    float increaseValue = 0.2f;

    public Button MusicIncreaseBtn, MusicDecreaseBtn, SoundIncreaseBtn, SoundDecreaseBtn;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("MusicPref") == false)
        {
            PlayerPrefs.SetFloat("MusicPref", 1);
        }
        else
        {
            PlayerPrefs.GetFloat("MusicPref");
        }
        if (PlayerPrefs.HasKey("SoundPref") == false)
        {
            PlayerPrefs.SetFloat("SoundPref", 1);
        }
        else
        {
            PlayerPrefs.GetFloat("SoundPref");
        }
    }

    private void Start()
    {
       //HomePanel.SetActive(true);
        //SettingPanel.SetActive(false);
        //SelectionPanel.SetActive(false);
        //GridPanel.SetActive(false);
        MusicSlider.value = PlayerPrefs.GetFloat("MusicPref");
        MusicManager.Instance.audioSource.volume = MusicSlider.value;
        SoundSlider.value = PlayerPrefs.GetFloat("SoundPref");
        SoundManager.Instance.audioSource.volume = SoundSlider.value;
    }
    public void PlayBtnClick()
    {
        //HomePanel.SetActive(false);
        //SelectionPanel.SetActive(true);
        HomePanel.transform.GetChild(0).GetComponent<Animator>().SetBool("IsMove", true);
        HomePanel.transform.GetChild(1).GetComponent<Animator>().SetBool("IsMove", true);
        SelectionPanel.transform.GetChild(0).GetComponent<Animator>().SetBool("IsMove", true);
        SelectionPanel.transform.GetChild(1).GetComponent<Animator>().SetBool("IsMove", true);

        SoundManager.Instance.BtnClickSound();
    }
    public void MusicIncrease()
    {
        if(MusicManager.Instance.audioSource.volume < 1)
        {
            MusicManager.Instance.audioSource.volume += increaseValue;
            MusicSlider.value = MusicManager.Instance.audioSource.volume;
            PlayerPrefs.SetFloat("MusicPref", MusicManager.Instance.audioSource.volume);
            if (MusicSlider.value > 0)
            {
                MusicDecreaseBtn.interactable = true;
                MusicImg.sprite = MusicOffSprite;
            }
            if (MusicSlider.value == 1)
            {
                MusicIncreaseBtn.interactable = false;      
            }
        }
    }
    public void MusicDecrease()
    {
        if(MusicManager.Instance.audioSource.volume > 0)
        {
            MusicIncreaseBtn.interactable = true;
            MusicImg.sprite = MusicOnSprite;

            MusicManager.Instance.audioSource.volume -= increaseValue;
            MusicSlider.value = MusicManager.Instance.audioSource.volume;
            PlayerPrefs.SetFloat("MusicPref", MusicManager.Instance.audioSource.volume);
            if (MusicManager.Instance.audioSource.volume < 0.2f)
            {
                MusicImg.sprite = MusicOffSprite;
                MusicDecreaseBtn.interactable = false;
            }
        }
    }
    public void SoundIncrease()
    {
        if(SoundManager.Instance.audioSource.volume < 1) 
        {
            SoundManager.Instance.audioSource.volume += increaseValue;
            SoundSlider.value = SoundManager.Instance.audioSource.volume;
            PlayerPrefs.SetFloat("SoundPref", SoundManager.Instance.audioSource.volume);
            if (SoundSlider.value > 0)
            {
                SoundDecreaseBtn.interactable = true;
            }
            if(SoundSlider.value == 1)
            {
                SoundIncreaseBtn.interactable = false;
            }
        }
    }
    public void SoundDecrease()
    {
        if(SoundManager.Instance.audioSource.volume > 0)
        {
            SoundIncreaseBtn.interactable = true;
            SoundImd.sprite = SoundOnSprite;
            SoundManager.Instance.audioSource.volume -= increaseValue;
            SoundSlider.value = SoundManager.Instance.audioSource.volume;
            PlayerPrefs.SetFloat("SoundPref", SoundManager.Instance.audioSource.volume);

            if (SoundManager.Instance.audioSource.volume < 0.2f)
            {
                SoundImd.sprite = SoundOffSprite;
                SoundDecreaseBtn.interactable = false;
            }
        }
    }
    public void okBtnClick()
    {
        SettingPanel.SetActive(false);
        HomePanel.transform.GetChild(0).GetComponent<Animator>().SetBool("IsMove", false);
        HomePanel.transform.GetChild(1).GetComponent<Animator>().SetBool("IsMove", false);
        //HomePanel.SetActive(true);
    }
    public void SettingBtnClick()
    {
        SettingPanel.SetActive(true);
        HomePanel.transform.GetChild(0).GetComponent<Animator>().SetBool("IsMove", true);
        HomePanel.transform.GetChild(1).GetComponent<Animator>().SetBool("IsMove", true);
        //HomePanel.SetActive(false);
        SoundManager.Instance.BtnClickSound();

    }
    public void SelectionBtnClick(string BtnName)
    {
        SelectionPanel.transform.GetChild(0).GetComponent<Animator>().SetBool("IsMove", false);
        SelectionPanel.transform.GetChild(1).GetComponent<Animator>().SetBool("IsMove", false);
        GridPanel.transform.GetChild(0).GetComponent<Animator>().SetBool("IsMove", true);
        GridPanel.transform.GetChild(1).GetComponent<Animator>().SetBool("IsMove", true);
        SoundManager.Instance.BtnClickSound();

        //SelectionPanel.SetActive(false);
        //GridPanel.SetActive(true);
        if (BtnName == "VsPlayer")
        {
            DDOL.instance.selection = SelectionMode.PlayerVsPlayer;
        }
        else if (BtnName == "VsBot")
        {
            DDOL.instance.selection = SelectionMode.PlayerVsBot;

        }
    }
    public void GridSelectionBtnClick(string GridBtnName)
    {
        SoundManager.Instance.BtnClickSound();

        if (GridBtnName == "Three")
        {
            DDOL.instance.selectionGrid = SelectionGridModa.ThreeVsThree;
        }
        else if (GridBtnName == "Five")
        {
            DDOL.instance.selectionGrid = SelectionGridModa.FiveVsFive;

        }
        else if (GridBtnName == "Seven")
        {
            DDOL.instance.selectionGrid = SelectionGridModa.SevenVsSeven;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);

    }

}
