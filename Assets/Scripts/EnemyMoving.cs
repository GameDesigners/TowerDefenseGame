using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
*   重构时间：2021.3.4
*   名称：EnemyMoving.cs
*   描述：敌人移动
*   编写：ZhuSenlin
*/


public class EnemyMoving : MonoBehaviour
{
    [Header("敌人走的线路ID")] public WAYID wayID;
    [Header("敌人的移动速度")] public float movingSpeed=2;
    [Header("敌人健康值的UI")] public GameObject healthUI;

    private int currentPointIndex = 1;
    private Vector3 direction = Vector3.zero;
    private List<GameObject> points;

    private void Start()
    {
        points = WayPoint.GlobalWayPoints[wayID];
        transform.position = points[0].transform.position;
        direction = GetDirection(transform.position, points[1].transform.position);
        transform.forward = direction.normalized;
    }

    private void FixedUpdate()
    {
        //当游戏对象的位置距离目标点还有0.1m的时候
        if (Vector3.Distance(transform.position, points[currentPointIndex].transform.position) < 0.1f)
        {
            //设置目标点为游戏对象自身的位置
            transform.position = points[currentPointIndex].transform.position;
            //判断有没有下一个目标点
            if (currentPointIndex + 1 < points.Count)
            {
                currentPointIndex++;
            }
        }
        if (transform.position != points[points.Count - 1].transform.position)
        {   //玩家移动的方向
            direction = GetDirection(transform.position, points[currentPointIndex].transform.position);
            transform.forward = direction.normalized;
            transform.Translate(Vector3.forward * movingSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = points[points.Count - 1].transform.position;
            //transform.forward = direction.normalized;
        }

        Vector2 ScreenSpaceArchore = Camera.main.WorldToScreenPoint(GetComponent<Enemy>().Cross.transform.position);
        healthUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(ScreenSpaceArchore.x, ScreenSpaceArchore.y+30);  //20为偏移量
    }

    Vector3 GetDirection(Vector3 v1, Vector3 v2)
    {
        return v2 - v1;
    }

    public void ReSetEnemy()
    {
        points = WayPoint.GlobalWayPoints[wayID];
        transform.position = points[0].transform.position;
        direction = GetDirection(transform.position, points[1].transform.position);
        transform.forward = direction.normalized;
        currentPointIndex = 1;
        direction = Vector3.zero;

        gameObject.SetActive(false);
        if(FindObjectOfType<EnemySpawner>()!=null)
        {
            FindObjectOfType<EnemySpawner>().EnemyInstances[GetComponent<Enemy>().enemyType].Enqueue(gameObject);
        }
    }

}
