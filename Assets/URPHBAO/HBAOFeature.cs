using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
namespace URPHBAO
{
    public class HBAOFeature : ScriptableRendererFeature
    {
        public string _CameraNormalsTexture = "_CameraNormalsTexture";

        public RenderPassEvent renderPassEventOutputNormal = RenderPassEvent.AfterRenderingOpaques;
        public int renderPassEventOutputNormalOffset = 0;

        public RenderPassEvent renderPassEventAO = RenderPassEvent.AfterRenderingOpaques;
        public int renderPassEventAOOffset = 0;

        HBAORenderPass_DrawScene drawScenePass;
        HBAORenderPass_AO aoPass;
        public int layerMask = -1;
        public List<string> shaderTagNames = new() { "SRPDefaultUnlit" };

        public Material hbaoMat;

        public override void Create()
        {
            drawScenePass = new HBAORenderPass_DrawScene() { feature = this };
            drawScenePass.renderPassEvent = renderPassEventOutputNormal + renderPassEventOutputNormalOffset;

            aoPass = new HBAORenderPass_AO() { feature = this };
            aoPass.renderPassEvent = renderPassEventAO + renderPassEventAOOffset;
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            renderer.EnqueuePass(drawScenePass);
            renderer.EnqueuePass(aoPass);
        }
    }

}
