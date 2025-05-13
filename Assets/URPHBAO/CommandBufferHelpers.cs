using UnityEngine.Rendering;

namespace URPHBAO
{
    public static class CommandBufferHelpers
    {

        public static void Execute(this CommandBuffer cmd, ref ScriptableRenderContext context)
        {
            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();
        }
    }
}