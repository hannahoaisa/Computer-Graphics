using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject[] pauseObjects;
    public GameObject[] showOnDeath;
    public GameObject[] showOnWin;
    public bool isPaused = false;
    public bool isDead;
    public bool gameWon;
    public FirstPersonMovement fpMove;
    public PlayerTriggerHandler ptHandler;
    public Button resumeButton, restartButton, quitButton;

    private void Start()
    {
        resumeButton.onClick.AddListener(resumeHandle);
        restartButton.onClick.AddListener(restartHandle);
        quitButton.onClick.AddListener(quitHandle);
        Time.timeScale = 1;
        hidePaused();
    }
    void Update()
    {
        isDead = fpMove.isDead;
        gameWon = ptHandler.gameWon;
        if (!isPaused && Input.GetKeyDown("escape") || isDead || gameWon)
        {
            Pause();
        }
    }
    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (!isDead
            && !gameWon)
        {
            showPaused();
        }
        else if (gameWon)
        {
            showWon();
        }
        else if(isDead)
        {
            showDead();
        }
    }
    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        hidePaused();
        hideDead();
        hideWin();
    }

    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with ShowOnPause tag
    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    public void showDead()
    {
        foreach (GameObject g in showOnDeath)
        {
            g.SetActive(true);
        }
    }

    public void showWon()
    {
        foreach (GameObject g in showOnWin)
        {
            g.SetActive(true);
        }
    }

    //hides objects with ShowOnPause tag
    public void hideDead()
    {
        foreach (GameObject g in showOnDeath)
        {
            g.SetActive(false);
        }
    }
    public void hideWin()
    {
        foreach (GameObject g in showOnWin)
        {
            g.SetActive(false);
        }
    }

    public void resumeHandle()
    {
        Resume();
    }
    public void restartHandle()
    {
        isPaused = false;
        isDead = false;
        gameWon = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void quitHandle()
    {
        Application.Quit();
        Debug.Log("Game quit.");
    }
}
