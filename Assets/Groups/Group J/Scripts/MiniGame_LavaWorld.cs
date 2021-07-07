using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Groups.Group_J.Scripts
{
    public class MiniGame_LavaWorld : MiniGame
    {
        public override string getDisplayName()
        {
            return "Meteorfall";
        }

        public override MiniGameType getMiniGameType()
        {
            return MiniGameType.teamVsTeam;
        }

        public override string getSceneName()
        {
            return "Minigame_Group_J";
        }
    }
}
