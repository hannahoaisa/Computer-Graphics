using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;
    public Animator playerAnimator;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public bool isWalking;
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    public Gravity gravity;
    public ConstantForce customGravity;         // Using this so we're not affecting global gravity

    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        playerAnimator.SetBool("isWalking", false);
    }

    void FixedUpdate()
    {
        // Apply custom gravity.
        customGravity.force = rigidbody.mass * gravity.strength * gravity.direction;
        //Physics.gravity = gravity.direction * gravity.strength * rigidbody.mass;

        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        if (targetVelocity.x > 0.01
            || targetVelocity.y > 0.01)
        {
            playerAnimator.SetBool("isWalking", true);
        }
        else
        {
            playerAnimator.SetBool("isWalking", false);
        }

        // Apply movement.
        transform.Translate(targetVelocity.x * Time.deltaTime, 0, targetVelocity.y * Time.deltaTime);

    }
}