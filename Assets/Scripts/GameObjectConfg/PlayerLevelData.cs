using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConsumptionEvent
{
    默认,
    创建等级一的塔防,
    升级至等级二的塔防,
    升级至等级三的塔防,
    砍树,
    种树,
    动物治疗成功,
    下一轮,
    基地自愈动物,
    动物增加速度
}

[Serializable]
public class PlayerLevelData
{
    private static PlayerLevelData _instance;
    public static PlayerLevelData Instance
    {
        get
        {
            if (_instance == null)
                InitialInstance();
            return _instance;
        }
        private set { }
    }

    public int LifeCount = 1000;
    public int CoinCount = 400;
    public int WoodCount = 100;
    public int TurnCount = 5;
    public float EnemySpeed = 2;
    public float KValue;



    public PlayerLevelData(int lifeCount=1000,int coinCount=400,int woodCount=100,int turnCount=5,float enemySpeed=2,float kValue=1)
    {
        LifeCount = lifeCount;
        CoinCount = coinCount;
        WoodCount = woodCount;
        TurnCount = turnCount;
        EnemySpeed = enemySpeed;
        KValue = kValue;
    }

    /// <summary>
    /// 消耗函数
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public bool Consume(ConsumptionEvent e,PlayerLevelData other=null)
    {
        ConsumeConfig config = StaticConfigs.consumeConfig_Static;
        switch (e)
        {
            case ConsumptionEvent.创建等级一的塔防:
                if (IsCanConsume(config.CreateTower))
                    Consume(config.CreateTower);
                else
                    return false;
                Debug.Log(config.CreateTower.ToString());
                break;

            case ConsumptionEvent.升级至等级二的塔防:
                if (IsCanConsume(config.UpgradeTowerToLevelTwo))
                    Consume(config.UpgradeTowerToLevelTwo);
                else
                    return false;
                Debug.Log(config.UpgradeTowerToLevelTwo.ToString());
                break;
            case ConsumptionEvent.升级至等级三的塔防:
                if (IsCanConsume(config.UpgradeTowerToLevelThree))
                    Consume(config.UpgradeTowerToLevelThree);
                else
                    return false;
                Debug.Log(config.UpgradeTowerToLevelThree.ToString());
                break;
            case ConsumptionEvent.砍树:
                if (IsCanConsume(config.CutWoodConsume))
                    Consume(config.CutWoodConsume);
                else
                    return false;
                Debug.Log(config.CutWoodConsume.ToString());
                break;
            case ConsumptionEvent.种树:
                if (IsCanConsume(config.CreateTreeConsume))
                    Consume(config.CreateTreeConsume);
                else
                    return false;
                Debug.Log(config.CreateTreeConsume.ToString());
                break;
            case ConsumptionEvent.动物治疗成功:
                if (IsCanConsume(config.CueAnimalSucc))
                    Consume(config.CueAnimalSucc);
                else
                    return false;
                Debug.Log(config.CueAnimalSucc.ToString());
                break;
            case ConsumptionEvent.下一轮:
                if(IsCanConsume(config.NextTurn))
                    Consume(config.NextTurn);
                else
                    return false;
                Debug.Log(config.NextTurn.ToString());
                break;
            case ConsumptionEvent.基地自愈动物:
                if(other != null)
                {
                    if (IsCanConsume(other))
                        Consume(other);
                }
                else
                    return false;
                Debug.Log(other.ToString());
                break;
            case ConsumptionEvent.动物增加速度:
                if (other != null)
                {
                    if (IsCanConsume(other))
                        Consume(other);
                }
                else
                    return false;
                Debug.Log(other.ToString());
                break;
            case ConsumptionEvent.默认:
                return false;
        }
        Debug.Log("AFTER UPGRADE:"+Instance.ToString());
        LevelUIEvent.UpdatePlayerDataEvent?.Invoke();
        return true;
    }

    public bool IsCanConsume(PlayerLevelData data)
    {
        return Instance.LifeCount >= data.LifeCount &&
               Instance.CoinCount >= data.CoinCount &&
               Instance.WoodCount >= data.WoodCount &&
               Instance.TurnCount >= data.TurnCount &&
               Instance.EnemySpeed >= data.EnemySpeed &&
               Instance.KValue >= data.KValue;
    }

    public void Consume(PlayerLevelData d)
    {
        Instance.LifeCount -= d.LifeCount;
        Instance.CoinCount -= d.CoinCount;
        Instance.WoodCount -= d.WoodCount;
        Instance.TurnCount -= d.TurnCount;
        Instance.EnemySpeed -= d.EnemySpeed;
        Instance.KValue -= d.KValue;
    }

    public static void InitialInstance()
    {
        if (StaticConfigs.resourceInitializeConfig_Static != null)
        {
            LevelResourceInitialConfig config = StaticConfigs.resourceInitializeConfig_Static;
            _instance = new PlayerLevelData(config.ForestLevelResource.LifeCount,
                                                    config.ForestLevelResource.CoinCount,
                                                    config.ForestLevelResource.WoodCount,
                                                    config.ForestLevelResource.TurnCount,
                                                    config.ForestLevelResource.EnemySpeed,
                                                    config.ForestLevelResource.KValue);
        }
        else
        {
            _instance = new PlayerLevelData();
            Debug.LogWarning("LevelResourceInitialConfig Is Not Initial Suc");
        }
    }

    public override string ToString() => $"L:{LifeCount}  C:{CoinCount}  W:{WoodCount}  T:{TurnCount}  ES:{EnemySpeed}  KV:{KValue}";
    #region 运算符重载
    public static PlayerLevelData operator +(PlayerLevelData d1,PlayerLevelData d2)
    {
        PlayerLevelData d = new PlayerLevelData();
        d.LifeCount = d1.LifeCount + d2.LifeCount;
        d.CoinCount = d1.CoinCount + d2.CoinCount;
        d.WoodCount = d1.WoodCount + d2.WoodCount;
        d.TurnCount = d1.TurnCount + d2.TurnCount;
        d.EnemySpeed = d1.EnemySpeed + d2.EnemySpeed;
        d.KValue = d1.KValue + d2.KValue;

        return d;
    }
    #endregion
}
