using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
namespace URPHBAO
{
    public class HBAORenderPass_DrawScene : ScriptableRenderPass
    {
        public HBAOFeature feature;

        CommandBuffer cmd = new CommandBuffer { name = nameof(HBAORenderPass_DrawScene) };


        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            var cameraData = renderingData.cameraData;
            if (cameraData.cameraType > CameraType.SceneView)
                return;

            var renderer = renderingData.cameraData.renderer;
            var desc = cameraData.cameraTargetDescriptor;

            if (RTHandleTools.TryCreateNormalTexture(desc, feature._CameraNormalsTexture))
            {
                ConfigureClear(ClearFlag.Color, Color.clear);
            }

            cmd.SetGlobalTexture(feature._CameraNormalsTexture, RTHandleTools.normalsTexture);

            RTHandleTools.SetTargets(renderer.cameraColorTargetHandle, RTHandleTools.normalsTexture);

            ConfigureTarget(RTHandleTools.colorTargets_2, renderer.cameraDepthTargetHandle);
        }


        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var cameraData = renderingData.cameraData;
            if (cameraData.cameraType > CameraType.SceneView)
                return;

            cmd.BeginSample(cmd.name);
            cmd.Execute(ref context);

            DrawSceneOutputColorNormal(ref context, ref renderingData);

            cmd.EndSample(cmd.name);
            cmd.Execute(ref context);
        }

        void DrawSceneOutputColorNormal(ref ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var cullResults = renderingData.cullResults;
            var filterSettings = new FilteringSettings(RenderQueueRange.all, feature.layerMask);
            var drawSettings = new DrawingSettings(new ShaderTagId("UniversalForward"), new SortingSettings(renderingData.cameraData.camera));

            for (int i = 0; i < feature.shaderTagNames.Count; i++)
            {
                drawSettings.SetShaderPassName(i, new ShaderTagId(feature.shaderTagNames[i]));
            }

            context.DrawRenderers(cullResults, ref drawSettings, ref filterSettings);
        }


        public override void OnCameraCleanup(CommandBuffer cmd)
        {
        }
    }


}