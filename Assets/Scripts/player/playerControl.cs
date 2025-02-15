﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    public float defaultSpeed = 10, fastSpeed = 20, speed = 10, sensitivity = 10, jumpSpeed = 10;
    private float seconds = 0, tallHeight = 10f, mediumHeight = 5, smallHeight = 2f;
    CharacterController cc;
    HealthPoint hp;

    void Start()
    {
        hp = GetComponent<HealthPoint>();
        Cursor.lockState = CursorLockMode.Locked;
        cc = GetComponent<CharacterController>();
    }

    Vector3 newPoint;
    public Animator anim;
    void Update()
    {
        newPoint.Set(
            Input.GetAxis("Horizontal") * speed,
            (cc.isGrounded ? Input.GetAxis("Jump") * jumpSpeed : newPoint.y),
            Input.GetAxis("Vertical") * speed
        );

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = fastSpeed;
        }
        else speed = defaultSpeed;

        newPoint += Physics.gravity * Time.deltaTime;
        cc.Move(transform.rotation * newPoint * Time.deltaTime);
        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);

        // control Down
        if (!cc.isGrounded)
        {
            seconds += Time.deltaTime;
        }
        else
        {
            if (seconds > tallHeight)
            {
                hp.Health -= 50;
            }
            else if (seconds > mediumHeight)
            {
                hp.Health -= 25;
            }
            else if (seconds > smallHeight)
            {
                hp.Health -= 10;
            }
            seconds = 0;
        }
    }
}
