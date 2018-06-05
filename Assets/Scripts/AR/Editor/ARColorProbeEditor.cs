using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


[CustomEditor(typeof(ARColorProbe))]
public class ARColorProbeEditor : Editor {

	public override void OnInspectorGUI() {

		ARColorProbe probe = (ARColorProbe)target;

		EditorGUILayout.PropertyField(serializedObject.FindProperty("readingHistorySize"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("averageColor"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("foundSampleColor"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("samples"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("drawPreview"), true);

		serializedObject.ApplyModifiedProperties();

	}
}
