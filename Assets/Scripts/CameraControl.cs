using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/*
*   重构时间：2021.3.4
*   名称：CameraControl.cs
*   描述：镜头控制脚本
*   编写：ZhuSenlin
*   修改：2021.3.22  移动从基于Object坐标系修改为世界坐标系
*/

[Serializable]
public class Range
{
    public float min;
    public float max;
}

public enum Direction
{
    X,Y,Z
}

public enum MoveDirection
{
    Left,Right,
    Up,Down,
    ScaleIn,ScaleOut
}
/// <summary>
/// 镜头（视野）控制
/// </summary>
[RequireComponent(typeof(Camera))]
public class CameraControl : MonoBehaviour
{
#if UNITY_EDITOR
    private string CAMERA_DEFAULT_POS_X = "Camera Default Pos X";
    private string CAMERA_DEFAULT_POS_Y = "Camera Default Pos Y";
    private string CAMERA_DEFAULT_POS_Z = "Camera Default Pos Z";
#endif
    /// <summary>
    /// 相机初始位置
    /// </summary>
    [Header("相机初始位置")]
    [ContextMenuItem("设置相机至默认位置位置", "SetCameraPosToDefaultPos")]
    [ContextMenuItem("设置相机当前位置为默认位置", "SetCameraDefaultPos")]
    public Vector3 postion;

    [Header("镜头移动坐标基")]
    public Direction CameraControlLeftDirection;
    public Direction CameraControlUpDirection;
    public Direction CameraControlScaleInDirection;

    [Serializable]
    public class CameraMoveEvent : UnityEvent { }
    [Header("镜头移动响应事件"),SerializeField] public CameraMoveEvent CameraMoveActionEvent=new CameraMoveEvent();

    private void SetCameraPosToDefaultPos()
    {
#if UNITY_EDITOR
        postion = new Vector3(UnityEditor.EditorPrefs.GetFloat(CAMERA_DEFAULT_POS_X), UnityEditor.EditorPrefs.GetFloat(CAMERA_DEFAULT_POS_Y), UnityEditor.EditorPrefs.GetFloat(CAMERA_DEFAULT_POS_Z));
        camPos.position = postion;
#endif
    }

    private void SetCameraDefaultPos()
    {
        postion = camPos.position;
#if UNITY_EDITOR
        UnityEditor.EditorPrefs.SetFloat(CAMERA_DEFAULT_POS_X, camPos.position.x);
        UnityEditor.EditorPrefs.SetFloat(CAMERA_DEFAULT_POS_Y, camPos.position.y);
        UnityEditor.EditorPrefs.SetFloat(CAMERA_DEFAULT_POS_Z, camPos.position.z);
#endif
    }
    /// <summary>
    /// 镜头上下移动的范围
    /// </summary>
    [Header("相机移动的Y轴范围(WorldSpace)"), SerializeField] public Range yRange;

    /// <summary>
    /// 镜头远近移动的范围
    /// </summary>
    [Header("相机移动的Z轴范围(WorldSpace)"), SerializeField] public Range zRange;

    /// <summary>
    /// 镜头左右移动的范围
    /// </summary>
    [Header("相机移动的X轴范围(WorldSpace)"), SerializeField] public Range xRange;

    /// <summary>
    /// 镜头移动的速度
    /// </summary>
    [Header("移动速度")] public float moveSpeed;

    /// <summary>
    /// 镜头缩放的速度
    /// </summary>
    [Header("缩放速度")] public float scaleSpeed;

    private Transform camPos;
    private Camera mainCamera;
    private Dictionary<Direction, Vector3> directionBase;

    private void Awake()
    {
        camPos = GetComponent<Transform>();
        mainCamera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Start()
    {
        directionBase = new Dictionary<Direction, Vector3>();
        directionBase.Add(Direction.X, Vector3.right);
        directionBase.Add(Direction.Y, Vector3.up);
        directionBase.Add(Direction.Z, Vector3.forward);
    }

    private void Update()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (scrollWheel > 0)
            CameraMove(MoveDirection.ScaleIn);
        else if (scrollWheel < 0)
            CameraMove(MoveDirection.ScaleOut);

        #region KeyBoard Input
        if (Input.GetKey(KeyCode.W))
            CameraMove(MoveDirection.Up);
        else if (Input.GetKey(KeyCode.S))
            CameraMove(MoveDirection.Down);
        else if (Input.GetKey(KeyCode.A))
            CameraMove(MoveDirection.Left);
        else if (Input.GetKey(KeyCode.D))
            CameraMove(MoveDirection.Right);
        #endregion
    }

    /// <summary>
    /// 镜头移动函数
    /// </summary>
    /// <param name="moveDir">移动方向</param>
    /// <param name="data">按钮数据</param>
    public void CameraMove(MoveDirection moveDir)
    {
        Direction direction;
        bool isPositiveDirection;
        if (moveDir == MoveDirection.Left) 
        {
            direction = CameraControlLeftDirection;
            isPositiveDirection = true;
        }
        else if (moveDir == MoveDirection.Right)
        {
            direction = CameraControlLeftDirection;
            isPositiveDirection = false;
        }
        else if (moveDir == MoveDirection.Up)
        {
            direction = CameraControlUpDirection;
            isPositiveDirection = true;
        }
        else if (moveDir == MoveDirection.Down)
        {
            direction = CameraControlUpDirection;
            isPositiveDirection = false;
        }

        else if (moveDir == MoveDirection.ScaleOut)
        {
            direction = CameraControlScaleInDirection;
            isPositiveDirection = true;
        }
        else  //Scale In
        {
            direction = CameraControlScaleInDirection;
            isPositiveDirection = false;
        }

        if (!CameraCanMove(direction, isPositiveDirection)) return;

        //相机镜头移动函数
        if (isPositiveDirection)
            camPos.position += directionBase[direction] * moveSpeed * Time.deltaTime;
        else
            camPos.position -= directionBase[direction] * moveSpeed * Time.deltaTime;

        CameraMoveActionEvent?.Invoke();
    }

    /// <summary>
    /// 判断相机是否可以移动
    /// </summary>
    /// <param name="diectionBase">移动轴向</param>
    /// <param name="isPositiveDirection">运动方向是否为轴的正向</param>
    /// <returns></returns>
    private bool CameraCanMove(Direction diectionBase,bool isPositiveDirection)
    {
        bool isCanMove = false;
        Range range;
        float positionValue;
        switch(diectionBase)
        {
            case Direction.X:
                range = xRange;
                positionValue = camPos.position.x;
                break;
            case Direction.Y:
                range = yRange;
                positionValue = camPos.position.y;
                break;
            case Direction.Z:
                range = zRange;
                positionValue = camPos.position.z;
                break;
            default:
                return false;

        }

        if (isPositiveDirection)
        {
            if (positionValue > range.max)
                isCanMove = false;
            else
                isCanMove = true;
        }
        else
        {
            if (positionValue < range.min)
                isCanMove = false;
            else
                isCanMove = true;
        }
        return isCanMove;
    }
    //private void OnGUI()
    //{
    //    GUILayout.Label($"当前摄像机的坐标系为（World）：{transform.position}");
    //}
}