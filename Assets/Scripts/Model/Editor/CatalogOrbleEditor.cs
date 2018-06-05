using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor(typeof(CatalogOrble))]
public class CatalogOrbleEditor : Editor {

	public override void OnInspectorGUI() {

		CatalogOrble catalogOrble = (CatalogOrble)target;

		catalogOrble.catalogNumber = EditorGUILayout.IntField("Catalog number", int.Parse(catalogOrble.name));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("species"), true);

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PropertyField(serializedObject.FindProperty("typeId"), true);
		EditorGUILayout.LabelField(catalogOrble.TypeStringName());
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.PropertyField(serializedObject.FindProperty("level"), true);

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PropertyField(serializedObject.FindProperty("colourId"), true);
		EditorGUILayout.LabelField(catalogOrble.ColorStringName());
		EditorGUILayout.EndHorizontal();

		serializedObject.ApplyModifiedProperties();


	}
}
