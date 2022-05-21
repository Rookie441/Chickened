using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject player;
    private Vector3 offset = new Vector3(0, 4, -4);

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (!gameManager.isGameOver)
            transform.position = player.transform.position + offset;
    }
}
