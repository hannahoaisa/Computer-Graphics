using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public bool canErinInteract = true;
    public bool canEagleInteract = false;
    public bool isGameWon;                                               // Will be read by other scripts
    
    private bool _newInteraction;
    private string _characterName;

    // Start is called before the first frame update
    void Start()
    {
        isGameWon = false;
    }

    private void Update()
    {
        CheckInteractions();

        // If Erin is near the objective and she pressed it, end the game
        if (_newInteraction)
        {
            isGameWon = true;
        }
    }

    private void CheckInteractions()
    {
        // If Erin interacted with it
        if (gameObject.tag == "ErinHit"
            && canErinInteract)
        {
            //Debug.Log("Erin interacted");
            gameObject.tag = "Untagged";
            _newInteraction = true;
            _characterName = "Erin";
        }
        // If Eagle interacted with it
        else if (gameObject.tag == "EagleHit"
            && canEagleInteract)
        {
            //Debug.Log("Eagle interacted");
            gameObject.tag = "Untagged";
            _newInteraction = true;
            _characterName = "Eagle";
        }
        // If someone interacted with it but isn't allowed to
        else if (gameObject.tag != "Untagged")
        {
            //Debug.Log("Unauthorized interaction");
            gameObject.tag = "Untagged";
        }
    }
}
