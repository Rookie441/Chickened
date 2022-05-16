using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }
}
