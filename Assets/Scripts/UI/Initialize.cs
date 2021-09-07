using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 *   重构时间：2021.3.2
 *   名称：MainPageUIEvent.cs
 *   描述：主场景UI事件脚本
 *   编写：ZhuSenlin
 */

public class Initialize : MonoBehaviour
{
    private void Start()
    {
        AsyncLoad.LoadScene(SceneName.MainPage);
    }
}
