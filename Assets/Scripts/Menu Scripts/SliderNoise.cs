using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SliderNoise : MonoBehaviour, IEndDragHandler
{
    public AudioSource effectsDeselect;
    public void OnEndDrag(PointerEventData eventData)
    {
        effectsDeselect.Play();
    }
}
