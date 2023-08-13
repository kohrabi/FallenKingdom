using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraPostProcess : MonoBehaviour
{
    public Material InvertColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (InvertColor == null)
        {
            Graphics.Blit(source, destination);
            return;
        }
        //RenderTexture main = RenderTexture.GetTemporary(320, 180);
        Graphics.Blit(source, destination, InvertColor);
        //Graphics.Blit(main, destination);

        //RenderTexture.ReleaseTemporary(main);
    }
}
