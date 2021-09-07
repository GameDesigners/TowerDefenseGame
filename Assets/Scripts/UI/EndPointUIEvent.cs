using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomizeUI.Customize_Slider))]
public class EndPointUIEvent : MonoBehaviour
{
    [Header("End Point Transform")] public Transform EndPointTransform;
    private CustomizeUI.Customize_Slider LifeSlider;
    private RectTransform UIRect;

    private void Start()
    {
        LifeSlider = GetComponent<CustomizeUI.Customize_Slider>();
        UIRect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector3 screenSpacePos = Camera.main.WorldToScreenPoint(EndPointTransform.position);
        UIRect.anchoredPosition = new Vector2(screenSpacePos.x, screenSpacePos.y);
    }
}
