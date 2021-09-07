using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(PolygonCollider2D))]
public class UIPolygon : Image
{
    private PolygonCollider2D _polygon = null;
    private PolygonCollider2D polygon
    {
        get
        {
            if (_polygon == null)
                _polygon = GetComponent<PolygonCollider2D>();
            return _polygon;
        }
    }

    //设置只响应点击，不进行渲染
    protected UIPolygon()
    {
        useLegacyMeshGeneration = true;
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
    }

    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        return polygon.OverlapPoint(eventCamera.ScreenToWorldPoint(screenPoint));
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(UIPolygon), true)]
public class UIPolygonInspector : Editor
{
    public override void OnInspectorGUI()
    {
        //什么都不写用于隐藏面板的显示
    }
}
#endif