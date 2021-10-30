using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject[] pauseObjects;
    public GameObject[] showOnDeath;
    public bool isPaused = false;
    public bool isDead;
    public FirstPersonMovement fpMove;
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
        if (!isPaused && Input.GetKeyDown("escape") || isDead)
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
        if (!isDead)
        {
            showPaused();
        }
        else
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

    //hides objects with ShowOnPause tag
    public void hideDead()
    {
        foreach (GameObject g in showOnDeath)
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void quitHandle()
    {
        Application.Quit();
    }
}
