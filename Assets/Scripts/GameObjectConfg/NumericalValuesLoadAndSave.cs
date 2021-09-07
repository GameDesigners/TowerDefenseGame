using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
/*
 *   重构时间：2021.3.15
 *   名称：NumericalValuesLoadAndSave.cs
 *   描述：基于XML的数值读写类
 *   编写：ZhuSenlin
 */

/// <summary>
/// Base On XML
/// </summary>
public class NumericalValuesLoadAndSave
{
    /// <summary>
    /// 文件路径
    /// </summary>
    public string FilePath = Application.dataPath;

    public static object Deserialize(Type type, string xml)
    {
        using (StringReader sr = new StringReader(xml))
        {
            XmlSerializer xmldes = new XmlSerializer(type);
            return xmldes.Deserialize(sr);
        }
    }

    /// <summary>  
    /// 序列化  
    /// </summary>  
    /// <param name="type">类型</param>  
    /// <param name="obj">对象</param>  
    /// <returns></returns>  
    public static string Serializer(Type type, object obj)
    {
        MemoryStream Stream = new MemoryStream();
        XmlSerializer xml = new XmlSerializer(type);
        //序列化对象  
        xml.Serialize(Stream, obj);
        Stream.Position = 0;
        StreamReader sr = new StreamReader(Stream);
        string str = sr.ReadToEnd();

        sr.Dispose();
        Stream.Dispose();

        return str;
    }
}