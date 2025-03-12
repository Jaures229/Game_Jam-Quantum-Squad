using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BatchPivotChanger : EditorWindow
{
    private Vector2 pivot = new Vector2(0.5f, 0.5f); // Pivot par défaut (centre)

    [MenuItem("Tools/Batch Set Pivot")]
    public static void ShowWindow()
    {
        GetWindow<BatchPivotChanger>("Batch Set Pivot");
    }

    private void OnGUI()
    {
        GUILayout.Label("Changer le Pivot des Sprites", EditorStyles.boldLabel);
        pivot = EditorGUILayout.Vector2Field("Pivot (X, Y)", pivot);

        if (GUILayout.Button("Appliquer aux images sélectionnées"))
        {
            SetPivotForSelectedSprites(pivot);
        }
    }

    private static void SetPivotForSelectedSprites(Vector2 newPivot)
    {
        foreach (Object obj in Selection.objects)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            if (textureImporter != null)
            {
                textureImporter.textureType = TextureImporterType.Sprite; // Forcer le mode Sprite
                textureImporter.spriteImportMode = SpriteImportMode.Single; // Assurer un seul sprite

                // Modifier les paramètres du sprite
                TextureImporterSettings settings = new TextureImporterSettings();
                textureImporter.ReadTextureSettings(settings);

                settings.spriteAlignment = (int)SpriteAlignment.Custom; // Activer le mode "Custom"
                settings.spritePivot = newPivot; // Appliquer le pivot

                textureImporter.SetTextureSettings(settings);
                textureImporter.SaveAndReimport(); // Réimporter pour appliquer les changements
            }
        }

        Debug.Log($"✅ Pivot appliqué pour {Selection.objects.Length} images !");
    }
}
