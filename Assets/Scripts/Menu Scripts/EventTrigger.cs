using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventTrigger : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource buttonHovSound;

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonHovSound.Play();
    }
}
