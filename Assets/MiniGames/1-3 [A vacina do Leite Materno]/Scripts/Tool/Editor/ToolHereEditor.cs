using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ToolHere))]
public class ToolHereEditor : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        ToolHere myScript = (ToolHere)target;
        string nameButtom;

        if (!myScript.active) {
            nameButtom = "Ativar Ludica";
        } else {
            nameButtom = "Ativar Ditatica";
        }

        if (GUILayout.Button(nameButtom)) {
            myScript.Toggle();
        }

        
    }

}
