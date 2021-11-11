﻿using System.Collections.Generic;
using UnityEngine;

public class EagleMove : MonoBehaviour
{
    public float speed = 5;
    public float flySpeed = 5;
    public Animator eagleAnimator;

    public bool isWalking;
    public bool isDead;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Deadly")
        {
            isDead = true;
        }
    }

    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        eagleAnimator.SetBool("isWalking", false);
    }

    void FixedUpdate()
    {
        // Stop movement if we're controlling Erin
        if (gameObject.tag == "InactiveCharacter")
        {
            return;
        }

        // Get targetVelocity from input.
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Fly") * flySpeed, Input.GetAxis("Vertical") * speed);

        if (targetVelocity.x > 0.01
            || targetVelocity.y > 0.01
            || targetVelocity.z > 0.01)
        {
            eagleAnimator.SetBool("isWalking", true);
        }
        else
        {
            eagleAnimator.SetBool("isWalking", false);
        }

        // Apply movement.
        transform.Translate(targetVelocity.x * Time.deltaTime, targetVelocity.y * Time.deltaTime, targetVelocity.z * Time.deltaTime);

    }
}