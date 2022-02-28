using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[CanEditMultipleObjects]
[CustomEditorForRenderPipeline(typeof(Light), typeof(CustomRenderPipeLineAsset))]
public class CustomLightEditor : LightEditor
{
    
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if (
            !settings.lightType.hasMultipleDifferentValues &&
            (LightType)settings.lightType.enumValueIndex == LightType.Spot
        )
        {
            settings.DrawInnerAndOuterSpotAngle();
            settings.ApplyModifiedProperties();
        }
    }
    
    
    
}
