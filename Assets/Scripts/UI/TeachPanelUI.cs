using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 *   编写时间：2021.3.5
 *   名称：TeachPanelUI.cs
 *   描述：教学UI的点击事件
 *   编写：ZhuSenlin
 */

[RequireComponent(typeof(Button),typeof(Image),typeof(UIAudioSource))]
public class TeachPanelUI : MonoBehaviour
{
    public List<Sprite> TeachSprites;
    private int index = 0;

    [Header("下一轮计时器")] public NextTurnTimer nextTurnTimer;
    private void Awake()
    {
        index = 0;
    }
    private void Start()
    {
        GetComponent<Image>().sprite = TeachSprites[0];
        GetComponent<Button>().onClick.AddListener(delegate ()
        {
            index = index + 1 > TeachSprites.Count - 1 ? -1 : index + 1;
            if (index == -1)
            {
                index = 0;
                GetComponent<Image>().sprite = TeachSprites[index];
                gameObject.transform.parent.gameObject.SetActive(false);
                nextTurnTimer.NextTurnTimerStart();
            }
            else
            {
                GetComponent<Image>().sprite = TeachSprites[index];
                GetComponent<UIAudioSource>().ClickSourcePlay();
            }
        });
    }
}
