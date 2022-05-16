using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator playerAnim;

    private float horizontalInput;
    private float forwardInput;
    [SerializeField] private float speed = 1;

    Vector3 lookDirection = new Vector3(1, 0, 0);
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");      

        Vector3 move = new Vector3(horizontalInput, 0, forwardInput);

        // Check if player is moving
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.z, 0.0f))
        {
            lookDirection.Set(move.x, move.y, move.z);
            lookDirection.Normalize();
            playerAnim.SetBool("Walk", true);
        }
        // Player is not moving
        else
        {
            playerAnim.SetBool("Walk", false);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * 40f);
        
        

    }

    private void FixedUpdate()
    {
        Vector3 position = rigidbody.position;
        position.x = position.x + speed * horizontalInput * Time.deltaTime;
        position.z = position.z + speed * forwardInput * Time.deltaTime;

        rigidbody.MovePosition(position);
    }
}
