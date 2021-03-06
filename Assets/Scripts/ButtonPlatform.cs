using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlatform : MonoBehaviour
{
    public GameObject[] bridgesPrefab;
    public GameObject[] oldPatrolPrefab;
    public GameObject[] newPatrolPrefab;
    private bool pressed;

    AudioSource audioSource;
    public AudioClip buttonPressedClip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        // Buttons can be pressed by player or blocks. Once pressed, cannot be pressed again.
        if ((controller != null && !pressed) || (other.gameObject.CompareTag("Block") && !pressed))
        {
            // Depress button (reduce y scale)
            gameObject.transform.localScale = new Vector3(1, 1, 1);

            // Play Activation Sound
            audioSource.PlayOneShot(buttonPressedClip);

            // If Blue button, add bridges, remove old patrol point, add new patrol point
            if (gameObject.CompareTag("BlueButton"))
            {
                ActivatePrefabs(true, false, true);
            }

            // If Red button, remove bridges, add old patrol point, remove new patrol point 
            else if (gameObject.CompareTag("RedButton"))
            {
                ActivatePrefabs(false, true, false);
            }

            pressed = true;
        }
    }

    private void ActivatePrefabs(bool isActiveBridge, bool isActiveOldPatrol, bool isActiveNewPatrol)
    {
        foreach (GameObject bridge in bridgesPrefab)
        {
            bridge.SetActive(isActiveBridge);
        }
        foreach (GameObject oldPatrol in oldPatrolPrefab)
        {
            oldPatrol.SetActive(isActiveOldPatrol);
        }
        foreach (GameObject newPatrol in newPatrolPrefab)
        {
            newPatrol.SetActive(isActiveNewPatrol);
        }
    }
}
