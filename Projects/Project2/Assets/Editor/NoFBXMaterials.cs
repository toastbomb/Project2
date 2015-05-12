using UnityEngine;
using UnityEditor;

public class NoFBXMaterials : AssetPostprocessor
{
	void OnPreprocessModel()
	{
		ModelImporter importer = assetImporter as ModelImporter;
		importer.importMaterials = false;
	}
}
