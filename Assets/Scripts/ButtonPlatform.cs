using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlatform : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        if (controller != null)
        {
            // Depress button
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
