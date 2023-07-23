
using OpenRA;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapCreator))]
public class MapCreatorEditor : Editor
{

    private MapCreator creator;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        creator = target as MapCreator;
        if (creator)
        {
            if (GUILayout.Button("绘制"))
            {
                creator.CreateMap();
            }  
        }
    }
}