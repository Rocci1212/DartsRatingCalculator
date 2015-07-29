using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartsRatingCalculator
{
    public class Match
    {
        // for example http://stats.mmdl.org/index.php?view=match&matchid=36527 would have a match id of 36527
        int MatchId;
        int WeekNumber;
        Squad AwaySquad, HomeSquad;
        Campaign _Campaign;
        Game[] Games;

        public Match(string matchDesc, string squadDesc, string campaignDesc)
        {
            MatchId = Convert.ToInt32(matchDesc.Substring(matchDesc.IndexOf("#") + 1));
            WeekNumber = Convert.ToInt32(squadDesc.Substring(5, squadDesc.IndexOf(":") - 5));
            _Campaign = Campaign.GetCampaignFromDesc(campaignDesc);
            Squad.GetSquadsFromDesc(squadDesc, _Campaign, ref AwaySquad, ref HomeSquad);
        }
    }
}
