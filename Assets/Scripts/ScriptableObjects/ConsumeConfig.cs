using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ConsumeConfig")]
public class ConsumeConfig : ScriptableObject
{
    [Header("创建防御塔消耗"), SerializeField] public PlayerLevelData CreateTower = new PlayerLevelData(0, 100, 10, 0, 0, 0);
    [Header("升级至防御塔二的消耗"), SerializeField] public PlayerLevelData UpgradeTowerToLevelTwo = new PlayerLevelData(0, 180, 20, 0, 0, 0);
    [Header("升级至防御塔三的消耗"), SerializeField] public PlayerLevelData UpgradeTowerToLevelThree = new PlayerLevelData(0, 260, 30, 0, 0, 0);

    [Header("砍树消耗"), SerializeField] public PlayerLevelData CutWoodConsume;
    [Header("种树获得金币(获得为负)"), SerializeField] public PlayerLevelData CreateTreeConsume;
    [Header("治疗成功获得（获得为负）"), SerializeField] public PlayerLevelData CueAnimalSucc;
    [Header("进行至下一轮的参数变化"), SerializeField] public PlayerLevelData NextTurn;
}

