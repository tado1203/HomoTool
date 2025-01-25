using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC.SDKBase;

namespace HomoTool.Extensions
{
    public static class VRCPlayerApiExtensions
    {
        public static GamePlayer GetPlayer(this VRCPlayerApi playerApi)
        {
            return playerApi.gameObject.GetComponent<GamePlayer>();
        }
    }
}
