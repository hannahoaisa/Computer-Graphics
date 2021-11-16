using System.Collections.Generic;
using UnityEngine;

public class EagleMove : MonoBehaviour
{
    public float speed = 5;
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
        //eagleAnimator.SetBool("isWalking", false);
    }

    void FixedUpdate()
    {
        // Stop movement if we're controlling Erin
        if (gameObject.tag == "InactiveCharacter")
        {
            return;
        }

        // Get targetVelocity from input.
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal") * speed * 50, Input.GetAxis("Fly") * speed * 50, Input.GetAxis("Vertical") * speed * 50);

        // Apply movement.
        Vector3 forceVector = new Vector3(targetVelocity.x * Time.deltaTime, targetVelocity.y * Time.deltaTime, targetVelocity.z * Time.deltaTime);
        rigidbody.velocity = transform.rotation * forceVector;

    }
}