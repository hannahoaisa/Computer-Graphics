using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class sliderScript : MonoBehaviour
{
    public Slider effects, music, sensitivity;
    public Sound soundScript;
    public AudioSource effectsSound;
    // Start is called before the first frame update
    void Start()
    {
        effects.enabled = true;
        effects.SetValueWithoutNotify(PlayerPrefs.GetFloat("effectsVol"));
        soundScript.SetSfxLevel(Mathf.Log10(PlayerPrefs.GetFloat("effectsVol")) * 20);
        music.enabled = true;
        music.SetValueWithoutNotify(PlayerPrefs.GetFloat("musicVol"));
        soundScript.SetMusicLevel(Mathf.Log10(PlayerPrefs.GetFloat("musicVol")) * 20);
        sensitivity.enabled = true;
        sensitivity.SetValueWithoutNotify(PlayerPrefs.GetFloat("sensitivity"));
    }

    // Update is called once per frame
    void Update()
    {
        soundScript.SetSfxLevel(Mathf.Log10 (effects.value) * 20);
        PlayerPrefs.SetFloat("effectsVol", effects.value);
        soundScript.SetMusicLevel(Mathf.Log10(music.value) * 20);
        PlayerPrefs.SetFloat("musicVol", music.value);
        PlayerPrefs.SetFloat("sensitivity", sensitivity.value);
        Debug.Log(PlayerPrefs.GetFloat("sensitivity"));
    }
}
