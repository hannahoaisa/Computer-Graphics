using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject[] pauseObjects;
    public GameObject[] hideOnPause;
    public GameObject[] showOnDeath;
    public GameObject[] showOnWin;
    public GameObject[] showOnSettings;
    public GameObject[] showOnSave;
    public saveLoad SaveLoad;
    public AudioSource theMusic;
    public bool isPaused = false;
    public bool isDead = false;
    public bool isGameWon;
    public ErinMove erinMove;
    public EagleMove eagleMove;
    public GravityUI gravityUI;
    public Objective objective;
    public Button resumeButton, restartFromPause, restartFromDeath, restartFromWin,
        quitFromPause, quitFromDeath, quitFromWin, settings, returnFromSettings, returnFromSave,
        saveButton, save1, save2, save3;

    private void Start()
    {
        resumeButton.onClick.AddListener(resumeHandle);
        restartFromPause.onClick.AddListener(restartHandle);
        restartFromDeath.onClick.AddListener(restartHandle);
        restartFromWin.onClick.AddListener(restartHandle);
        quitFromPause.onClick.AddListener(quitHandle);
        quitFromDeath.onClick.AddListener(quitHandle);
        quitFromWin.onClick.AddListener(quitHandle);
        //nextButton.onClick.AddListener(nextHandle);
        settings.onClick.AddListener(settingsHandle);
        returnFromSettings.onClick.AddListener(returnHandle);
        returnFromSave.onClick.AddListener(returnHandle);
        saveButton.onClick.AddListener(saveHandle);
        save1.onClick.AddListener(slot1Handle);
        save2.onClick.AddListener(slot2Handle);
        save3.onClick.AddListener(slot3Handle);
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
        else if (isPaused && !isDead && !isGameWon && Input.GetKeyDown("escape"))
        {
            Resume();
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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
        foreach (GameObject g in hideOnPause)
        {
            //g.SetActive(false);
        }
    }

    //hides objects with ShowOnPause tag
    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in hideOnPause)
        {
            g.SetActive(true);
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

    public void showSave()
    {
        foreach (GameObject g in showOnSave)
        {
            g.SetActive(true);
        }
    }

    public void hideSave()
    {
        foreach (GameObject g in showOnSave)
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
        Debug.Log("Restart");
        isPaused = false;
        isDead = false;
        isGameWon = false;
        Physics.gravity = Vector3.down * erinMove.gravity.strength;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void quitHandle()
    {
        Destroy(GameObject.Find("Music"));
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void nextHandle()
    {
        Physics.gravity = Vector3.down * erinMove.gravity.strength;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void settingsHandle()
    {
        showSettings();
        hidePaused();
    }

    public void saveHandle()
    {
        showSave();
        hidePaused();
    }

    public void returnHandle()
    {
        hideSettings();
        hideSave();
        showPaused();
    }
    public void slot1Handle()
    {
        SaveLoad.saveGame("1");
    }
    public void slot2Handle()
    {
        SaveLoad.saveGame("2");
    }
    public void slot3Handle()
    {
        SaveLoad.saveGame("3");
    }

}
