using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CustomrRenderPipeLine : RenderPipeline
{
    private CameraRender renderer = new CameraRender();
    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        foreach (Camera camera in cameras) 
        {
            renderer.Render(context, camera);
        }
    }


    
    
}
