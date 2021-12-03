using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class saveLoad : MonoBehaviour
{
    public void saveGame(string slot)
    {
        PlayerPrefs.SetInt(slot, SceneManager.GetActiveScene().buildIndex);
    }

    public void loadGame(string slot)
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt(slot));
    }
}
