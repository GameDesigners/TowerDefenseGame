using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Turn
{
    public int AnimalNum;
    public float AnimalSpeed;
}

[CreateAssetMenu(menuName = "ScriptableObject/GameTurnData")]
public class GameTurnData : ScriptableObject
{
    public List<Turn> TurnData;
}