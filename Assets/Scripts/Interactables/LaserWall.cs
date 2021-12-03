using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWall : MonoBehaviour
{
    public Activator[] activatorsAND;
    public Activator[] activatorsOR;
    public bool areInputsActive = false;
    public bool lasersStartOn = true;

    private bool _areLasersOn = true;
    private Animator _laserAnimator;

    void Start()
    {
        _laserAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        areInputsActive = false;

        // The following inputs must ALL be on for door to open
        foreach (Activator input in activatorsAND)
        {
            areInputsActive = true;

            // Sets to false as soon as it finds one inactive input
            if (!input.isButtonActivated)
            {
                areInputsActive = false;
                break;
            }
        }

        // Only continue to check inputs if it's not already activated
        if (!areInputsActive)
        {
            // Only ONE of the following inputs must be on for the door to open
            foreach (Activator input in activatorsOR)
            {
                areInputsActive = false;

                // Sets to true as soon as it finds one active input
                if (input.isButtonActivated)
                {
                    areInputsActive = true;
                    break;
                }
            }
        }

        // Inverts the state if the lasers start on
        if (!lasersStartOn)
        {
            areInputsActive = (areInputsActive) ? false : true;
        }

        // Turns lasers on and off
        if (areInputsActive
            && _areLasersOn)
        {
            gameObject.tag = "Untagged";
            _areLasersOn = false;
            StartCoroutine(turnOff());
        }
        else if (!areInputsActive
             && !_areLasersOn)
        {
            gameObject.tag = "Deadly";
            _areLasersOn = true;
            StartCoroutine(turnOn());
        }

    }
    IEnumerator turnOn()
    {
        yield return new WaitForSeconds(0.1f);
        _laserAnimator.SetTrigger("Lasers On");
    }

    IEnumerator turnOff()
    {
        yield return new WaitForSeconds(0.1f);
        _laserAnimator.SetTrigger("Lasers Off");
    }
}
