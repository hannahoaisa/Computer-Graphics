using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravity : MonoBehaviour
{
    public float strength = 9.8f;
    public Vector3 direction;
    public Transform character;
    bool jumping = false;

    // Define the frame that the character is in.
    // Each vector changes depending on how the direction changes.
    public Vector3 right = Vector3.right;
    public Vector3 up = Vector3.up;
    public Vector3 forward = Vector3.forward;

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.down;
    }

    // Update is called once per frame
    void Update()
    {
        // Down (normal)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpdateFrame(Vector3.down);
            direction = Vector3.down;
        }
        // Up
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UpdateFrame(Vector3.up);
            direction = Vector3.up;
        }
        // North
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UpdateFrame(Vector3.forward);
            direction = Vector3.forward;
        }
        // East
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UpdateFrame(Vector3.right);
            direction = Vector3.right;
        }
        // South
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UpdateFrame(Vector3.back);
            direction = Vector3.back;
        }
        // West
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            UpdateFrame(Vector3.left);
            direction = Vector3.left;
        }
    }

    // Changes the frame as gravity changes
    void UpdateFrame(Vector3 NewDirection)
    {
        Vector3 temp;

        // Do nothing if we're changing to the same gravity
        if (NewDirection == direction)
        {
            return;
        }
        // 90 degree rotation along right axis
        // Ex: Pulling Down to pulling North
        else if (NewDirection == forward)
        {
            temp = up;
            up = forward * -1;
            forward = temp;
        }
        // -90 degree rotation along right axis
        // Ex: Pulling Down to pulling South
        else if(NewDirection == -forward)
        {
            temp = up;
            up = forward;
            forward = temp * -1;
        }
        // 180 degree rotation along right axis
        // Ex: Pulling Down to pulling Up
        else if(NewDirection == -direction)
        {
            up = -up;
            forward = -forward;
        }
        // 90 degree rotation along forward axis
        // Ex: Pulling Down to pulling East
        else if(NewDirection == right)
        {
            temp = up;
            up = -right;
            right = temp;
        }
        // -90 degree rotation along forward axis
        // Ex: Pulling Down to pulling West
        else if(NewDirection == -right)
        {
            temp = up;
            up = right;
            right = -temp;
        }
        character.rotation = Quaternion.LookRotation(forward, up);
    }

}
