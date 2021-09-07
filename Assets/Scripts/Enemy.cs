using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *   重构时间：2021.3.5
 *   名称：Enemy.cs
 *   描述：敌人类
 *   编写：ZhuSenlin
 */
public enum EnemyType
{
    牛, 兔子, 浣熊
}
public class Enemy : MonoBehaviour
{
    [Header("动物类型")] public EnemyType enemyType;
    [Header("ID")] public int ID;
    [Header("防御塔准星")] public Transform Cross;
    [Header("血量满额")] public float HoleHealth = 100;

    public Action<Enemy> DeathEvent;
    public Dictionary<DefensiveTowerTrigger, GameObject> triggers = new Dictionary<DefensiveTowerTrigger, GameObject>();

    #region UI部分
    [Header("血量滑动条")] public CustomizeUI.Customize_Slider healthSlider;
    [Header("治愈中文本")] public GameObject AddHealthTextTip;
    [Header("治愈粒子特效")] public GameObject CueParticleSystem;
    [Header("模型主体")] public GameObject Model;
    [Header("UI 主体")] public GameObject UIBase;

    [HideInInspector] public float Health { get; private set; }
    #endregion

    private void Start()
    {
        Health = 0;
        healthSlider.Value = Health;
    }

    /// <summary>
    /// 敌人受到伤害
    /// </summary>
    /// <param name="damage"></param>
    public void GetDamage(float damage)
    {
        Health = Health - damage >= HoleHealth ? HoleHealth : Health + damage;
        if (Health >= HoleHealth)
        {
            Health = 0;
            PlayerLevelData.Instance.Consume(ConsumptionEvent.动物治疗成功);
            Death();
            GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>().AddAnimalCueNum();
            if (CueParticleSystem != null)
            {
                CueParticleSystem.SetActive(true);
                Model.SetActive(false);
                UIBase.SetActive(false);
            }
            StartCoroutine(ReSet(2));
        }

        healthSlider.Value = Health / HoleHealth;
    }

    private IEnumerator ReSet(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (CueParticleSystem != null)
        {
            CueParticleSystem.SetActive(false);
            Model.SetActive(true);
            UIBase.SetActive(true);
            AddHealthTextTip.SetActive(false);
        }
        GetComponent<EnemyMoving>().ReSetEnemy();
    }
    public void Death()
    {
        foreach (var t in triggers)
            t.Key.ChangeTarget(this);

        triggers.Clear();
    }
}