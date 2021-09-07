using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *   重构时间：2021.3.15
 *   名称：FogWithDepthTexture.cs
 *   描述：基于屏幕后处理的全局雾效
 *   编写：ZhuSenlin
 */

public class FogWithDepthTexture : PostEffectsBase
{
    public int count = 18;
    public static int currentCount = 0;
    /// <summary>
    /// 雾效着色器
    /// </summary>
    [Header("雾效着色器")] public Shader fogShader;

    /// <summary>
    /// 着色器材质
    /// </summary>
    private Material fogMaterial = null;
    public Material material
    {
        get
        {
            fogMaterial = CheckShaderAndCreateMaterial(fogShader, fogMaterial);
            return fogMaterial;
        }
    }

    /// <summary>
    /// 主相机
    /// </summary>
    private Camera myCamera;
    public Camera MyCamera
    {
        get
        {
            if (myCamera == null)
                myCamera = GetComponent<Camera>();
            return myCamera;
        }
    }

    /// <summary>
    /// 主相机位置组件
    /// </summary>
    private Transform myCameraTransform;
    public Transform CameraTransform
    {
        get
        {
            if (myCameraTransform == null)
                myCameraTransform = MyCamera.transform;
            return myCameraTransform;
        }
    }

    [ContextMenuItem("Reset", "ResetFogDensity")]
    [Range(0.0f, 3.0f)] public float fogDensity = 1.0f;

    private static void ResetFogDensity()
    {
        currentCount = 0;
    }
    public Color fogColor = Color.white;


    public float fogStart = 0.0f;
    public float fogEnd = 2.0f;

    private void Update()
    {
        fogDensity = Mathf.Pow(currentCount / (float)count,2);
    }


    private void OnEnable()
    {
        //需要获取摄像机的深度纹理
        MyCamera.depthTextureMode |= DepthTextureMode.Depth;
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if(material!=null)
        {
            Matrix4x4 frustumCorners = Matrix4x4.identity;
            float fov = MyCamera.fieldOfView;
            float near = MyCamera.nearClipPlane;
            float far = MyCamera.farClipPlane;
            float aspect = MyCamera.aspect;

            float halfHeight = near * Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad);
            Vector3 toRight = CameraTransform.right * halfHeight * aspect;
            Vector3 toTop = CameraTransform.up * halfHeight;

            Vector3 topLeft = CameraTransform.forward * near + toTop - toRight;
            float scale = topLeft.magnitude / near;
            topLeft.Normalize();
            topLeft *= scale;

            Vector3 topRight = CameraTransform.forward * near + toRight + toTop;
            topRight.Normalize();
            topRight *= scale;

            Vector3 bottomLeft = CameraTransform.forward * near - toTop - toRight;
            bottomLeft.Normalize();
            bottomLeft *= scale;

            Vector3 bottomRight = CameraTransform.forward * near + toRight - toTop;
            bottomRight.Normalize();
            bottomRight *= scale;

            frustumCorners.SetRow(0, bottomLeft);
            frustumCorners.SetRow(1, bottomRight);
            frustumCorners.SetRow(2, topRight);
            frustumCorners.SetRow(3, topLeft);

            material.SetMatrix("_FrustumCornersRay", frustumCorners);
            material.SetFloat("_FogDensity", fogDensity);
            material.SetColor("_FogColor", fogColor);
            material.SetFloat("_FogStart", fogStart);
            material.SetFloat("_FogEnd", fogEnd);

            Graphics.Blit(src, dest, material);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
