using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public bool isGameWon;                                               // Will be read by other scripts

    private GameObject[] _nearbyCharacterObjects = new GameObject[2];    // Slot 0 = Erin; Slot 1 = Eagle
    private GameObject _nearbyCharacter;

    // Start is called before the first frame update
    void Start()
    {
        isGameWon = false;
    }

    // See if a character approached and, if one did, which one
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

    // See if a character left and, if one did, which one
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

    private void Update()
    {
        // See if Erin is near the objective
        if (_nearbyCharacterObjects[0] != null
            && _nearbyCharacterObjects[0].tag == "ActiveCharacter")
        {
            //Debug.Log("Erin");
            _nearbyCharacter = _nearbyCharacterObjects[0];
        }
        // See if Eagle is near the objective
        else if (_nearbyCharacterObjects[1] != null
            && _nearbyCharacterObjects[1].tag == "ActiveCharacter")
        {
            //Debug.Log("Eagle");
            _nearbyCharacter = _nearbyCharacterObjects[1];
        }
        // Either no one is near the objective or, for some bizarre reason, neither are active
        else
        {
            //Debug.Log("No one");
            _nearbyCharacter = null;
        }

        // If Erin is near the objective and she pressed it, end the game
        if (_nearbyCharacter == GameObject.FindGameObjectWithTag("Erin").transform.parent.gameObject
            && Input.GetKeyDown(KeyCode.E))
        {
            isGameWon = true;
        }
        // If Eagle is near the objective and it presses it, tell the player
        else if (_nearbyCharacter == GameObject.FindGameObjectWithTag("Eagle").transform.parent.gameObject
            && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Erin needs to be here!");
        }
    }
}
