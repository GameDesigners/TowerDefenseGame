using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 关卡枚举类
/// </summary>
public enum Level
{
    森林关卡,
    山脉关卡,
    淡水关卡,
    海洋关卡,
    湿地关卡,
    苔原关卡,
    草原关卡,
    沙漠关卡
}

public class LevelManager
{
    public static List<Level> UnlockLevels = new List<Level>();
}

public class ChooseLevelUIEvent : MonoBehaviour
{
    [Header("按钮集合对像")] public GameObject Btns;
    [Header("设置按钮")] public Button SettingBtn;
    [Header("图鉴按钮")] public Button TuJianBtn;

    [Header("海洋关卡选择按钮")] public Button SeaLevelBtn;
    [Header("淡水关卡选择按钮")] public Button DanShuiLevelBtn;
    [Header("森林关卡选择按钮")] public Button ForestLevelBtn;
    [Header("状态栏显示隐藏动画")] public Animator stateAnimator;

    [Header("关卡介绍Sprites集合")] public List<Sprite> levelIntroSprites;
    [Header("关卡介绍面板")] public GameObject LevelIntoPanel;
    [Header("关卡介绍Image")] public Image LevelIntroImg;
    [Header("退出关卡介绍按钮")] public Button QuitLevelIntroBtn;
    [Header("进入关卡按钮")] public Button EnterLevelBtn;

    #region 设置面板
    [Header("设置面板")] public GameObject SettingPanel;
    [Header("关闭设置面板的按钮")] public Button CloseSettingPanelBtn;
    [Header("控制音乐的滑动条")] public Slider ControlMusicSlider;
    [Header("控制声音的滑动条")] public Slider ControlSoundSlider;
    #endregion

    #region 图鉴面板
    [Header("图鉴面板")] public GameObject TuJianPanel;
    #endregion
    private void Start()
    {
        LevelManager.UnlockLevels.Add(Level.森林关卡);

        #region 设置面板的UI控件事件集合
        SettingBtn.onClick.AddListener(delegate ()
        {
            SettingPanel.SetActive(true);
        });

        CloseSettingPanelBtn.onClick.AddListener(delegate ()
        {
            SettingPanel.SetActive(false);
        });


        #endregion

        SeaLevelBtn.onClick.AddListener(delegate ()
        {
            Debug.Log("进入海洋关卡");
            ChooseLevel(Level.海洋关卡);
        });

        DanShuiLevelBtn.onClick.AddListener(delegate ()
        {
            Debug.Log("进入淡水关卡");
            ChooseLevel(Level.淡水关卡);
        });

        ForestLevelBtn.onClick.AddListener(delegate ()
        {
            Debug.Log("进入森林关卡");
            ChooseLevel(Level.森林关卡);
        });

        QuitLevelIntroBtn.onClick.AddListener(delegate ()
        {
            LevelIntoPanel.SetActive(false);
        });

        TuJianBtn.onClick.AddListener(delegate ()
        {
            TuJianPanel.SetActive(true);
        });

        EnterLevelBtn.onClick.AddListener(delegate() 
        {
            AsyncLoad.LoadScene(SceneName.Level01);
        });
    }

    private void ChooseLevel(Level level)
    {
        if (!LevelManager.UnlockLevels.Contains(level))
            return;

        int index = (int)level;
        LevelIntroImg.sprite = levelIntroSprites[index];
        LevelIntoPanel.SetActive(true);
    }
}