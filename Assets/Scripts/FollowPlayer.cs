using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private PlayerController playerControllerScript;
    public GameObject player;
    private Vector3 offset = new Vector3(0, 4, -4);
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    void Update()
    {
        if (!playerControllerScript.gameOver)
            transform.position = player.transform.position + offset;
    }
}
