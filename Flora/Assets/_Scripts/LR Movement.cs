using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LRMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Rigidbody2D myRigidbody2D;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        int moveDirection = 0;
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += 1;
        }

        myRigidbody2D.velocity = new Vector2(moveDirection * moveSpeed, myRigidbody2D.velocity.y);
    }

}
