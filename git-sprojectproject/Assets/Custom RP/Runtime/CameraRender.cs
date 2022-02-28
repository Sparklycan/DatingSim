using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public partial class CameraRender
{
    
    ScriptableRenderContext context;

    static ShaderTagId unlitShaderTagId = new ShaderTagId("SRPDefaultUnlit");
    
    Camera camera;

    const string bufferName = "Render Camera";

    CommandBuffer buffer = new CommandBuffer {
        name = bufferName
    };

    private CullingResults _cullingResults;
    
    bool Cull () {
        if (camera.TryGetCullingParameters(out ScriptableCullingParameters p))
        {
            _cullingResults = context.Cull(ref p);
            return true;
        }
        return false;
    }
    
    public void Render (ScriptableRenderContext context, Camera camera) {
        this.context = context;
        this.camera = camera;

        PrepareForSceneWindow();
        if (!Cull())
        {
            return;
        }
        
        SetUp();
        DrawVisibleGeometry();
        DrawUnsupportedShaders();
        DrawGizmos();
        Submit();
    }

    void SetUp()
    {
        context.SetupCameraProperties(camera);
        CameraClearFlags flags = camera.clearFlags;
        buffer.ClearRenderTarget(flags <= CameraClearFlags.Depth, flags == CameraClearFlags.Color, flags == CameraClearFlags.Color ? camera.backgroundColor.linear : Color.clear);
        buffer.BeginSample(SampleName);
        ExecuteBuffer();
    }
    
    void DrawVisibleGeometry () {
        var sortingSettings = new SortingSettings(camera) {criteria = SortingCriteria.CommonOpaque
        };        var drawingSettings = new DrawingSettings(unlitShaderTagId, sortingSettings);
        var filteringSettings = new FilteringSettings(RenderQueueRange.opaque);
        
        context.DrawRenderers(
            _cullingResults, ref drawingSettings, ref filteringSettings
        );

        context.DrawSkybox(camera);
        
        sortingSettings.criteria = SortingCriteria.CommonTransparent;
        drawingSettings.sortingSettings = sortingSettings;
        filteringSettings.renderQueueRange = RenderQueueRange.transparent;

        context.DrawRenderers(
            _cullingResults, ref drawingSettings, ref filteringSettings
        );
    }
    
    
    void Submit () {
        buffer.EndSample(SampleName);
        ExecuteBuffer();
        context.Submit();
    }
    
    
    void ExecuteBuffer () {
        context.ExecuteCommandBuffer(buffer);
        buffer.Clear();
    }
    
    
    
    
    
}
