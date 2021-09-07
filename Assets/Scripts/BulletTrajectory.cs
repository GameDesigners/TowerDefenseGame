using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *   重构时间：2021.3.5
 *   名称：BulletTrajectory.cs
 *   描述：子弹轨迹计算
 *   编写：ZhuSenlin
 */

public class BulletTrajectory : MonoBehaviour
{
    private LineRenderer line;
    [Header("防御塔中心点")] public Transform StartPos;

    [Header("轨迹材质球")] public Material lineMaterial;

    /// <summary>
    /// 射线字典
    /// </summary>
    [Header("射线列表")] public Dictionary<GameObject, LineRenderer> bulletTrajectories; 

    /// <summary>
    /// 射线宽度
    /// </summary>
    public float lineWidth;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        bulletTrajectories = new Dictionary<GameObject, LineRenderer>();



    }

    /// <summary>
    /// 设置射线属性
    /// </summary>
    /// <param name="line"></param>
    private void InitialLineParam(LineRenderer line)
    {
        line.positionCount = 2;//设置两点

        line.material = lineMaterial;
        //设置直线颜色,宽度
        line.startColor = Color.red;
        line.endColor = Color.red;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;

        line.SetPosition(0, StartPos.position);
    }

    /// <summary>
    /// 添加攻击射线
    /// </summary>
    public void CreateAttackTrajectoryLine(GameObject gameObject)
    {
        GameObject lineRender= new GameObject(GetLineObjectName(gameObject.name));
        lineRender.AddComponent(typeof(LineRenderer));
        InitialLineParam(lineRender.GetComponent<LineRenderer>());
        bulletTrajectories.Add(gameObject, lineRender.GetComponent<LineRenderer>());
    }

    /// <summary>
    /// 删除攻击射线
    /// </summary>
    /// <param name="gameObject"></param>
    public void DeleteAttackTrajectoryLine(GameObject gameObject)
    {
        LineRenderer line;
        if (bulletTrajectories.TryGetValue(gameObject, out line))
        {
            Destroy(line.gameObject);
            bulletTrajectories.Remove(gameObject);
        }

    }

    private string GetLineObjectName(string name) => name + "_line";

    private void Update()
    {
        foreach (var v in bulletTrajectories)
        {
            if (v.Value != null && v.Key.GetComponent<Enemy>() != null)
            {
                v.Value.SetPosition(1, v.Key.GetComponent<Enemy>().Cross.position);
                v.Key.GetComponent<Enemy>().GetDamage(5f*Time.deltaTime);
            }
        }
    }
}
