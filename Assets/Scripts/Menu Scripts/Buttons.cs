using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    public Button playButton, loadButton, controlsButton, settingsButton, quitButton, slot1, slot2, slot3;
    public GameObject mainMenu, controlsMenu, settingsMenu, effectsSlide, musicSlide, sensitiveSlide, settingsBack, controlsBack,
        loadMenu;
    public AudioSource buttonSound;
    public saveLoad SaveLoad;

    void Start()
    {
        playButton.onClick.AddListener(playHandle);
        loadButton.onClick.AddListener(loadHandle);
        settingsButton.onClick.AddListener(settingsHandle);
        controlsButton.onClick.AddListener(controlsHandle);
        quitButton.onClick.AddListener(quitHandle);
        slot1.onClick.AddListener(slot1Handle);
        slot2.onClick.AddListener(slot2Handle);
        slot3.onClick.AddListener(slot3Handle);
    }

    public void playHandle()
    {
        Physics.gravity = Vector3.down * 9.8f;
        SceneManager.LoadScene(1);
    }

    public void loadHandle()
    {
        mainMenu.SetActive(false);
        loadMenu.SetActive(true);
    }

    public void controlsHandle()
    {
        mainMenu.SetActive(false);
        controlsBack.SetActive(true);
        controlsMenu.SetActive(true);
    }

    public void settingsHandle()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        effectsSlide.SetActive(true);
        musicSlide.SetActive(true);
        sensitiveSlide.SetActive(true);
        settingsBack.SetActive(true);

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
        loadMenu.SetActive(false);
        sensitiveSlide.SetActive(false);
        mainMenu.SetActive(true);
        settingsBack.SetActive(false);
        controlsBack.SetActive(false);
    }
    public void slot1Handle()
    {
        int scene = PlayerPrefs.GetInt("1", -1);
        if(scene != -1)
        {
            SaveLoad.loadGame("1");
            mainMenu.SetActive(false);
        }
        loadMenu.SetActive(false);
    }
    public void slot2Handle()
    {
        int scene = PlayerPrefs.GetInt("2", -1);
        if (scene != -1)
        {
            SaveLoad.loadGame("2");
            mainMenu.SetActive(false);
        }
        loadMenu.SetActive(false);
    }
    public void slot3Handle()
    {
        int scene = PlayerPrefs.GetInt("3", -1);
        if (scene != -1)
        {
            SaveLoad.loadGame("3");
            mainMenu.SetActive(false);
        }
        loadMenu.SetActive(false);
    }
}