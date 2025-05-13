using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
namespace URPHBAO
{
    public class HBAORenderPass_AO : ScriptableRenderPass
    {
        public HBAOFeature feature;

        CommandBuffer cmd = new CommandBuffer { name = nameof(HBAORenderPass_AO) };

        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (!feature.hbaoMat)
                return;

            var cameraData = renderingData.cameraData;
            if (cameraData.cameraType > CameraType.SceneView)
                return;
            cmd.BeginSample(cmd.name);
            cmd.Execute(ref context);

            feature.hbaoMat.SetFloat("_AORangeMin", feature.aoRangeMin);
            feature.hbaoMat.SetFloat("_AORangeMax", feature.aoRangeMax);
            feature.hbaoMat.SetFloat("_StepScale", feature.stepScale);
            feature.hbaoMat.SetInt("_DirCount", feature.dirCount);
            feature.hbaoMat.SetInt("_StepCount", feature.stepCount);
            feature.hbaoMat.SetKeyword("_NORMAL_FROM_DEPTH", feature.isNormalFromDepth);

            cmd.DrawProcedural(Matrix4x4.identity, feature.hbaoMat, 0, MeshTopology.Triangles, 3);

            cmd.EndSample(cmd.name);
            cmd.Execute(ref context);
        }



        public override void OnCameraCleanup(CommandBuffer cmd)
        {
        }
    }

}
