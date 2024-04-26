using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;

    public static SoundManager Instance;

    public AudioClip BtnClick, ResultClik, ObjPutSound;

    private void Awake()
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
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void BtnClickSound()
    {
        audioSource.PlayOneShot(BtnClick);
    }
    public void WinClickSound()
    {
        audioSource.PlayOneShot(ResultClik);
    }
    public void ObjectPutSound()
    {
        audioSource.PlayOneShot(ObjPutSound);
    }
}
