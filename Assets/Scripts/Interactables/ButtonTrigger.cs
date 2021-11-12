using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public float timerLength = 2.0f;
    public GameObject buttonConsole;
    public AudioSource timerStart;
    public AudioSource timerDone;
    public bool isButtonActivated;                                // This value will be read by other scripts

    private Material _consoleScreen;
    private GameObject[] _nearbyCharacterObjects = new GameObject[2];    // Slot 0 = Erin; Slot 1 = Eagle
    private GameObject _nearbyCharacter;
    private IEnumerator _buttonCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        _consoleScreen = buttonConsole.GetComponent<Renderer>().materials[1];
        _consoleScreen.SetColor("_Color", Color.black);
        isButtonActivated = false;
        _buttonCoroutine = ButtonTimer();
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

    private void Update()
    {
        // See if Erin is near the button
        if (_nearbyCharacterObjects[0] != null
            && _nearbyCharacterObjects[0].tag == "ActiveCharacter")
        {
            _nearbyCharacter = _nearbyCharacterObjects[0];
        }
        // See if Eagle is near the button
        else if (_nearbyCharacterObjects[1] != null
            && _nearbyCharacterObjects[1].tag == "ActiveCharacter")
        {
            _nearbyCharacter = _nearbyCharacterObjects[1];
        }
        // Either no one is near the button or, for some bizarre reason, neither are active
        else
        {
            _nearbyCharacter = null;
        }

        if (_nearbyCharacter != null
            && Input.GetKeyDown(KeyCode.E))
        {
            StopAllCoroutines();
            StartCoroutine(ButtonTimer());
        }
    }

    IEnumerator ButtonTimer()
    {
        // Button turns on
        isButtonActivated = true;
        timerStart.Play();
        _consoleScreen.SetColor("_Color", new Color32(0xf2, 0x23, 0x13, 0xff));
        yield return new WaitForSeconds(timerLength / 2f);

        // Intermediate color, signalling timer is running out
        _consoleScreen.SetColor("_Color", new Color32(0x78, 0x11, 0x0a, 0xff));
        yield return new WaitForSeconds(timerLength / 2f);

        // Button turns off
        isButtonActivated = false;
        _consoleScreen.SetColor("_Color", Color.black);
        timerDone.Play();
        Debug.Log("Completed");
    }
}
