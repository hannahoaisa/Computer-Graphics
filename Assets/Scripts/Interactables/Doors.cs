using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public StandupButton button;
    public AudioSource openDoorSound;
    public AudioSource closeDoorSound;
    public bool isButtonActivated;
    public bool areDoorsOpen;

    private Animator _doorOneAnimator;
    private Animator _doorTwoAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _doorOneAnimator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        _doorTwoAnimator = gameObject.transform.GetChild(1).GetComponent<Animator>();
        areDoorsOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        isButtonActivated = button.isButtonActivated;
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
        yield return new WaitForSeconds(0.5f);
        _doorOneAnimator.SetTrigger("Button Press");
        _doorTwoAnimator.SetTrigger("Button Press");
        //topDoor.SetActive(false);
        //bottomDoor.SetActive(false);
    }

    IEnumerator closeDoors()
    {
        closeDoorSound.Play();
        yield return new WaitForSeconds(0.5f);
        _doorOneAnimator.SetTrigger("Timer Done");
        _doorTwoAnimator.SetTrigger("Timer Done");
        //topDoor.SetActive(true);
        //bottomDoor.SetActive(true);
    }
}
