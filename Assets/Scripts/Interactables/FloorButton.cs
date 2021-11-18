using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : Activator
{
    [SerializeField]
    private List<GameObject> _validObjectsOnButton = new List<GameObject>();
    private Animator _buttonHeadAnimator;

    // Start is called before the first frame update
    void Start()
    {
        // We assume that the button head is the first child of the floor button. Breaks otherwise
        _buttonHeadAnimator = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Entered");
        // If an object collided with the button that is also allowed to press it
        if (((other.GetComponent("PickupObject") as PickupObject) != null
            && (other.GetComponent("PickupObject") as PickupObject).canPressFloorButtons)
            || (other.transform == GameObject.FindGameObjectWithTag("Erin").transform.parent)
            || (other.transform == GameObject.FindGameObjectWithTag("Eagle").transform.parent))
        {
            _validObjectsOnButton.Add(other.gameObject);
            isButtonActivated = true;
            _buttonHeadAnimator.SetBool("isButtonActivated", true);
            //Debug.Log("Object placed on button");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Left");
        if (((other.GetComponent("PickupObject") as PickupObject) != null
            && (other.GetComponent("PickupObject") as PickupObject).canPressFloorButtons)
            || (other.transform == GameObject.FindGameObjectWithTag("Erin").transform.parent)
            || (other.transform == GameObject.FindGameObjectWithTag("Eagle").transform.parent))
        {
            _validObjectsOnButton.Remove(other.gameObject);

            // If nothing is on the button anymore, deactivate it
            if (_validObjectsOnButton.Count < 1)
            {
                isButtonActivated = false;
                _buttonHeadAnimator.SetBool("isButtonActivated", false);
                //Debug.Log("Button now off");
            }
            //Debug.Log("Object taken off button");
        }
    }
}
