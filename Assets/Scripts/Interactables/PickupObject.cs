using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public bool canErinInteract = true;
    public bool canEagleInteract = false;
    public bool canPressFloorButtons = false;
    public float carryRange = 3f;

    private bool _newInteraction;
    private GameObject _interactablesObject;
    private GameObject _character;
    private string _characterName;
    private string _pickedUpBy;

    // Start is called before the first frame update
    void Start()
    {
        _newInteraction = false;
        _interactablesObject = gameObject.transform.parent.gameObject;
        _character = null;
        _characterName = "No one";
        _pickedUpBy = "No one";
    }

    // Update is called once per frame
    void Update()
    {
        CheckInteractions();
        HandlePickups();

        // Layer 8 is the "PickedupObject" layer
        Physics.IgnoreLayerCollision(6, 8);
    }

    // Generic function to check for interactions. Some version of this is in every interactable object's script.
    private void CheckInteractions()
    {
        // If Erin interacted with it
        if (gameObject.tag == "ErinHit"
            && canErinInteract)
        {
            gameObject.tag = "Untagged";
            _newInteraction = true;
            _character = GameObject.Find("Erin");
            _characterName = "Erin";
        }
        // If Eagle interacted with it
        else if (gameObject.tag == "EagleHit"
            && canEagleInteract)
        {
            gameObject.tag = "Untagged";
            _newInteraction = true;
            _character = GameObject.Find("Eagle");
            _characterName = "Eagle";
        }
        // If someone interacted with it but isn't allowed to
        else if (gameObject.tag != "Untagged")
        {
            gameObject.tag = "Untagged";
        }
    }

    private void HandlePickups()
    {
        // Let Erin pickup object
        if (_newInteraction
            && _characterName == "Erin"
            && _character.tag == "ActiveCharacter"
            && _pickedUpBy == "No one")
        {
            _newInteraction = false;
            _pickedUpBy = "Erin";
            gameObject.transform.parent = GameObject.Find("ErinCamera").gameObject.transform;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.layer = 8;
        }
        // Let Eagle pickup object;
        else if (_newInteraction
            && _characterName == "Eagle"
            && _character.tag == "ActiveCharacter"
            && _pickedUpBy == "No one")
        {
            _newInteraction = false;
            _pickedUpBy = "Eagle";
            gameObject.transform.parent = GameObject.Find("EagleCamera").gameObject.transform;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.layer = 8;
        }
        // Let go of object if they click E again
        else if (_pickedUpBy != "No one"
            && _character.tag == "ActiveCharacter"
            && _newInteraction)
        {
            DropObject();
        }

        // What to do when someone has the object picked up
        if (_pickedUpBy != "No one")
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            // Let go early if object gets too far from the character
            float distance = Vector3.Distance(transform.position, transform.parent.position);
            if (distance >= carryRange)
            {
                DropObject();
            }
        }
    }

    private void DropObject()
    {
        _newInteraction = false;
        _character = null;
        _characterName = "No one";
        _pickedUpBy = "No one";
        gameObject.transform.parent = _interactablesObject.transform;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.layer = 7;
    }
}
