using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlatform : MonoBehaviour
{
    public GameObject[] bridgesPrefab;
    private bool pressed = false;
    void OnTriggerEnter(Collider other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        if (controller != null)
        {
            // Depress button
            gameObject.transform.localScale = new Vector3(1, 1, 1);

            // If Blue button, create bridges
            if (gameObject.CompareTag("BlueButton"))
            {
                foreach (GameObject bridge in bridgesPrefab)
                {
                    if (!pressed)
                    {
                        bridge.SetActive(true);
                    }
                        
                }
                pressed = true;
            }

            // If Red button, remove bridges
            if (gameObject.CompareTag("RedButton"))
            {
                foreach (GameObject bridge in bridgesPrefab)
                {
                    if (!pressed)
                    {
                        bridge.SetActive(false);
                    }
                    
                }
                pressed = true;
            }
        }
    }
}
