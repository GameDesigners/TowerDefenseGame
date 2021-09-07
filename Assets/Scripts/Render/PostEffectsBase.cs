using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *   重构时间：2021.3.15
 *   名称：PostEffectsBase.cs
 *   描述：屏幕后处理基类
 *   编写：ZhuSenlin
 */

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class PostEffectsBase : MonoBehaviour
{
	/// <summary>
	/// 检测着色器和材质信息
	/// </summary>
	/// <param name="shader"></param>
	/// <param name="material"></param>
	/// <returns></returns>
	protected Material CheckShaderAndCreateMaterial(Shader shader, Material material)
	{
		if (shader == null)
		{
			return null;
		}

		if (shader.isSupported && material && material.shader == shader)
			return material;

		if (!shader.isSupported)
		{
			return null;
		}
		else
		{
			material = new Material(shader);
			material.hideFlags = HideFlags.DontSave;
			if (material)
				return material;
			else
				return null;
		}
	}

}