using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTriggerHandler : MonoBehaviour
{
    public GameObject standupButton;
    public GameObject topDoor;
    public GameObject bottomDoor;

    private bool _isNearButton = false;
    private bool _isNearBed = false;
    private Material _stanupButtonScreen;
    // Start is called before the first frame update
    void Start()
    {
        _stanupButtonScreen = standupButton.GetComponent<Renderer>().materials[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)
            && _isNearButton)
        {
            Debug.Log("Button Pushed");
            StartCoroutine(ButtonTimer());
        }
        if (Input.GetKeyDown(KeyCode.E)
            && _isNearBed)
        {
            Debug.Log("Game over");
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

    private void OnTriggerleave(Collider other)
    {
        if (other.tag == "StandupButtonTrigger")
        {
            _isNearButton = false;
        }
    }

    IEnumerator ButtonTimer()
    {
        topDoor.SetActive(false);
        bottomDoor.SetActive(false);
        _stanupButtonScreen.SetColor("_Color", new Color32(0xf2, 0x23, 0x13, 0xff));
        yield return new WaitForSeconds(1);
        _stanupButtonScreen.SetColor("_Color", new Color32(0x78, 0x11, 0x0a, 0xff));
        yield return new WaitForSeconds(1);
        _stanupButtonScreen.SetColor("_Color", Color.black);
        topDoor.SetActive(true);
        bottomDoor.SetActive(true);
    }
}
