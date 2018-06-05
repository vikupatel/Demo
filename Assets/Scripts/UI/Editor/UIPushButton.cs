using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;

[CustomEditor(typeof(UIPushButton))]
public class UIPushButtonEditor : UnityEditor.UI.ButtonEditor {


	public override void OnInspectorGUI() {

		UIPushButton component = (UIPushButton)target;

		component.pushScale = EditorGUILayout.FloatField("Push scale", component.pushScale);
		component.pushSpeed = EditorGUILayout.FloatField("Push speed", component.pushSpeed);

		base.OnInspectorGUI();

	

	}
}