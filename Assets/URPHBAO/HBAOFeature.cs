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

        [Header("DrawScene Pass")]
        public RenderPassEvent renderPassEventOutputNormal = RenderPassEvent.AfterRenderingOpaques;
        public int renderPassEventOutputNormalOffset = 0;

        public int layerMask = -1;
        public List<string> shaderTagNames = new() { "SRPDefaultUnlit" };
        HBAORenderPass_DrawScene drawScenePass;
        HBAORenderPass_AO aoPass;

        [Header("AO Pass")]
        public RenderPassEvent renderPassEventAO = RenderPassEvent.AfterRenderingOpaques;
        public int renderPassEventAOOffset = 0;
        public Material hbaoMat;

        [Tooltip("ao size")]
        [Range(0, 1)] public float aoRangeMin = 0.1f, aoRangeMax = 1;

        [Tooltip("scale step distance(0.125)")]
        [Range(0.02f, .2f)] public float stepScale = 0.1f;

        [Tooltip("divide 360 into parts")]
        public int dirCount = 10;

        [Tooltip("move step alone a direction")]
        public int stepCount = 4;

        [Tooltip("calc normal from _CameraDepthTexture or use _CameraNormalsTexture")]
        public bool isNormalFromDepth = false;

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
