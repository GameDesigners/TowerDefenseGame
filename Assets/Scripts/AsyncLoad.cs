using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomizeUI;
using UnityEngine.SceneManagement;

/*
 *   重构时间：2021.3.2
 *   名称：AsyncLoad.cs
 *   描述：异步加载代码
 *   编写：ZhuSenlin
 */
public class AsyncLoad : MonoBehaviour
{
    /// <summary>
    /// 场景加载进度条
    /// </summary>
    [Header("场景加载进度条")] public Customize_Slider slider;
    
    /// <summary>
    /// 场景加载速度
    /// </summary>
    [Header("场景加载速度")] public float loadingSpeed = 1;


    //异步加载操作
    private AsyncOperation _async;

    //异步操作目标场景
    private static string targetScene;

    //当前到达进度
    private float _currentProgress;

    private void Start()
    {
        _currentProgress = 0;
        _async = null;
        slider.Value = 0;

        //开启异步操作
        StartCoroutine(LoadScene());
    }

    private void Update()
    {
        _currentProgress = _async.progress;

        if(_async.progress>=0.9f)
        {
            _currentProgress = 1.0f;
        }

        if(_currentProgress!=slider.Value)
        {
            slider.Value = Mathf.Lerp(slider.Value, _currentProgress, Time.deltaTime * loadingSpeed);
            if (Mathf.Abs(slider.Value - _currentProgress) < 0.01f)
                slider.Value = _currentProgress;
        }

        if ((int)(slider.Value * 100) == 100)
            _async.allowSceneActivation = true;
    }

    //协程函数
    private IEnumerator LoadScene()
    {
        //异步加载
        _async = SceneManager.LoadSceneAsync(targetScene);

        //在未加载完成之前禁止激活场景
        _async.allowSceneActivation = false;

        yield return _async;
    }

    /// <summary>
    /// 通过异步加载场景
    /// </summary>
    /// <param name="target">目标场景</param>
    public static void LoadScene(string target)
    {
        SceneManager.LoadScene(SceneName.AyscScene);
        targetScene = target;
    }
}