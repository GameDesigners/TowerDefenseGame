using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/*
 *   重构时间：2021.3.4
 *   名称：MouseCoverEvent.cs
 *   描述：鼠标悬停事件
 *   编写：ZhuSenlin
 */

/// <summary>
/// 鼠标悬停事件
/// </summary>
public enum MouseEvent
{
    /// <summary>
    /// 屏幕左边缘悬停
    /// </summary>
    LeftCover,
    /// <summary>
    /// 屏幕右边缘悬停
    /// </summary>
    RightCover,
    /// <summary>
    /// 屏幕上边缘悬停
    /// </summary>
    UpCover,
    /// <summary>
    /// 屏幕下边缘悬停
    /// </summary>
    DownCover
}

/// <summary>
/// 鼠标悬停事件
/// </summary>
public class MouseCoverEvent : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    /// <summary>
    /// 鼠标事件类型
    /// </summary>
    [Header("鼠标事件类型")] public MouseEvent mEvent;
    [Header("镜头控制脚本")] public CameraControl camControl;

    //是否处于悬停状态
    private bool isCover=false;

    private void Update()
    {
        if (isCover)
            CoverEvent();
    }

    private void CoverEvent()
    {
        switch(mEvent)
        {
            case MouseEvent.DownCover:
                camControl.CameraMove(MoveDirection.Down);
                break;
            case MouseEvent.UpCover:
                camControl.CameraMove(MoveDirection.Up);
                break;
            case MouseEvent.LeftCover:
                camControl.CameraMove(MoveDirection.Left);
                break;
            case MouseEvent.RightCover:
                camControl.CameraMove(MoveDirection.Right);
                break;

        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isCover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isCover = false;
    }
}