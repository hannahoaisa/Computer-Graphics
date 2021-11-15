using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gravity : MonoBehaviour
{
    public float strength;
    public Vector3 direction;
    public Vector3 normal;
    public bool isPaused;
    public bool isGravitySwitch = false;
    public Transform character;
    public UIManager uiScript;
    public AudioSource gravitySound;
    public GravityUI gravUI;
    public MouseOverGrav mouseGravUp;
    public MouseOverGrav mouseGravForward;
    public MouseOverGrav mouseGravLeft;
    public MouseOverGrav mouseGravRight;
    public Vector3 myForward;

    [SerializeField]
    private Vector3 _erinForward;

    // Start is called before the first frame update
    void Start()
    {
        strength = 9.8f;
        direction = Vector3.down;
        normal = -direction;
        isPaused = false;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 flattenedForward = getFlattenedForward();
        myForward = flattenedForward;
        isPaused = uiScript.isPaused;
        isGravitySwitch = gravUI.gravityChange;
        if (!isPaused || isGravitySwitch)
        {

            _erinForward = findErinForward();
            Debug.Log("MouseUp: " + Input.GetMouseButtonUp(1) + "; ChangeUp: " + mouseGravUp.upChange);

            // CHANGING GRAVITY WITH UI
            // Flip
            if (Input.GetMouseButtonUp(1) && mouseGravUp.upChange){
                Debug.Log("Flip");
                UpdateGravity(normal);
            }
            // Forward
            else if (Input.GetMouseButtonUp(1) && mouseGravForward.forwardChange)
            {
                UpdateGravity(_erinForward);
                Debug.Log("Forward");
            }
            // Right
            else if (Input.GetMouseButtonUp(1) && mouseGravRight.rightChange)
            {
                UpdateGravity(Vector3.Cross(normal, _erinForward));
                Debug.Log("Right");
            }
            // Left
            else if (Input.GetMouseButtonUp(1) && mouseGravLeft.leftChange)
            {
                UpdateGravity(Vector3.Cross(_erinForward, normal));
                Debug.Log("Left");
            }

            // CHANGING GRAVITY WITH 1, 2, 3, 4, 5, 6
            // Down (default gravity)
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                UpdateGravity(Vector3.down);
                //Debug.Log("Down");
            }
            // Up
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                UpdateGravity(Vector3.up);
                //Debug.Log("Up");
            }
            // North
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                UpdateGravity(Vector3.forward);
                //Debug.Log("North");
            }
            // East
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                UpdateGravity(Vector3.right);
                //Debug.Log("East");
            }
            // South
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                UpdateGravity(Vector3.back);
                //Debug.Log("South");
            }
            // West
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                UpdateGravity(Vector3.left);
                //Debug.Log("West");
            }

            // Set gravity direction to be the negative of the normal vector
            direction = -normal;
        }
    }

    // Changes the frame as gravity changes
    void UpdateGravity(Vector3 NewDirection)
    {
        // Do nothing if we're changing to the same gravity
        if (NewDirection == direction)
        {
            return;
        }
        gravitySound.Play();

        Physics.gravity = NewDirection * strength;

        Vector3 flattenedForward = getFlattenedForward();
        Vector3 newForward = new Vector3();

        // 180 degree rotation along the character's right axis, e.g. flip upside down
        // Ex: Pulling Down to pulling Up
        if (NewDirection == -direction)
        {
            newForward = Quaternion.AngleAxis(180f, character.right) * flattenedForward;
        }
        // -90 degree rotation along right axis
        // Ex: Pulling Down to pulling North
        else if (NewDirection == Quaternion.AngleAxis(-90f, Vector3.right) * direction)
        {
            newForward = Quaternion.AngleAxis(-90f, Vector3.right) * flattenedForward;
        }
        // 90 degree rotation along right axis
        // Ex: Pulling Down to pulling South
        else if (NewDirection == Quaternion.AngleAxis(90f, Vector3.right) * direction)
        {
            newForward = Quaternion.AngleAxis(90f, Vector3.right) * flattenedForward;
        }
        // -90 degree rotation along up axis
        // Ex: Pulling West to pulling North
        else if (NewDirection == Quaternion.AngleAxis(-90f, Vector3.up) * direction)
        {
            newForward = Quaternion.AngleAxis(-90f, Vector3.up) * flattenedForward;
        }
        // 90 degree rotation along up axis
        // Ex: Pulling West to pulling South
        else if (NewDirection == Quaternion.AngleAxis(90f, Vector3.up) * direction)
        {
            newForward = Quaternion.AngleAxis(90f, Vector3.up) * flattenedForward;
        }
        // -90 degree rotation along forward axis
        // Ex: Pulling Down to pulling West
        else if (NewDirection == Quaternion.AngleAxis(-90f, Vector3.forward) * direction)
        {
            newForward = Quaternion.AngleAxis(-90f, Vector3.forward) * flattenedForward;
        }
        // 90 degree rotation along forward axis
        // Ex: Pulling Down to pulling East
        else if(NewDirection == Quaternion.AngleAxis(90f, Vector3.forward) * direction)
        {
            newForward = Quaternion.AngleAxis(90f, Vector3.forward) * flattenedForward;
        }

        // Set normal to be the opposite of where gravity is now pointing
        normal = -NewDirection;

        // Stop the rotation animation if it's active and then start a new one
        StopCoroutine(smoothCharacterRotation(newForward));
        StartCoroutine(smoothCharacterRotation(newForward));
    }

    IEnumerator smoothCharacterRotation(Vector3 characterForward)
    {
        Quaternion orgRot = character.rotation;
        Quaternion dstRot = Quaternion.LookRotation(characterForward, normal);
        for (float time = 0.0f; time < 1.0f; )          // Change the right side of the inequality to shorten/lengthen animation
        {
            time += Time.deltaTime;
            character.rotation = Quaternion.Slerp(orgRot, dstRot, time);
            yield return new WaitForEndOfFrame();
        }
    }
    Vector3 getFlattenedForward()
    {
        // keepAxes tracks what axes are actually relevant on the rotation given where gravity is pulling.
        // E.g. If gravity is pulling Down, the character's forward vector should not have a y value. 
        // Therefore, only keep x and z axes on the vector.
        Vector3 keepAxes = new Vector3();

        // Find where gravity is pulling before the shift and then update the keepAxes
        if (direction == Vector3.up
            || direction == Vector3.down)
        {
            keepAxes = Vector3.forward + Vector3.right;
        }
        if (direction == Vector3.forward
            || direction == Vector3.back)
        {
            keepAxes = Vector3.up + Vector3.right;
        }
        if (direction == Vector3.right
            || direction == Vector3.left)
        {
            keepAxes = Vector3.up + Vector3.forward;
        }

        // Flatten the forward vector so it ignores values in axes that shouldn't be considered
        // E.g. If the character is rotating from Down to Up, their forward vector will have a non-zero y value
        // that shouldn't be considered if, while rotating, they suddenly get pulled East. It would result in them
        // landing East on one of their corners if the forward vector isn't flattened.
        Vector3 forward = character.forward;
        return new Vector3(forward.x * keepAxes.x, forward.y * keepAxes.y, forward.z * keepAxes.z);
    }

    Vector3 findErinForward()
    {
        Vector3 flattenedForward = getFlattenedForward();
        Vector3 _erinForward = flattenedForward;
        float angleBound = Mathf.Sqrt(2)/2;                   // Value calculated from unit circle

        // Find where gravity is pulling and then figure out where is forward, left, and right for them
        if (direction == Vector3.up
            || direction == Vector3.down)
        {
            // Gravity is pulling UP or DOWN
            if (flattenedForward.x <= angleBound
                && flattenedForward.x >= -angleBound)
            {
                if (flattenedForward.z >= angleBound)
                {
                    _erinForward = Vector3.forward;
                }
                else
                {
                    _erinForward = Vector3.back;
                }
            }
            else if (flattenedForward.z <= angleBound
                && flattenedForward.z >= -angleBound)
            {
                if (flattenedForward.x >= angleBound)
                {
                    _erinForward = Vector3.right;
                }
                else
                {
                    _erinForward = Vector3.left;
                }
            }
        }
        else if (direction == Vector3.forward
            || direction == Vector3.back)
        {

            // Gravity is pulling NORTH or SOUTH
            if (flattenedForward.x <= angleBound
                && flattenedForward.x >= -angleBound)
            {
                if (flattenedForward.y >= angleBound)
                {
                    _erinForward = Vector3.up;
                }
                else
                {
                    _erinForward = Vector3.down;
                }
            }
            else if (flattenedForward.y <= angleBound
                && flattenedForward.y >= -angleBound)
            {
                if (flattenedForward.x >= angleBound)
                {
                    _erinForward = Vector3.right;
                }
                else
                {
                    _erinForward = Vector3.left;
                }
            }
        }
        else if (direction == Vector3.right
            || direction == Vector3.left)
        {
            // Gravity is pulling EAST or WEST
            if ((flattenedForward.y <= angleBound
                && flattenedForward.y >= -angleBound))
            {
                if (flattenedForward.z >= angleBound)
                {
                    _erinForward = Vector3.forward;
                }
                else
                {
                    _erinForward = Vector3.back;
                }
            }
            else if (flattenedForward.z <= angleBound
                && flattenedForward.z >= -angleBound)
            {
                if (flattenedForward.y >= angleBound)
                {
                    _erinForward = Vector3.up;
                }
                else
                {
                    _erinForward = Vector3.down;
                }
            }
        }
        return _erinForward;
    }
}
