using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *   重构时间：2021.3.5
 *   名称：GamingUIEvent.cs
 *   描述：游戏场景的Ui事件
 *   编写：ZhuSenlin
 */

public class GamingUIEvent : MonoBehaviour
{
    public Animator stateAnimator;
    private void Start()
    {
        stateAnimator.Play(AnimationName.StateShow);
    }

    private void Update()
    {

    }
}
