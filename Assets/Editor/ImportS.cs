using UnityEngine;
using UnityEditor;

public class ImportS : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        int width;
        int height;
        TextureImporter textureImporter = (TextureImporter)assetImporter;
        textureImporter.GetSourceTextureWidthAndHeight(out width, out height);
        int uiuiuiui = Mathf.Max(width, height);
        textureImporter.spritePixelsPerUnit = uiuiuiui;
    }
}
