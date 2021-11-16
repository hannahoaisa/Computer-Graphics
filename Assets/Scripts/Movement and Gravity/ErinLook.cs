using UnityEngine;
using UnityEngine.UI;

public class ErinLook : MonoBehaviour
{
    public Transform character;
    public float sensitivity = 12;
    public float smoothing = 1.5f;
    public Button resumeButton;
    public bool isPaused = false;

    public Gravity gravity;
    public UIManager uiScript;

    [SerializeField]
    private Vector2 velocity;
    [SerializeField]
    private Vector2 frameVelocity;

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
        isPaused = uiScript.isPaused;
        if (!isPaused
            && character.tag == "ActiveCharacter")
        {
            // Get smooth velocity.
            Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X") * PlayerPrefs.GetFloat("sensitivity", 0.25f), 
                Input.GetAxisRaw("Mouse Y") * PlayerPrefs.GetFloat("sensitivity", 0.25f));
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
}
