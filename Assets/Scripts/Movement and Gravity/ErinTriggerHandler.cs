using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErinTriggerHandler : MonoBehaviour
{
    public GameObject standupButton;
    public GameObject topDoor;
    public GameObject bottomDoor;
    public AudioSource buttonPress;
    public AudioSource openDoor;
    public AudioSource closeDoor;
    public AudioSource timerDone;
    public Animator door1;
    public Animator door2;
    public bool gameWon;

    private bool _isNearButton = false;
    private bool _isNearBed = false;
    private Material _standupButtonScreen;
    // Start is called before the first frame update
    void Start()
    {
        _standupButtonScreen = standupButton.GetComponent<Renderer>().materials[1];
        gameWon = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)
            && _isNearButton)
        {
            Debug.Log("Button Pushed");
            StartCoroutine(ButtonTimer());
            buttonPress.Play();
        }
        if (Input.GetKeyDown(KeyCode.E)
            && _isNearBed)
        {
            Debug.Log("Game over");
            gameWon = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "StandupButtonTrigger")
        {
            _isNearButton = true;
            Debug.Log("Near Button");
        }
        if (other.tag == "EndGoal")
        {
            _isNearBed = true;
            Debug.Log("Near Bed");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "StandupButtonTrigger")
        {
            _isNearButton = false;
        }
    }

    IEnumerator ButtonTimer()
    {
        //topDoor.SetActive(false);
        //bottomDoor.SetActive(false);
        openDoor.Play();
        _standupButtonScreen.SetColor("_Color", new Color32(0xf2, 0x23, 0x13, 0xff));
        yield return new WaitForSeconds(0.5f);
        door1.SetTrigger("Button Press");
        door2.SetTrigger("Button Press");
        yield return new WaitForSeconds(1.2f);
        _standupButtonScreen.SetColor("_Color", new Color32(0x78, 0x11, 0x0a, 0xff));
        yield return new WaitForSeconds(1.2f);
        _standupButtonScreen.SetColor("_Color", Color.black);
        closeDoor.Play();
        timerDone.Play();
        yield return new WaitForSeconds(0.5f);
        door1.SetTrigger("Timer Done");
        door2.SetTrigger("Timer Done");
        //topDoor.SetActive(true);
        //bottomDoor.SetActive(true);
    }
}
