using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *   重构时间：2021.3.2
 *   名称：GlobalValue.cs
 *   描述：全局变量脚本
 *   编写：ZhuSenlin
 */

public class GlobalValue
{

}

/// <summary>
/// 场景名称类
/// </summary>
public class SceneName
{
    /// <summary>
    /// 初始化场景
    /// </summary>
    public static string InitialGame = "Initialize";

    /// <summary>
    /// 首页场景
    /// </summary>
    public static string MainPage = "01MainPage";

    /// <summary>
    /// 选择关卡场景
    /// </summary>
    public static string ChooseLevelScene = "02ChooseLevelScene";

    /// <summary>
    /// 第一个关卡
    /// </summary>
    public static string Level01 = "Scene-Forest";

    /// <summary>
    /// 游戏场景
    /// </summary>
    public static string GameScene = "03Game";

    /// <summary>
    /// 异步加载场景
    /// </summary>
    public static string AyscScene = "AyscScene";

}

public class AnimationName
{
    public static string StateHide = "StateHide";
    public static string StateShow = "StateShow";

    public static string UIFadeIn = "FadeIn";

    public static string AttackRageFadeIn = "AttackRageFadeIn";
    public static string AttackRageFadeOut = "AttackRageFadeOut";
}
