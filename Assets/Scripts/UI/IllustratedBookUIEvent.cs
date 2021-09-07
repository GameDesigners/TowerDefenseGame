using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
/*
 *   重构时间：2021.3.3
 *   名称：IllustratedBookUIEvent.cs
 *   描述：图鉴UI事件
 *   编写：ZhuSenlin
 */

public class IllustratedBookUIEvent : MonoBehaviour
{
    [Header("总体的游戏对象")] public GameObject holePanel;
    [Header("选择图鉴地区的面板")] public GameObject SelectPanel;
    [Header("选择查看动物的面板")] public GameObject TuJianPanel;
    [Header("动物详细介绍的面板")] public GameObject AnimalDetialIntroPanel;


    #region 图鉴页面一级UI控件
    [Header("图鉴页面一级UI控件")] 
    public Button CloseHolePageBtnInSelectPanel;
    public Button PreRegionBtn;
    public Button NextRegionBtn;
    public Button EnterRegionBtn;
    public Text RegionTip;
    public static Level currentRegion=0;
    #endregion

    #region 动物列表页面
    [Header("动物列表页面UI控件")]
    public Image AnimalIcon;
    public Button CloseHolePageBtnInAnimalPanelBtn;
    public Button PrePageBtnInAnimalPanelBtn;
    public Button PreAnimalBtn;
    public Button NextAnimalBtn;
    public Button CheckAnimalDetialBtn;
    public static int animalIndex = 0;
    #endregion

    #region 动物详情页
    [Header("动物详情页面UI控件")]
    public Image AnimalDetialImage;
    public Button ReturnBtn;
    #endregion

    private void Start()
    {
        RegionTip.text = currentRegion.ToString();
        CloseHolePageBtnInSelectPanel.onClick.AddListener(CloseIllustratedBookPage);
        PreRegionBtn.onClick.AddListener(PreRegion);
        NextRegionBtn.onClick.AddListener(NextRegion);
        EnterRegionBtn.onClick.AddListener(EnterRegion);

        CloseHolePageBtnInAnimalPanelBtn.onClick.AddListener(CloseIllustratedBookPage);
        PrePageBtnInAnimalPanelBtn.onClick.AddListener(PrePageBtnInAnimalPanel);
        PreAnimalBtn.onClick.AddListener(PreAnimal);
        NextAnimalBtn.onClick.AddListener(NextAnimal);
        CheckAnimalDetialBtn.onClick.AddListener(CheckAnimalDetial);

        ReturnBtn.onClick.AddListener(ReturnAnimalPage);
    }



    #region UI控件事件集合
    //关闭图鉴页面
    private void CloseIllustratedBookPage()
    {
        //初始化页面的显隐状态
        SetPanelState(true, false, false);
        holePanel.SetActive(false);
    }

    /// <summary>
    /// 设置各个页面的显隐状态
    /// </summary>
    /// <param name="selectPanel"></param>
    /// <param name="tujianPanel"></param>
    /// <param name="animalDetialPanel"></param>
    private void SetPanelState(bool selectPanel,bool tujianPanel,bool animalDetialPanel)
    {
        SelectPanel.SetActive(selectPanel);
        TuJianPanel.SetActive(tujianPanel);
        AnimalDetialIntroPanel.SetActive(animalDetialPanel);
    }

    //上一个地区事件
    private void NextRegion()
    {
        if ((int)currentRegion >= Enum.GetValues(typeof(Level)).Length - 1)
            currentRegion = 0;
        else
            ++currentRegion;

        RegionTip.text = currentRegion.ToString();
    }

    //下一个地区事件
    private void PreRegion()
    {
        if ((int)currentRegion <= 0)
            currentRegion = (Level)(Enum.GetValues(typeof(Level)).Length - 1);
        else
            --currentRegion;

        RegionTip.text = currentRegion.ToString();
    }

    //确认地区事件
    private void EnterRegion()
    {
        SetPanelState(false, true, false);
        animalIndex = 0;
        AnimalIcon.sprite = IllustratedBookResource.IllustratedBookResourceDic[currentRegion][animalIndex];
    }

    private void PrePageBtnInAnimalPanel()
    {
        SetPanelState(true, false, false);
    }

    private void PreAnimal()
    {
        if (animalIndex <= 0)
            animalIndex = IllustratedBookResource.IllustratedBookResourceDic[currentRegion].Count - 1;
        else
            --animalIndex;
        AnimalIcon.sprite = IllustratedBookResource.IllustratedBookResourceDic[currentRegion][animalIndex];
    }

    private void NextAnimal()
    {
        if (animalIndex >= IllustratedBookResource.IllustratedBookResourceDic[currentRegion].Count - 1)
            animalIndex = 0;
        else
            ++animalIndex;

        AnimalIcon.sprite = IllustratedBookResource.IllustratedBookResourceDic[currentRegion][animalIndex];
    }

    private void CheckAnimalDetial()
    {
        SetPanelState(false, false, true);
        string animalName = IllustratedBookResource.IllustratedBookResourceDic[currentRegion][animalIndex].name;
        if (IllustratedBookResource.AnimalDetialResourceDic.ContainsKey(animalName))
            AnimalDetialImage.sprite = IllustratedBookResource.AnimalDetialResourceDic[animalName];
    }

    private void ReturnAnimalPage()
    {
        SetPanelState(false, true, false);
    }
    #endregion
}