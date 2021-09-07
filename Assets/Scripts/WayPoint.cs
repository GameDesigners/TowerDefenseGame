using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *   重构时间：2021.3.4
 *   名称：WayPoint.cs
 *   描述：路径点
 *   编写：ZhuSenlin
 */

public enum WAYID
{
    线路一,    //森林地图的线路1
    线路二,    //森林地图的线路2
    线路三     //森林地图的线路3
}

public class WayPoint : MonoBehaviour
{
    /// <summary>
    /// 第一关的路径点
    /// </summary>
    public static Dictionary<WAYID, List<GameObject>> GlobalWayPoints = new Dictionary<WAYID, List<GameObject>>();

    [Header("本条线路的ID")] public WAYID wayID;

    private void Awake()
    {
        List<GameObject> wayPoints = new List<GameObject>();

        //将路径点依次存入数组
        for(int index=0;index<transform.childCount;index++)
            wayPoints.Add(transform.GetChild(index).gameObject);

        if (GlobalWayPoints.ContainsKey(wayID))
            GlobalWayPoints.Remove(wayID);    
        GlobalWayPoints.Add(wayID, wayPoints);
    }
}
