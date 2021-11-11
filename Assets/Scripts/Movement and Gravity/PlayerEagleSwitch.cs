using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEagleSwitch : MonoBehaviour
{
    public GameObject PlayerMesh;
    public GameObject PlayerReference;
    public GameObject EagleMesh;
    //public GameObject EagleReference;
    public GameObject PlayerCamera;
    public GameObject EagleCamera;
    public GameObject Player;
    public GameObject Eagle;

    private void Start()
    {
        // Layer 6 is reserved for Erin and the Eagle and we want them to not collide with each other
        Physics.IgnoreLayerCollision(6, 6);

        PlayerMesh.SetActive(false);
        PlayerReference.SetActive(false);
        EagleMesh.SetActive(true);
        EagleCamera.SetActive(false);
        PlayerCamera.SetActive(true);
        Player.tag = "ActiveCharacter";
        Eagle.tag = "InactiveCharacter";
    }

    // Update is called once per frame
    void Update()
    {
        SwitchToPlayer();
    }

    void SwitchToPlayer()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            // Switch from player to eagle
            if (Player.tag == "ActiveCharacter")
            {
                PlayerMesh.SetActive(true);
                PlayerReference.SetActive(true);
                EagleMesh.SetActive(false);
                PlayerCamera.SetActive(false);
                EagleCamera.SetActive(true);
                Player.tag = "InactiveCharacter";
                Eagle.tag = "ActiveCharacter";
            }

            // Switch from eagle to player
            else if (Eagle.tag == "ActiveCharacter")
            {
                PlayerMesh.SetActive(false);
                PlayerReference.SetActive(false);
                EagleMesh.SetActive(true);
                PlayerCamera.SetActive(true);
                EagleCamera.SetActive(false);
                Player.tag = "ActiveCharacter";
                Eagle.tag = "InactiveCharacter";
            }
        }
    }
}
