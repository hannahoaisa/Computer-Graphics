using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ErinLook : MonoBehaviour
{
    public Transform character;
    public float sensitivity = 12;
    public float smoothing = 1.5f;
    public float pickupRange = 3f;
    public Button resumeButton;
    public bool isPaused = false;

    public Gravity gravity;
    public UIManager uiScript;

    [SerializeField]
    private Vector2 _velocity;
    [SerializeField]
    private Vector2 _frameVelocity;

    void Reset()
    {
        // Get the character from the FirstPersonMovement in parents.
        character = GetComponentInParent<ErinMove>().transform;
    }

    void Start()
    {
        // Lock the mouse cursor to the game screen.
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (character.tag == "ActiveCharacter"
            && Input.GetKeyUp(KeyCode.E))
        {
            Interact();
        }

        isPaused = uiScript.isPaused;
        if (!isPaused
            && character.tag == "ActiveCharacter")
        {
            // Get smooth velocity.
            Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X") * PlayerPrefs.GetFloat("sensitivity", 0.25f), 
                Input.GetAxisRaw("Mouse Y") * PlayerPrefs.GetFloat("sensitivity", 0.25f));
            Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
            _frameVelocity = Vector2.Lerp(_frameVelocity, rawFrameVelocity, 1 / smoothing);
            float oldX = _velocity.x;
            _velocity += _frameVelocity;

            _velocity.y = Mathf.Clamp(_velocity.y, -90, 90);

            // Rotate camera up-down and controller left-right from velocity.
            transform.localRotation = Quaternion.AngleAxis(-_velocity.y, Vector3.right);       // Works perfectly (I think)
            character.RotateAround(character.position, character.up, _velocity.x - oldX);
        }
    }

    void Interact()
    {
        RaycastHit hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange))
        {
            //Debug.Log("Hit");
            StopCoroutine(TriggerAHit(hit.transform.gameObject));
            StartCoroutine(TriggerAHit(hit.transform.gameObject));
        }

        // If Erin's camera has child objects then she has them picked up. Automatically interect with them.
        foreach (Transform childObject in GameObject.Find("ErinCamera").transform)
        {
            if (hit.transform != childObject)
            {
                StopCoroutine(TriggerAHit(childObject.gameObject));
                StartCoroutine(TriggerAHit(childObject.gameObject));
            }
        }
    }

    IEnumerator TriggerAHit(GameObject hitObject)
    {
        string prevTag = hitObject.tag;
        hitObject.tag = "ErinHit";

        // Give 10 milliseconds for the object to catch the interaction
        yield return new WaitForSeconds(.01f);

        // If 10 ms have passed and the object still has the tag, then it likely doesn't have a script to revert it itself
        if (hitObject.tag == "ErinHit")
        {
            //Debug.Log("Reverted");
            hitObject.tag = prevTag;
        }
    }
}
