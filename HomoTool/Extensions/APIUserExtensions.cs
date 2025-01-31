using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.Core;

namespace HomoTool.Extensions
{
    public enum TrustRanks
    {
        Visitor,
        NewUser,
        User,
        Known,
        Trusted
    }

    public static class APIUserExtensions
    {
        public static bool IsFriend(this APIUser a, APIUser b)
        {
            if (b == null)
                return false;

            return a.friendIDs.Contains(b.id);
        }

        public static TrustRanks GetTrustRank(this APIUser apiUser)
        {
            if (apiUser.tags.Count > 0)
            {
                if (apiUser.tags.Contains("system_trust_veteran") && apiUser.tags.Contains("system_trust_trusted"))
                    return TrustRanks.Trusted;

                if (apiUser.tags.Contains("system_trust_trusted"))
                    return TrustRanks.Known;

                if (apiUser.tags.Contains("system_trust_known"))
                    return TrustRanks.User;

                if (apiUser.tags.Contains("system_trust_basic"))
                    return TrustRanks.NewUser;
            }

            return TrustRanks.Visitor;
        } 

        public static Color GetPlayerColor(this APIUser apiUser)
        {
            if (apiUser == null)
                return Color.white;

            if (APIUser.CurrentUser.IsFriend(apiUser))
				return Color.yellow;

			switch (apiUser.GetTrustRank())
			{
				case TrustRanks.Visitor:
					return Color.white;
				case TrustRanks.NewUser:
					return new Color(21f/255f, 91f/255f, 190f/255f);
				case TrustRanks.User:
					return new Color(39f/255f, 199f/255f, 86f/255f);
				case TrustRanks.Known:
					return new Color(252f/255f, 120f/255f, 65f/255f);
				case TrustRanks.Trusted:
					return new Color(119f/255f, 62f/255f, 216f/255f);	
			}

			return Color.white;
        }
    }
}