using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
 *   重构时间：2021.3.2
 *   名称：MainPageUIEvent.cs
 *   描述：主场景UI事件脚本
 *   编写：ZhuSenlin
 */

[RequireComponent(typeof(UIAudioSource))]
public class MainPageUiEvent : MonoBehaviour
{
    [Header("开始游戏按钮")] public Button enterGameBtn;
    [Header("UI动画控制器")] public Animator animator;

    private void Start()
    {
        enterGameBtn.onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene(SceneName.ChooseLevelScene);   
        });
    }

    /// <summary>
    ///播放UI淡出动画
    /// </summary>
    public void PlayUIFadeInAnim()
    {
        animator.Play(AnimationName.UIFadeIn);
        GetComponent<UIAudioSource>().ClickSourcePlay();
    }
}