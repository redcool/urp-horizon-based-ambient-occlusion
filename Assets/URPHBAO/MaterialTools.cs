using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace URPHBAO
{
    public static class MaterialTools
    {
        public static void SetKeyword(this Material material, string keyword, bool enable)
        {
            if (string.IsNullOrEmpty(keyword) || material.IsKeywordEnabled(keyword) == enable)
                return;

            if (enable)
            {
                material.EnableKeyword(keyword);
            }
            else
            {
                material.DisableKeyword(keyword);
            }
        }
    }
}
