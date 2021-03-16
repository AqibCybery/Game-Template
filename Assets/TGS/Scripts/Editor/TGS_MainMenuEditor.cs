using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TGS_MainMenu))]
public class TGS_MainMenuEditor : Editor
{
    public override void OnInspectorGUI()
    {
       
        Texture t = Resources.Load("Logo")as Texture;
        GUILayout.Box(t);
      
        TGS_MainMenu myTarget = (TGS_MainMenu)target;
        if (GUILayout.Button("Clear PlayerPref"))
        {
            myTarget.ClearPlayerPref();
        }
        myTarget.experience = EditorGUILayout.IntField("Experience", myTarget.experience);
        EditorGUILayout.LabelField("Level", myTarget.Level.ToString());
        DrawDefaultInspector();
    }

}
