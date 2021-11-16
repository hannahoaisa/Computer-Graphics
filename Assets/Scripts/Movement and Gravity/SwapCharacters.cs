using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCharacters : MonoBehaviour
{
    public GameObject Erin;
    public GameObject Eagle;
    public GameObject ErinMesh;
    public GameObject EagleMesh;
    public GameObject ErinCamera;
    public GameObject EagleCamera;

    private void Start()
    {
        // Layer 6 is reserved for Erin and the Eagle and we want them to not collide with each other
        Physics.IgnoreLayerCollision(6, 6);

        Erin.tag = "ActiveCharacter";
        Eagle.tag = "InactiveCharacter";
        ErinMesh.SetActive(false);
        EagleMesh.SetActive(true);
        ErinCamera.GetComponent<Camera>().enabled = true;
        ErinCamera.GetComponent<AudioListener>().enabled = true;
        EagleCamera.GetComponent<Camera>().enabled = false;
        EagleCamera.GetComponent<AudioListener>().enabled = false;
        Erin.GetComponent<Rigidbody>().isKinematic = false;
        Eagle.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchCharacters();
    }

    void SwitchCharacters()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            // Switch from erin to eagle
            if (Erin.tag == "ActiveCharacter")
            {
                Erin.tag = "InactiveCharacter";
                Eagle.tag = "ActiveCharacter";
                ErinMesh.SetActive(true);
                EagleMesh.SetActive(false);
                ErinCamera.GetComponent<Camera>().enabled = false;
                ErinCamera.GetComponent<AudioListener>().enabled = false;
                EagleCamera.GetComponent<Camera>().enabled = true;
                EagleCamera.GetComponent<AudioListener>().enabled = true;
                Erin.GetComponent<Rigidbody>().isKinematic = true;
                Eagle.GetComponent<Rigidbody>().isKinematic = false;
            }

            // Switch from eagle to erin
            else if (Eagle.tag == "ActiveCharacter")
            {
                Erin.tag = "ActiveCharacter";
                Eagle.tag = "InactiveCharacter";
                ErinMesh.SetActive(false);
                EagleMesh.SetActive(true);
                ErinCamera.GetComponent<Camera>().enabled = true;
                ErinCamera.GetComponent<AudioListener>().enabled = true;
                EagleCamera.GetComponent<Camera>().enabled = false;
                EagleCamera.GetComponent<AudioListener>().enabled = false;
                Erin.GetComponent<Rigidbody>().isKinematic = false;
                Eagle.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }
}
