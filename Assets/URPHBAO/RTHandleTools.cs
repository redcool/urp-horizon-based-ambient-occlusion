using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
namespace URPHBAO
{
    public static class RTHandleTools
    {
        public static RTHandle normalsTexture;
        public static RTHandle[] colorTargets_2 = new RTHandle[2];

        public static bool IsInvalid(this RTHandle rth, RenderTextureDescriptor desc)
        {
            return rth == null || !rth.rt || rth.rt.width != desc.width || rth.rt.height != desc.height;
        }

        public static bool TryCreateNormalTexture(RenderTextureDescriptor desc, string name)
        {
            var isInvalid = normalsTexture.IsInvalid(desc);
            if (isInvalid)
            {
                desc.graphicsFormat = GraphicsFormat.R16G16B16A16_SFloat;
                desc.depthBufferBits = 0;
                normalsTexture = RTHandles.Alloc(desc, name: name);
            }
            return isInvalid;
        }

        public static void SetTargets(RTHandle cameraColorTargetHandle, RTHandle normalsTexture)
        {
            colorTargets_2[0] = cameraColorTargetHandle;
            colorTargets_2[1] = normalsTexture;
        }
    }
}