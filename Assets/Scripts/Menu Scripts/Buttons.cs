using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    public Button playButton, loadButton, controlsButton, settingsButton, quitButton;
    public GameObject mainMenu, controlsMenu, settingsMenu, effectsSlide, musicSlide, sensitiveSlide;

    void Start()
    {
        playButton.onClick.AddListener(playHandle);
        settingsButton.onClick.AddListener(settingsHandle);
        controlsButton.onClick.AddListener(controlsHandle);
        quitButton.onClick.AddListener(quitHandle);
    }

    public void playHandle()
    {
        SceneManager.LoadScene(1);
    }

    public void loadHandle()
    {
        
    }

    public void controlsHandle()
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void settingsHandle()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        effectsSlide.SetActive(true);
        musicSlide.SetActive(true);
        sensitiveSlide.SetActive(true);
    }
    public void quitHandle()
    {
        Application.Quit();
    }
    
    public void returnHandle()
    {
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        effectsSlide.SetActive(false);
        musicSlide.SetActive(false);
        sensitiveSlide.SetActive(false);
        mainMenu.SetActive(true);
    }
}