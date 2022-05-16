using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float lowerBound = -10;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < lowerBound)
        {
            Destroy(gameObject);
        }
            
    }
}
