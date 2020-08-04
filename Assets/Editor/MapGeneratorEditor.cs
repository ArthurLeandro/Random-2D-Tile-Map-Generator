using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor {

 
  public override void OnInspectorGUI() {
    DrawDefaultInspector();
    MapGenerator script = (MapGenerator)target;
    if(GUILayout.Button("Generate Map")){
      if (Application.isPlaying){
          script.InstantiateNewMap();
    }
    }
  }
}