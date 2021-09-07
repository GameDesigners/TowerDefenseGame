using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 *   重构时间：2021.3.2
 *   名称：Customize_Slider.cs
 *   描述：自定义Slider组件
 *   编写：ZhuSenlin
 */

namespace CustomizeUI
{
    /// <summary>
    /// 自定义滑动条
    /// </summary>
    public class Customize_Slider : MonoBehaviour
    {
        [Header("滑动条图片")] public Image slider;
        [Header("滑动条提示")] public Text sliderTip;
        
        /// <summary>
        /// 滑动条修改的值
        /// </summary>
        public float Value
        {
            get { return slider.fillAmount; }
            set { SetValue(value); }
        }

        /// <summary>
        /// 设置Slider的值,并修改提示
        /// </summary>
        /// <param name="percent">百分比</param>
        public void SetValue(float percent)
        {
            if (percent < 0 || percent > 1)
                return;

            slider.fillAmount = percent;

            if (sliderTip != null)
                sliderTip.text = ((int)(percent * 100)).ToString() + "%";
        }

        /// <summary>
        /// 同步UGUISlider组件的值（用于辅助）
        /// </summary>
        public void AsyncProgressFromSlider()
        {
            if (GetComponent<Slider>() != null)
                slider.fillAmount = GetComponent<Slider>().value;
        }
    }
}
