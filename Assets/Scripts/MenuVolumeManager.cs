using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuVolumeManager : MonoBehaviour
{
    public AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //to-do: load volume slider position from saved file attribute
    }

    void Update()
    {
        MainManager.Instance.volumeLevel = audioSource.volume;
    }
}
