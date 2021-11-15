using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GravityUI : MonoBehaviour
{
    public UIManager uiManage;
    public MouseOverGrav mouseGravUp;
    public MouseOverGrav mouseGravLeft;
    public MouseOverGrav mouseGravRight;
    public MouseOverGrav mouseGravForward;
    public GameObject[] showOnGravity;
    public bool gravityChange = false;
    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            gravityChange = true;
            uiManage.Pause();
            ShowGravityObj();
        }
        if (Input.GetMouseButtonUp(1))
        {
            gravityChange = false;
            uiManage.Resume();
            mouseGravUp.image.color = mouseGravUp.initColor;
            mouseGravLeft.image.color = mouseGravLeft.initColor;
            mouseGravRight.image.color = mouseGravRight.initColor;
            mouseGravUp.upChange = false;
            mouseGravLeft.leftChange = false;
            mouseGravRight.rightChange = false;
            mouseGravForward.forwardChange = false;
            HideGravityObj();
        }
    }
    public void ShowGravityObj()
    {
        foreach (GameObject g in showOnGravity)
        {
            g.SetActive(true);
        }
    }
    public void HideGravityObj()
    {
        foreach (GameObject g in showOnGravity)
        {
            g.SetActive(false);
        }
    }
}
