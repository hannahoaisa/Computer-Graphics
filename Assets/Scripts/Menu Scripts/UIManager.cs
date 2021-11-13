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
    public GameObject[] showOnSettings;
    public bool isPaused = false;
    public bool isDead;
    public bool isGameWon;
    public ErinMove erinMove;
    public EagleMove eagleMove;
    public GravityUI gravityUI;
    public Objective objective;
    public Button resumeButton, nextButton, restartButton, quitButton, settings, returnButton;

    private void Start()
    {
        resumeButton.onClick.AddListener(resumeHandle);
        restartButton.onClick.AddListener(restartHandle);
        quitButton.onClick.AddListener(quitHandle);
        nextButton.onClick.AddListener(nextHandle);
        settings.onClick.AddListener(settingsHandle);
        returnButton.onClick.AddListener(returnHandle);
        Time.timeScale = 1;
        hidePaused();
    }
    void Update()
    {
        if (erinMove.isDead || eagleMove.isDead)
        {
            isDead = true;
        }
        isGameWon = objective.isGameWon;
        if (!isPaused && Input.GetKeyDown("escape") || isDead || isGameWon)
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
            && !isGameWon && !gravityUI.gravityChange)
        {
            showPaused();
        }
        else if (isGameWon)
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

    public void showSettings()
    {
        foreach (GameObject g in showOnSettings)
        {
            g.SetActive(true);
        }
    }

    public void hideSettings()
    {
        foreach (GameObject g in showOnSettings)
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
        isGameWon = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void quitHandle()
    {
        Application.Quit();
        Debug.Log("Game quit.");
    }

    public void nextHandle()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void settingsHandle()
    {
        showSettings();
        hidePaused();
    }

    public void returnHandle()
    {
        hideSettings();
        showPaused();
    }

}
