using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 *   重构时间：2021.3.2
 *   名称：Customize_LongPressButton.cs
 *   描述：自定义长按式按钮组件
 *   编写：ZhuSenlin
 */

namespace CustomizeUI
{
    [RequireComponent(typeof(Button))]
    public class Customize_LongPressButton : MonoBehaviour, IPointerDownHandler,IPointerUpHandler,IPointerExitHandler
    {
        [Serializable]
        public class LongPressButtonEvent : UnityEvent { }
        [Header("长按按钮的响应事件")] public LongPressButtonEvent LongPressEvent;


        private Button button;
        private bool isPressing = false;
        [Range(0, 1)] private float pressProgress;
        private float pressLoadSpeed = 5;

        private bool isAlreadyAction = false;



        /// <summary>
        /// 当前是否为按下状态
        /// </summary>
        public bool IsPresssing
        {
            get { return isPressing; }
            private set { }
        }

        /// <summary>
        /// 响应事件的进度
        /// </summary>
        public float PressProgress
        {
            get { return pressProgress; }
            private set { }
        }

        /// <summary>
        /// 长按响应速度
        /// </summary>
        public float LongPressLoadSpeed
        {
            get { return pressLoadSpeed; }
            set { pressLoadSpeed = value; }
        }


        private void Start()
        {
            button = GetComponent<Button>();
        }

        private void Update()
        {
            if(IsPresssing)
            {
                pressProgress = Mathf.Clamp(pressProgress + pressLoadSpeed * Time.deltaTime, 0, 1);
                if(pressProgress==1&&!isAlreadyAction)
                {
                    //完成
                    LongPressEvent?.Invoke();
                    pressProgress = 0;
                    isAlreadyAction = false;
                    isPressing = false;
                }

            }
            else
            {
                pressProgress = Mathf.Clamp(pressProgress - pressLoadSpeed * Time.deltaTime, 0, 1);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isPressing = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if(isAlreadyAction)
            {
                pressProgress = 0;
                isAlreadyAction = false;
            }
            isPressing = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (isAlreadyAction)
            {
                pressProgress = 0;
                isAlreadyAction = false;
            }
            isPressing = false;
        }
    }
}
