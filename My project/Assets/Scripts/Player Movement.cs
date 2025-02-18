using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 100f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

    }

    void FixedUpdate()
    {
        transform.position += new Vector3(horizontalMove * speed, 0, 0);

        //if (verticalMove > 0 && Mathf.Abs(rigidbody2D.linearVelocityY) < .001f )
        //{
        //    rigidbody2D.linearVelocityY += verticalMove * jumpForce;
        //}
        //else
        //{
        //    rigidbody2D.linearVelocityY = 0;
        //}

    }

}
