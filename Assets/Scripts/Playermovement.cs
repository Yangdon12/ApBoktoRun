using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    public Rigidbody playerMovement;
    public float speed;

    public Vector3 startPosition;
    public Vector3 endPosition;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;
    private CharacterController _controller;
    private Vector3 playerVelocity;
    bool hasJump;
    public GameObject currentPath;

    private void Start()
    {
        playerMovement = GetComponent<Rigidbody>();
        _controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {

        PlayerMove();



        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endPosition = Input.mousePosition;
            Swipe();
        }

    }

    private void Swipe()
    {
        var xdisplacement = startPosition.x - endPosition.x;
        var ydisplacement = startPosition.y - endPosition.y;

        if (Math.Abs(xdisplacement) > Math.Abs(ydisplacement))
        {
            if (startPosition.x - endPosition.x < 0)
            {
                this.transform.Rotate(0, 90, 0);
                Debug.Log("swipe right");

            }
            else
            {
                this.transform.Rotate(0, -90, 0);
                Debug.Log("swipe left");

            }
        }
        else
        {
            if (startPosition.y - endPosition.y < 0)
            {
                Debug.Log("swipe up");
                //playerMovement.velocity = new Vector3(0, 30, 0);
                //playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                hasJump = true;
            }

            else
            {
               // playerMovement.velocity = new Vector3(0, 26, 0);
                Debug.Log("swipe downj");
            }
        }



    }

    private void PlayerMove()
    {
        if (_controller.isGrounded)
        {
            playerVelocity.y = 0;

            if (Input.GetKeyDown(KeyCode.Space) || hasJump)
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                hasJump = false;
            }
        }
        else
        {
            playerVelocity.y += gravity * Time.deltaTime;
        }
        playerVelocity = transform.forward + Vector3.up * playerVelocity.y;
        _controller.Move(playerVelocity * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentPath = collision.transform.gameObject;
        Debug.Log(currentPath);
    }

}
