using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraRender
{
    static ShaderTagId[] legacyShaderTagIds = {
        new ShaderTagId("Always"),
        new ShaderTagId("ForwardBase"),
        new ShaderTagId("PrepassBase"),
        new ShaderTagId("Vertex"),
        new ShaderTagId("VertexLMRGBM"),
        new ShaderTagId("VertexLM")
    };
    static Material errorMaterial;

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

        if (!Cull())
        {
            return;
        }
        
        SetUp();
        DrawVisibleGeometry();
        DrawUnsupportedShaders();
        Submit();
    }

    void SetUp()
    {
        context.SetupCameraProperties(camera);
        buffer.ClearRenderTarget(true, true, Color.clear);
        buffer.BeginSample(bufferName);
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
    
    
    void DrawUnsupportedShaders () {
        if (errorMaterial == null) {
            errorMaterial =
                new Material(Shader.Find("Hidden/InternalErrorShader"));
        }
        var drawingSettings = new DrawingSettings(
            legacyShaderTagIds[0], new SortingSettings(camera)
        ) {
            overrideMaterial = errorMaterial
        };
        for (int i = 1; i < legacyShaderTagIds.Length; i++) {
            drawingSettings.SetShaderPassName(i, legacyShaderTagIds[i]);
        }
        var filteringSettings = FilteringSettings.defaultValue;
        context.DrawRenderers(
            _cullingResults, ref drawingSettings, ref filteringSettings
        );
    }
    
    void Submit () {
        buffer.EndSample(bufferName);
        ExecuteBuffer();
        context.Submit();
    }
    
    
    void ExecuteBuffer () {
        context.ExecuteCommandBuffer(buffer);
        buffer.Clear();
    }
    
    
    
    
    
}
