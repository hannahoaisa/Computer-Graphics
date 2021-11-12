using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public ButtonTrigger buttonTrigger;
    public GameObject topDoor;
    public GameObject bottomDoor;
    public AudioSource openDoorSound;
    public AudioSource closeDoorSound;
    public bool isButtonActivated;
    public bool areDoorsOpen;

    private Animator _topDoorAnimator;
    private Animator _bottomDoorAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _topDoorAnimator = topDoor.GetComponent<Animator>();
        _bottomDoorAnimator = bottomDoor.GetComponent<Animator>();
        areDoorsOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        isButtonActivated = buttonTrigger.isButtonActivated;
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
        _topDoorAnimator.SetTrigger("Button Press");
        _bottomDoorAnimator.SetTrigger("Button Press");
        //topDoor.SetActive(false);
        //bottomDoor.SetActive(false);
    }

    IEnumerator closeDoors()
    {
        closeDoorSound.Play();
        yield return new WaitForSeconds(0.5f);
        _topDoorAnimator.SetTrigger("Timer Done");
        _bottomDoorAnimator.SetTrigger("Timer Done");
        //topDoor.SetActive(true);
        //bottomDoor.SetActive(true);
    }
}
