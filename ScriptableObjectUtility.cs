using UnityEngine;
using UnityEditor;
using System.IO;

#if UNITY_EDITOR
public static class ScriptableObjectUtility
{
	/// <summary>
	//	This makes it easy to create, name and place unique new ScriptableObject asset files.
	/// </summary>
	public static T CreateAsset<T>() where T : ScriptableObject
	{
		T asset = ScriptableObject.CreateInstance<T>();		
		return asset;
	}

	public static void SaveAsset<T>(Object asset, string path)
  {
		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).ToString() + ".asset");

		AssetDatabase.CreateAsset(asset, assetPathAndName);

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset;

	}
}
#endif