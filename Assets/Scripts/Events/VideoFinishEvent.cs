using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
/*
*   重构时间：2021.3.12
*   名称：VideoFinishEvent.cs
*   描述：CG视频播放完的事件
*   编写：ZhuSenlin
*/
[RequireComponent(typeof(VideoPlayer))]
public class VideoFinishEvent : MonoBehaviour
{
    [SerializeField] public UnityEvent FinishVideoPlayEvent;

    private VideoPlayer vp;
    private void Start()
    {
        vp = GetComponent<VideoPlayer>();
        vp.loopPointReached += Vp_loopPointReached;
    }

    /// <summary>
    /// 完成视频播放后响应的事件
    /// </summary>
    /// <param name="source"></param>
    private void Vp_loopPointReached(VideoPlayer source)
    {
        FinishVideoPlayEvent?.Invoke();
    }
}