using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioMixer masterMix;
    public void SetSfxLevel(float sfxLvl)
    {

        masterMix.SetFloat("volSfx", sfxLvl);

    }

    public void SetMusicLevel(float musicLvl)
    {

        masterMix.SetFloat("volMusic", musicLvl);

    }
}
