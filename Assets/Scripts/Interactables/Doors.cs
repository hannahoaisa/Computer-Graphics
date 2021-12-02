using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public Activator[] activatorsAND;
    public Activator[] activatorsOR;
    public AudioSource openDoorSound;
    public AudioSource closeDoorSound;
    public bool isButtonActivated;
    public bool areDoorsOpen;

    private GameObject _doorOne;
    private GameObject _doorTwo;
    private Animator _doorOneAnimator;
    private Animator _doorTwoAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _doorOne = gameObject.transform.GetChild(0).gameObject;
        _doorTwo = gameObject.transform.GetChild(1).gameObject;
        _doorOneAnimator = _doorOne.GetComponent<Animator>();
        _doorTwoAnimator = _doorTwo.GetComponent<Animator>();
        areDoorsOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        // The following inputs must ALL be on for door to open
        foreach (Activator input in activatorsAND)
        {
            isButtonActivated = true;

            // Sets to false as soon as it finds one inactive input
            if (!input.isButtonActivated)
            {
                isButtonActivated = false;
                break;
            }
        }

        // Only ONE of the following inputs must be on for the door to open
        foreach (Activator input in activatorsOR)
        {
            // Sets to true as soon as it finds one active input
            if (input.isButtonActivated)
            {
                isButtonActivated = true;
                break;
            }
        }

        // Open and close doors
        if (!areDoorsOpen
            && isButtonActivated)
        {
            areDoorsOpen = true;
            StartCoroutine(openDoors());
        }
        else if (areDoorsOpen
            && !isButtonActivated)
        {
            areDoorsOpen = false;
            StartCoroutine(closeDoors());
        }
    }

    IEnumerator openDoors()
    {
        openDoorSound.Play();
        yield return new WaitForSeconds(0.1f);
        _doorOneAnimator.SetTrigger("Button Press");
        _doorTwoAnimator.SetTrigger("Button Press");
        //_doorOne.SetActive(false);
        //_doorTwo.SetActive(false);
    }

    IEnumerator closeDoors()
    {
        closeDoorSound.Play();
        yield return new WaitForSeconds(0.1f);
        _doorOneAnimator.SetTrigger("Timer Done");
        _doorTwoAnimator.SetTrigger("Timer Done");
        //_doorOne.SetActive(true);
        //_doorTwo.SetActive(true);
    }
}
