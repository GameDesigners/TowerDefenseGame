using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

[RequireComponent(typeof(UIAudioSource))]
public class LevelUIEvent : MonoBehaviour
{
    [Header("暂停按钮")] public Button PauseBtn;
    [Header("继续按钮")] public Button ContinueGameBtn;
    [Header("重开按钮")] public Button ReStartGameBtn;
    [Header("大厅按钮")] public Button ReturnMainPageBtn;

    [Header("结算重开按钮")] public Button JieSuan_ReStartGameBtn;
    [Header("结算返回大厅")] public Button JieSuan_ReturnMainPageBtn;

    [Header("背包按钮")] public Button OpenBagBtn;
    [Header("关闭按钮")] public Button CloseBagBtn;


    [Header("暂停面板")] public GameObject PausePanel;
    [Header("背包面板")] public GameObject BagPanel;

    #region 玩家数据面板
    [Header("玩家关卡数据面板")]
    public Text LifeCountTxt;
    public Text CoinCountTxt;
    public Text WoodCountTxt;
    public Text TurnCountTxt;
    public Text SpeedTxt;
    public static Action UpdatePlayerDataEvent;
    #endregion
    private void Start()
    {
        UpdatePlayerDataUI();
        UpdatePlayerDataEvent += UpdatePlayerDataUI;

        PauseBtn.onClick.AddListener(delegate () 
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0;
            GetComponent<UIAudioSource>().ClickSourcePlay();
        });

        ContinueGameBtn.onClick.AddListener(delegate ()
        {
            PausePanel.SetActive(false);
            Time.timeScale = 1;
            GetComponent<UIAudioSource>().ClickSourcePlay();
        });

        ReStartGameBtn.onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene(SceneName.Level01);
            InitialData();
        });

        ReturnMainPageBtn.onClick.AddListener(delegate ()
        {
            AsyncLoad.LoadScene(SceneName.ChooseLevelScene);
            FogWithDepthTexture.currentCount = 0;
            InitialData();
        });

        JieSuan_ReStartGameBtn.onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            FogWithDepthTexture.currentCount = 0;
            InitialData();
        });

        JieSuan_ReturnMainPageBtn.onClick.AddListener(delegate ()
        {
            AsyncLoad.LoadScene(SceneName.ChooseLevelScene);
            InitialData();
        });

        OpenBagBtn.onClick.AddListener(delegate ()
        {
            BagPanel.SetActive(true);
            GetComponent<UIAudioSource>().ClickSourcePlay();
        });

        CloseBagBtn.onClick.AddListener(delegate ()
        {
            BagPanel.SetActive(false);
            GetComponent<UIAudioSource>().ClickSourcePlay();
        });
    }

    private void UpdatePlayerDataUI()
    {
        //Debug.Log("更新数据");
        PlayerLevelData data = PlayerLevelData.Instance;
        if(data!=null)
        {
            LifeCountTxt.text = $"{data.LifeCount}";
            CoinCountTxt.text = $"×{data.CoinCount}";
            WoodCountTxt.text = $"×{data.WoodCount}";
            TurnCountTxt.text = $"{data.TurnCount}";
            SpeedTxt.text = $"{data.EnemySpeed} m/s";
        }
    }

    private void InitialData()
    {
        PlayerLevelData.InitialInstance();
        UpdatePlayerDataEvent -= UpdatePlayerDataUI;
        Time.timeScale = 1;
        GetComponent<UIAudioSource>().ClickSourcePlay();
        FogWithDepthTexture.currentCount = 0;
    }
}