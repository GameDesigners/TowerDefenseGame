using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *   重构时间：2021.3.3
 *   名称：IllustratedBookResource.cs
 *   描述：图鉴资源文件类
 *   编写：ZhuSenlin
 */

public class IllustratedBookResource : MonoBehaviour
{
    [Header("山脉图鉴Sprites列表")] public List<Sprite> ShanMaiSprites;
    [Header("森林图鉴Sprites列表")] public List<Sprite> ForestSprites;
    [Header("沙漠图鉴Sprites列表")] public List<Sprite> ShaMoSprites;
    [Header("海洋图鉴Sprites列表")] public List<Sprite> SeaSprites;
    [Header("淡水图鉴Sprites列表")] public List<Sprite> DanShuiSprites;
    [Header("湿地图鉴Sprites列表")] public List<Sprite> ShiDiSprites;
    [Header("苔原图鉴Sprites列表")] public List<Sprite> TaiYuanSprites;
    [Header("草原图鉴Sprites列表")] public List<Sprite> CaoYuanSprites;

    [Header("动物详细介绍列表")] public List<Sprite> AllAnimalsDetialSprites;

    /// <summary>
    /// 资源字典
    /// </summary>
    public static Dictionary<Level, List<Sprite>> IllustratedBookResourceDic;

    public static Dictionary<string, Sprite> AnimalDetialResourceDic;

    //初始化资源
    private void Start()
    {
        IllustratedBookResourceDic = new Dictionary<Level, List<Sprite>>();
        IllustratedBookResourceDic.Add(Level.山脉关卡, ShanMaiSprites);
        IllustratedBookResourceDic.Add(Level.森林关卡, ForestSprites);
        IllustratedBookResourceDic.Add(Level.沙漠关卡, ShaMoSprites);
        IllustratedBookResourceDic.Add(Level.海洋关卡, SeaSprites);
        IllustratedBookResourceDic.Add(Level.淡水关卡, DanShuiSprites);
        IllustratedBookResourceDic.Add(Level.湿地关卡, ShiDiSprites);
        IllustratedBookResourceDic.Add(Level.苔原关卡, TaiYuanSprites);
        IllustratedBookResourceDic.Add(Level.草原关卡, CaoYuanSprites);

        AnimalDetialResourceDic = new Dictionary<string, Sprite>();
        foreach (var v in AllAnimalsDetialSprites)
            AnimalDetialResourceDic.Add(v.name, v);
    }
}