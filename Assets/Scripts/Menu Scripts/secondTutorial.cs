using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class secondTutorial : MonoBehaviour
{
    private bool toggleStart = false;
    private bool toggleDone = false;
    private bool flyStart = false;
    private bool flyDone = false;
    private bool interactStart = false;
    public bool timerDone = false;
    public TextMeshProUGUI toggleText;
    public TextMeshProUGUI flyText;
    public TextMeshProUGUI interactText;
    public RawImage toggleImage;
    public RawImage flyImage;
    public RawImage interactImage;
    private void Start()
    {
        gameObject.SetActive(true);
        toggleText.color = new Color(toggleText.color.r, toggleText.color.g, toggleText.color.b, 0);
        toggleImage.color = new Color(toggleImage.color.r, toggleImage.color.g, toggleImage.color.b, 0);
        flyText.color = new Color(flyText.color.r, flyText.color.g, flyText.color.b, 0);
        flyImage.color = new Color(flyImage.color.r, flyImage.color.g, flyImage.color.b, 0);
        interactText.color = new Color(interactText.color.r, interactText.color.g, interactText.color.b, 0);
        interactImage.color = new Color(interactImage.color.r, interactImage.color.g, interactImage.color.b, 0);
    }
    void Update()
    {
        if (!toggleStart)
        {
            StartCoroutine(StartTimer(2f));
            if (timerDone)
            {
                timerDone = false;
                toggleStart = true;
                StartCoroutine(playToggle());
            }
        }
        if (!flyStart && toggleDone)
        {
            flyStart = true;
            StartCoroutine(playFly());
        }
        if (!interactStart && flyDone && toggleDone)
        {
            interactStart = true;
            StartCoroutine(playInteract());
        }
    }



    public IEnumerator FadeToFullAlpha(float t, TextMeshProUGUI i, RawImage c)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        c.color = new Color(c.color.r, c.color.g, c.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            c.color = new Color(c.color.r, c.color.g, c.color.b, c.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeToZeroAlpha(float t, TextMeshProUGUI i, RawImage c)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        c.color = new Color(c.color.r, c.color.g, c.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            c.color = new Color(c.color.r, c.color.g, c.color.b, c.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator StartTimer(float time)
    {
        yield return new WaitForSeconds(time);
        timerDone = true;
    }

    public IEnumerator playToggle()
    {
        StartCoroutine(FadeToFullAlpha(1f, toggleText, toggleImage));
        yield return new WaitForSeconds(8f);
        StartCoroutine(FadeToZeroAlpha(1f, toggleText, toggleImage));
        yield return new WaitForSeconds(2f);
        toggleDone = true;
    }
    public IEnumerator playFly()
    {
        StartCoroutine(FadeToFullAlpha(1f, flyText, flyImage));
        yield return new WaitForSeconds(8f);
        StartCoroutine(FadeToZeroAlpha(1f, flyText, flyImage));
        yield return new WaitForSeconds(2f);
        flyDone = true;
    }
    public IEnumerator playInteract()
    {
        StartCoroutine(FadeToFullAlpha(1f, interactText, interactImage));
        yield return new WaitForSeconds(8f);
        StartCoroutine(FadeToZeroAlpha(1f, interactText, interactImage));
        yield return new WaitForSeconds(2f);
    }
}
