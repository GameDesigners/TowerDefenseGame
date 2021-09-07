using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticConfigs : MonoBehaviour
{
    [Header("关卡资源初始化配置表")] public LevelResourceInitialConfig resourceInitializeConfig;
    [Header("消耗配置表")] public ConsumeConfig consumeConfig;
    [Header("关卡波数数据")] public GameTurnData gameTurnData;

    #region 对外的静态变量接口
    public static ConsumeConfig consumeConfig_Static;
    public static LevelResourceInitialConfig resourceInitializeConfig_Static;
    public static GameTurnData gameTurnData_Static;
    #endregion
    private void Awake()
    {
        consumeConfig_Static = consumeConfig;
        resourceInitializeConfig_Static = resourceInitializeConfig;
        gameTurnData_Static = gameTurnData;
    }
}