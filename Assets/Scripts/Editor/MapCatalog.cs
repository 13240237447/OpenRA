using System.Collections;
using System.Collections.Generic;
using OpenRA;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapSettle))]
public class MapCatalog : Editor
{
   public override void OnInspectorGUI()
   {
      base.OnInspectorGUI();
      MapSettle mapSettle = target as MapSettle;
      if (mapSettle)
      {
         if (GUILayout.Button("分类"))
         {
            mapSettle.AutoGroup();
         }
         
         if (GUILayout.Button("设置标签"))
         {
            mapSettle.SetTag();
         }
      }
   }
}
