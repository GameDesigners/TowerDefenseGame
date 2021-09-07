using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *   重构时间：2021.3.19
 *   名称：CustomTimer.cs
 *   描述：自定义计时器
 *   编写：ZhuSenlin
 */

//计时事件枚举类
public enum UseTimerTag
{
    WaitingCompetitve,    //等待抢答
    AnsweringTheQuestion, //回答问题的时间
    None,
}

public class TimerManager
{
    public static CustomTimer StartCaculate(int _length, UseTimerTag _tag = UseTimerTag.None)
    {
        GameObject go = new GameObject(_tag.ToString() + "_Timers");
        go.AddComponent<CustomTimer>();
        go.GetComponent<CustomTimer>().StartCaculate(_length, _tag);
        return go.GetComponent<CustomTimer>();
    }
}

public class CustomTimer : MonoBehaviour
{
    private float caculateTimeLength; //计时长短
    private int currentTime;          //当前倒计时时间
    [SerializeField, Header("是否完成计时")] private static bool isFinish;            //是否完成计时
    private bool isCaculating;        //是否正在计时

    private float temp;
    private bool isPlaySound = false;

    public event Action<UseTimerTag> FinishCaculate;  //计时结束的委托事件
    public UseTimerTag tags;                          //事件计时器使用的标签

    private void Update()
    {
        if (isCaculating && !isFinish)  //正在计时且未完成
        {
            if (temp > 0)
            {
                temp -= Time.deltaTime;
                currentTime = (int)temp < 0 ? 0 : (int)temp;
            }
            else
            {
                currentTime = 0;
                FinishTimer();
            }
        }
    }

    /// <summary>
    /// 开始计时
    /// </summary>
    /// <param name="_length"></param>
    /// <param name="_tag"></param>
    public void StartCaculate(int _length, UseTimerTag _tag = UseTimerTag.None)
    {
        Debug.Log("开始计时" + _length + "秒，" + "类型为：" + _tag.ToString());
        caculateTimeLength = temp = _length;
        currentTime = 0;
        isCaculating = true;
        isFinish = false;
        tags = _tag;
        isPlaySound = false;
    }

    /// <summary>
    /// 提前结束计时器
    /// </summary>
    public void PreFinishTimer()
    {
        Debug.Log("提前关闭计时器：" + tags);
        tags = UseTimerTag.None;  //改为无用计时器
        FinishTimer();
    }

    /// <summary>
    /// 获取当前时间
    /// </summary>
    public int CurrentTime
    {
        get
        {
            if (isCaculating && !isFinish)
                return currentTime;
            else
                return 0;
        }
    }

    /// <summary>
    /// 获取计时长度
    /// </summary>
    public int CaculateTimeLength
    {
        get
        {
            return (int)caculateTimeLength;
        }
    }

    /// <summary>
    /// 完成计时
    /// </summary>
    public void FinishTimer()
    {
        Debug.Log("关闭计时器：" + tags);
        isFinish = true;
        isCaculating = false;
        FinishCaculate?.Invoke(tags);
    }

    //判断是否正在计时
    public bool IsCaculating()
    {
        return isCaculating;
    }
}
