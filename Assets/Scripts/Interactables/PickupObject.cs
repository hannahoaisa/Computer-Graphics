using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public bool canErinPickup = true;
    public bool canEaglePickup = false;
    public float pickupRange = 5f;

    private GameObject _interactablesObject;
    private GameObject[] _nearbyCharacterObjects = new GameObject[2];    // Slot 0 = Erin; Slot 1 = Eagle
    private GameObject _nearbyCharacter;
    private string _nearbyCharacterName;
    private GameObject _characterCamera;
    [SerializeField]
    private string _pickedUpBy;

    // Start is called before the first frame update
    void Start()
    {

        gameObject.GetComponent<BoxCollider>().size = new Vector3(pickupRange, pickupRange, pickupRange);
        _interactablesObject = gameObject.transform.parent.gameObject;
        _pickedUpBy = "No one";
    }

    // Update is called once per frame
    void Update()
    {

        findWhoIsNear();

        // Let Erin pickup object
        if (canErinPickup
            && _nearbyCharacterName == "Erin"
            && _pickedUpBy != "Erin"
            && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Erin pickup");
            _pickedUpBy = "Erin";
            gameObject.transform.parent = _characterCamera.transform;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            gameObject.layer = 6;
            Physics.IgnoreLayerCollision(6, 6);
        }
        // Let Eagle pickup object;
        else if (canEaglePickup
           && _nearbyCharacterName == "Eagle"
           && _pickedUpBy != "Eagle"
           && Input.GetKeyDown(KeyCode.E))
        {
            _pickedUpBy = "Eagle";
        }
        // Let go of object
        else if (_pickedUpBy != "No one"
            && Input.GetKeyDown(KeyCode.E))
        {
            _pickedUpBy = "No one";
            gameObject.transform.parent = _interactablesObject.transform;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.layer = 0;
        }

        /*// Let go early if player gets too far from it
        float distance = Vector3.Distance(transform.position, _nearbyCharacter.transform.position);
        if (distance >= pickupRange
            && _nearbyCharacter == transform.parent.parent.gameObject)
        {
            _pickedUpBy = "No one";
        }*/

        // What to do when someone has the object picked up
        if (_pickedUpBy != "No one")
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        // What to do when no one has the object picked up
        else
        {
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Both Erin and Eagle have an identifier child with a tag of their name
        if (other.gameObject == GameObject.FindGameObjectWithTag("Erin").transform.parent.gameObject)
        {
            // Put Erin into array
            _nearbyCharacterObjects[0] = other.gameObject;
        }
        else if (other.gameObject == GameObject.FindGameObjectWithTag("Eagle").transform.parent.gameObject)
        {
            // Put Eagle into array
            _nearbyCharacterObjects[1] = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // Both Erin and Eagle have an identifier child with a tag of their name
        if (other.gameObject == GameObject.FindGameObjectWithTag("Erin").transform.parent.gameObject)
        {
            // Remove Erin from array
            _nearbyCharacterObjects[0] = null;
        }
        else if (other.gameObject == GameObject.FindGameObjectWithTag("Eagle").transform.parent.gameObject)
        {
            // Remove Eagle from array
            _nearbyCharacterObjects[1] = null;
        }
    }

    private void findWhoIsNear()
    {
        // See if Erin is nearby
        if (_nearbyCharacterObjects[0] != null
            && _nearbyCharacterObjects[0].tag == "ActiveCharacter")
        {
            _nearbyCharacter = _nearbyCharacterObjects[0];
            _nearbyCharacterName = "Erin";
            _characterCamera = GameObject.Find("ErinCamera").gameObject;
        }
        // See if Eagle is nearby
        else if (_nearbyCharacterObjects[1] != null
            && _nearbyCharacterObjects[1].tag == "ActiveCharacter")
        {
            _nearbyCharacter = _nearbyCharacterObjects[1];
            _nearbyCharacterName = "Eagle";
            _characterCamera = GameObject.Find("EagleCamera").gameObject;
        }
        // Either no one is nearby, for some bizarre reason, neither are active (impossible)
        else
        {
            _nearbyCharacter = null;
            _nearbyCharacterName = "No one";
            _characterCamera = null;
        }
    }
}
