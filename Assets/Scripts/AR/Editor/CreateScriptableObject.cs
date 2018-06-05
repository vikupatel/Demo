using UnityEngine;
using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateScriptableObject {
	
	[MenuItem("Create/CatalogOrble")]
	public static void CreateCatalogOrble()
	{
		CatalogOrble newAsset = ScriptableObject.CreateInstance<CatalogOrble>();
		
		AssetDatabase.CreateAsset(newAsset, "Assets/Resources/Catalog/NewCatalogOrble.asset");
		AssetDatabase.SaveAssets();
		
		EditorUtility.FocusProjectWindow();
		
		Selection.activeObject = newAsset;
	}
}
