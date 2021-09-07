using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *   重构时间：2021.3.5
 *   名称：Shoter.cs
 *   描述：防御塔炮台类
 *   编写：ZhuSenlin
 */

public class Shoter : MonoBehaviour
{
    [Header("子弹预制体")]public GameObject bullet;
    [Header("射击目标")] public GameObject target;

    [Header("发射间隔")] public float EmmitInterval=0.2f;

    private float minDamage;
    private float maxDamage;

    public bool isShoting = true;

    private float currentTime;

    private void Start()
    {
        CreateBullet(minDamage,maxDamage);
    }

    private void Update()
    {
        if (isShoting)
        {
            currentTime += Time.deltaTime;
            if (currentTime > EmmitInterval)
                CreateBullet(minDamage,maxDamage);
        }
        else
        {
            if (transform.childCount == 0)
                DestroySelf();
        }
    }

    /// <summary>
    /// 炮台初始化
    /// </summary>
    /// <param name="_bullet"></param>
    /// <param name="_target"></param>
    public void Initialize(GameObject _bullet,GameObject _target,float _minDamage,float _maxDamage)
    {
        bullet = _bullet;
        target = _target;
        minDamage = _minDamage;
        maxDamage = _maxDamage;
        
    }

    /// <summary>
    /// 销毁炮台
    /// </summary>
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    //停止射击
    public void StopShoting()
    {
        isShoting = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        Destroy(gameObject);
    }

    private void CreateBullet(float minDamage,float maxDemage)
    {
        currentTime = 0;

        bullet.GetComponent<BulletTrace>().minDamage = minDamage;
        bullet.GetComponent<BulletTrace>().maxDamage = maxDemage;

        GameObject m = GameObject.Instantiate(bullet, transform);
        m.transform.localPosition = Vector3.zero;
        m.SetActive(true);
        if(target!=null)
            m.GetComponent<BulletTrace>().target = target.transform;
    }
}
