using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class tutorialScript : MonoBehaviour
{
    private bool moveStart = false;
    private bool moveDone = false;
    private bool sprintStart = false;
    private bool sprintDone = false;
    private bool interactStart = false;
    private bool interactDone = false;
    private bool gravityStart = false;
    private bool gravityDone = false;
    private bool objectiveStart = false;
    public bool timerDone = false;
    public TextMeshProUGUI moveText;
    public TextMeshProUGUI sprintText;
    public TextMeshProUGUI interactText;
    public TextMeshProUGUI gravityText;
    public TextMeshProUGUI objectiveText;
    public RawImage moveImage;
    public RawImage sprintImage;
    public RawImage interactImage;
    public RawImage gravityImage;
    public RawImage objectiveImage;
    private void Start()
    {
        gameObject.SetActive(true);
        moveText.color = new Color(moveText.color.r, moveText.color.g, moveText.color.b, 0);
        moveImage.color = new Color(moveImage.color.r, moveImage.color.g, moveImage.color.b, 0);
        sprintText.color = new Color(sprintText.color.r, sprintText.color.g, sprintText.color.b, 0);
        sprintImage.color = new Color(sprintImage.color.r, sprintImage.color.g, sprintImage.color.b, 0);
        interactText.color = new Color(interactText.color.r, interactText.color.g, interactText.color.b, 0);
        interactImage.color = new Color(interactImage.color.r, interactImage.color.g, interactImage.color.b, 0);
        gravityText.color = new Color(gravityText.color.r, gravityText.color.g, gravityText.color.b, 0);
        gravityImage.color = new Color(gravityImage.color.r, gravityImage.color.g, gravityImage.color.b, 0);
        objectiveText.color = new Color(objectiveText.color.r, objectiveText.color.g, objectiveText.color.b, 0);
        objectiveImage.color = new Color(objectiveImage.color.r, objectiveImage.color.g, objectiveImage.color.b, 0);
    }
    void Update()
    {
        if(!moveStart)
        {
            StartCoroutine(StartTimer(2f));
            if (timerDone)
            {
                timerDone = false;
                moveStart = true;
                StartCoroutine(playMove());
            }
        }
        if (!sprintStart && moveDone)
        {
            sprintStart = true;
            StartCoroutine(playSprint());
        }
        if (!interactStart && sprintDone && moveDone)
        {
            interactStart = true;
            StartCoroutine(playInteract());
        }
        if (!gravityStart && interactDone && sprintDone && moveDone)
        {
            gravityStart = true;
            StartCoroutine(playGravity());
        }
        if (!objectiveStart && gravityDone && interactDone && sprintDone && moveDone)
        {
            objectiveStart = true;
            StartCoroutine(playObjective());
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

    public IEnumerator playMove()
    {
        StartCoroutine(FadeToFullAlpha(1f, moveText, moveImage));
        yield return new WaitForSeconds(8f);
        StartCoroutine(FadeToZeroAlpha(1f, moveText, moveImage));
        yield return new WaitForSeconds(2f);
        moveDone = true;
    }
    public IEnumerator playSprint()
    {
        StartCoroutine(FadeToFullAlpha(1f, sprintText, sprintImage));
        yield return new WaitForSeconds(8f);
        StartCoroutine(FadeToZeroAlpha(1f, sprintText, sprintImage));
        yield return new WaitForSeconds(2f);
        sprintDone = true;
    }
    public IEnumerator playInteract()
    {
        StartCoroutine(FadeToFullAlpha(1f, interactText, interactImage));
        yield return new WaitForSeconds(8f);
        StartCoroutine(FadeToZeroAlpha(1f, interactText, interactImage));
        yield return new WaitForSeconds(2f);
        interactDone = true;
    }

    public IEnumerator playGravity()
    {
        StartCoroutine(FadeToFullAlpha(1f, gravityText, gravityImage));
        yield return new WaitForSeconds(8f);
        StartCoroutine(FadeToZeroAlpha(1f, gravityText, gravityImage));
        yield return new WaitForSeconds(2f);
        gravityDone = true;
    }

    public IEnumerator playObjective()
    {
        StartCoroutine(FadeToFullAlpha(1f, objectiveText, objectiveImage));
        yield return new WaitForSeconds(8f);
        StartCoroutine(FadeToZeroAlpha(1f, objectiveText, objectiveImage));
        yield return new WaitForSeconds(2f);
    }
}
