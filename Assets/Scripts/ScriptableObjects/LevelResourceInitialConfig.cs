using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/LevelResourceInitialConfig")]
public class LevelResourceInitialConfig : ScriptableObject
{
    [Header("森林关卡的资源初始化"), SerializeField] public PlayerLevelData ForestLevelResource = new PlayerLevelData(1000, 400, 100, 5, 2, 1);
}