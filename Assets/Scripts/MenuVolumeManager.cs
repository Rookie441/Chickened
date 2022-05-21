using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuVolumeManager : MonoBehaviour
{
    public AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        MainManager.Instance.volumeLevel = audioSource.volume;
    }
}
