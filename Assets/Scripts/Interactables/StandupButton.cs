using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandupButton : MonoBehaviour
{
    public bool canErinInteract = true;
    public bool canEagleInteract = true;
    public float timerLength = 2.0f;
    public AudioSource timerStart;
    public AudioSource timerDone;
    public bool isButtonActivated;                                // This value will be read by other scripts

    private Material _consoleScreen;
    private bool _newInteraction;

    // Start is called before the first frame update
    void Start()
    {
        _consoleScreen = gameObject.GetComponent<Renderer>().materials[1];
        _consoleScreen.SetColor("_Color", Color.black);
        isButtonActivated = false;
    }

    private void Update()
    {
        CheckInteractions();

        if (_newInteraction)
        {
            _newInteraction = false;
            StopAllCoroutines();
            StartCoroutine(ButtonTimer());
        }
    }

    private void CheckInteractions()
    {
        // If Erin interacted with it
        if (gameObject.tag == "ErinHit"
            && canErinInteract)
        {
            gameObject.tag = "Untagged";
            _newInteraction = true;
        }
        // If Eagle interacted with it
        else if (gameObject.tag == "EagleHit"
            && canEagleInteract)
        {
            gameObject.tag = "Untagged";
            _newInteraction = true;
        }
        // If someone interacted with it but isn't allowed to
        else if (gameObject.tag != "Untagged")
        {
            gameObject.tag = "Untagged";
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
    }
}
