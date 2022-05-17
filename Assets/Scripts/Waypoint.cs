using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        if (controller != null)
        {
            // to-do: Level complete animation
            //other.gameObject.transform.position = transform.position;
            //other.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            //disable player movement, play animation
            controller.CompleteLevel();
        }
    }
}
