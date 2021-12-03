using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EagleLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    public float sensitivity = 12;
    public float smoothing = 1.5f;
    public float pickupRange = 3f;
    public Button resumeButton;
    public bool isPaused = false;

    public Vector2 velocity;
    public Vector2 frameVelocity;

    public Gravity gravity;
    public UIManager uiScript;

    void Reset()
    {
        // Get the character from the FirstPersonMovement in parents.
        character = GetComponentInParent<EagleMove>().transform;
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
            Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X") * PlayerPrefs.GetFloat("sensitivity", 1),
                Input.GetAxisRaw("Mouse Y") * PlayerPrefs.GetFloat("sensitivity", 1));
            Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
            frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
            float oldX = velocity.x;
            velocity += frameVelocity;

            velocity.y = Mathf.Clamp(velocity.y, -90, 90);

            // Rotate camera up-down and controller left-right from velocity.
            transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);       // Works perfectly (I think)
            character.RotateAround(character.position, character.up, velocity.x - oldX);
        }
    }

    void Interact()
    {RaycastHit hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange))
        {
            //Debug.Log("Hit");
            StopCoroutine(TriggerAHit(hit.transform.gameObject));
            StartCoroutine(TriggerAHit(hit.transform.gameObject));
        }

        // If Eagle's camera has child objects then it has them picked up. Automatically interect with them.
        foreach (Transform childObject in GameObject.Find("EagleCamera").transform)
        {
            //Debug.Log("Eagle child");
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
        hitObject.tag = "EagleHit";

        // Give 10 milliseconds for the object to catch the interaction
        yield return new WaitForSeconds(.01f);

        // If 10 ms have passed and the object still has the tag, then it likely doesn't have a script to revert it itself
        if (hitObject.tag == "EagleHit")
        {
            hitObject.tag = prevTag;
            //Debug.Log("Reverted");
        }
    }
}
