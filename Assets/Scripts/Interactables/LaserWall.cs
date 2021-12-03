using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWall : MonoBehaviour
{
    public Activator[] activatorsAND;
    public Activator[] activatorsOR;
    public bool areInputsActivated = false;

    private Animator _laserAnimator;

    void Start()
    {
        _laserAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // The following inputs must ALL be on for door to open
        foreach (Activator input in activatorsAND)
        {
            areInputsActivated = true;

            // Sets to false as soon as it finds one inactive input
            if (!input.isButtonActivated)
            {
                areInputsActivated = false;
                break;
            }
        }

        // Only continue to check inputs if it's not already activated
        if (!areInputsActivated)
        {
            // Only ONE of the following inputs must be on for the door to open
            foreach (Activator input in activatorsOR)
            {
                areInputsActivated = false;

                // Sets to true as soon as it finds one active input
                if (input.isButtonActivated)
                {
                    areInputsActivated = true;
                    break;
                }
            }
        }

        // Turns lasers on and off
        if (areInputsActivated)
        {
            StartCoroutine(turnOn());
        }
        else
        {
            StartCoroutine(turnOff());
        }

        IEnumerator turnOn()
        {
            yield return new WaitForSeconds(0.1f);
            _laserAnimator.SetTrigger("Lasers On");
        }

        IEnumerator turnOff()
        {
            yield return new WaitForSeconds(0.1f);
            _laserAnimator.SetTrigger("Lasers On");
        }

    }

}
