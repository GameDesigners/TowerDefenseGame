using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *   重构时间：2021.3.5
 *   名称：DefensiveTower.cs
 *   描述：防御塔触发类
 *   编写：ZhuSenlin
 */
public enum DefendsiveTowerType
{
    SingleShoter=0,        //单发炮台
    MultiAttackShoter,     //多发炮台
    LaserShoter        
}
[RequireComponent(typeof(SphereCollider))]
public class DefensiveTowerTrigger : MonoBehaviour
{
    [Header("子弹预制体")] public GameObject bulletPrefab;
    [Header("炮台预制体列表")] public List<GameObject> shoterPrefabs;
    [Header("炮台类型")] public DefendsiveTowerType DTType;
    [Header("炮弹发射点")] public Transform EmittStartPoint;

    [Header("炮台最小伤害")] public float minDamage=8f;
    [Header("炮台最大伤害")] public float maxDamage=15f;

    [SerializeField] private bool isDirection = false;
    [SerializeField] private Transform DirectionTarget;
    public Dictionary<GameObject, GameObject> shoters;
    [Header("炮台数量")] public int shoterNum;

    private Animator attackRageEffectAnimator;
    private Queue<Transform> AttackTargets = new Queue<Transform>();

    [SerializeField] private int EnemyCount=0;

    [SerializeField] private bool isRangeShow = false;
    [Header("炮台旋转部分"),SerializeField] private Transform TowerModelRoot;

    private void Awake()
    {
        shoters = new Dictionary<GameObject, GameObject>();
        attackRageEffectAnimator = transform.GetChild(0).GetComponent<Animator>();
        attackRageEffectAnimator.gameObject.transform.localScale = new Vector3(0, 0, 0);
    }

    private void Start()
    {
        if (DTType==DefendsiveTowerType.SingleShoter)
        {
            isDirection = true;
        }
    }

    private void Update()
    {
        if(isDirection)
        {
            if (DirectionTarget != null)
                //转动方向使枪口对准动物
                TowerModelRoot.transform.LookAt(DirectionTarget);
        }
        shoterNum = shoters.Count;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            CreateTowerShoter(other.gameObject);
            EnemyCount++;
        }
        Debug.Log($"{other.gameObject.name} 进入范围");
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy") 
        { 
            //if (isDirection)
            //    DirectionTarget = null;

            //Debug.Log("触发出去事件");
            //Shoter shoter;
            //if(shoters.TryGetValue(other.gameObject,out shoter))
            //{
            //    shoter.StopShoting();
            //    attackRageEffectAnimator.Play(AnimationName.AttackRageFadeOut);
            //}

            //if (other.GetComponent<Enemy>() != null)
            //    other.GetComponent<Enemy>().AddHealthTextTip.SetActive(false);
            //shoters.Remove(other.gameObject);
            ChangeTarget(other.GetComponent<Enemy>());

            if (other.GetComponent<Enemy>() != null)
            {
                other.GetComponent<Enemy>().AddHealthTextTip.SetActive(false);
                other.GetComponent<Enemy>().triggers.Remove(this);
            }
        }
    }

    private string GetShoterName(string name) => "Match_" + name.ToString() + "_Shoter";

    public void ChangeTarget(Enemy obj)
    {
        //EnemyCount = EnemyCount - 1 < 0 ? 0 : EnemyCount - 1;
        EnemyCount--;
        GameObject shoter;
        if (shoters.TryGetValue(obj.gameObject, out shoter))
        {
            if(DTType==DefendsiveTowerType.SingleShoter || DTType==DefendsiveTowerType.MultiAttackShoter)
            {
                if (shoter.GetComponent<Shoter>() != null)
                    shoter.GetComponent<Shoter>().StopShoting();
            }
            else if(DTType==DefendsiveTowerType.LaserShoter)
            {
                Destroy(shoter);
            }
            shoters.Remove(obj.gameObject);
        }

        if (DTType==DefendsiveTowerType.SingleShoter)
        {
            Debug.Log("改变目标");
            

            if (AttackTargets.Count > 0)
            {
                Transform enemy = AttackTargets.Dequeue();
                CreateTowerShoter(enemy.gameObject);
            }
            else
            {
                DirectionTarget = null;
                if (isRangeShow)
                {
                    attackRageEffectAnimator.Play(AnimationName.AttackRageFadeOut);
                    isRangeShow = false;
                }
            }
        }
        else if(DTType==DefendsiveTowerType.MultiAttackShoter)
        {
            if(EnemyCount==0)
                if (isRangeShow)
                {
                    attackRageEffectAnimator.Play(AnimationName.AttackRageFadeOut);
                    isRangeShow = false;
                }
        }
    }

    //创建炮台实例
    private void CreateTowerShoter(GameObject obj)
    {
        if (DTType == DefendsiveTowerType.SingleShoter)
        {
            if (shoters.Count > 0)
            {
                AttackTargets.Enqueue(obj.transform);
                return;
            }
        }
        GameObject shoter=null;
        if (DTType == DefendsiveTowerType.SingleShoter || DTType == DefendsiveTowerType.MultiAttackShoter)
        {

            //生成炮台
            int index = (int)DTType;
            shoter = Instantiate(shoterPrefabs[index], EmittStartPoint);
            shoter.name = GetShoterName(obj.name);

            if (shoter.GetComponent<Shoter>() != null)
                shoter.GetComponent<Shoter>().Initialize(bulletPrefab, obj.GetComponent<Enemy>().Cross.gameObject, minDamage, maxDamage);
            shoter.transform.localPosition = Vector3.zero;

            if (isDirection)
                DirectionTarget = obj.GetComponent<Enemy>().Cross;
        }

        else if(DTType==DefendsiveTowerType.LaserShoter)
        {

            //生成炮台
            int index = (int)DTType;
            shoter = Instantiate(shoterPrefabs[index], transform.parent);
            shoter.name = GetShoterName(obj.name);
            shoter.transform.position = EmittStartPoint.position;

            EGA_DemoLasers laser = shoter.GetComponent<EGA_DemoLasers>();
            if (laser != null)
            {
                laser.FirePoint = EmittStartPoint.gameObject;
                laser.TargetPoint = obj.GetComponent<Enemy>().Cross.gameObject;
            }
        }

        if (shoters.ContainsKey(obj.gameObject))
            shoters.Remove(obj.gameObject);
        if (obj.gameObject != null)
        {
            //添加到炮台字典中
            shoters.Add(obj.gameObject, shoter);
            if (!isRangeShow)
            {
                attackRageEffectAnimator.Play(AnimationName.AttackRageFadeIn);
                isRangeShow = true;
            }
        }
        else
            Destroy(shoter);

        if (obj.GetComponent<Enemy>() != null)
        {
            obj.GetComponent<Enemy>().AddHealthTextTip.SetActive(true);
            if (!obj.GetComponent<Enemy>().triggers.ContainsKey(this))
                obj.GetComponent<Enemy>().triggers.Add(this, obj);
        }
    }
}
