using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseOverGrav : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
    public Color initColor;
    public bool upChange = false;
    public bool leftChange = false;
    public bool rightChange = false;
    public bool forwardChange = false;
    public void Start()
    {
        image = gameObject.GetComponent<Image>();
        initColor = image.color;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        switch (gameObject.name){
            case "Up":
                upChange = true;
                break;
            case "Left":
                leftChange = true;
                break;
            case "Right":
                rightChange = true;
                break;
            case "Forward":
                forwardChange = true;
                break;
            default:
                break;
        }
        image.color = Color.green;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        upChange = false;
        leftChange = false;
        rightChange = false;
        forwardChange = false;
        image.color = initColor;
    }
}
