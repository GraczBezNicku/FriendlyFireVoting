using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.Events.EventArgs;
using MEC;

namespace FriendlyFireVote
{
    public class EventsHandler
    {
        public List<Player> playersAfterHint = new List<Player>();

        public void OnStartRound()
        {
            if (Plugin.Instance.FFOffVotes == 0 && Plugin.Instance.FFOnVotes == 0 && Plugin.Instance.FFReverseVotes == 0)
                Plugin.Instance.winningFFMode = Plugin.Instance.Config.defaultFFMode;

            Plugin.Instance.FFMode = Plugin.Instance.winningFFMode;
            
            switch(Plugin.Instance.FFMode)
            {
                case "off":
                    Plugin.Instance.FFModeIndex = 1;
                    Server.FriendlyFire = false;
                    break;
                case "on":
                    Plugin.Instance.FFModeIndex = 0;
                    Server.FriendlyFire = true; 
                    break;
                case "reverse":
                    Plugin.Instance.FFModeIndex = 2;
                    Server.FriendlyFire = true; 
                    break;
            }

            playersAfterHint.Clear();

            Map.ClearBroadcasts();
            Map.Broadcast(Plugin.Instance.Config.bcDuration, Plugin.Instance.Config.startRoundBroadcast.Replace("{mode}", Plugin.Instance.Config.FFModeNames[Plugin.Instance.FFModeIndex]));

            Plugin.Instance.FFOffVotes = 0;
            Plugin.Instance.FFOnVotes = 0;
            Plugin.Instance.FFReverseVotes = 0;
            Plugin.Instance.votedIDs.Clear();
        }

        public void OnHurt(HurtingEventArgs ev)
        {
            if (ev.Attacker == null)
                return;

            if (Plugin.Instance.FFModeIndex != 2 || ev.Target.Role.Team != ev.Attacker.Role.Team)
                return;

            ev.Attacker.Hurt(ev.Amount);
            ev.IsAllowed = false;
        }

        public void OnVerified(VerifiedEventArgs ev)
        {
            if (Plugin.Instance.coroutines.Count == 0)
                Plugin.Instance.coroutines.Add(Timing.RunCoroutine(ResultsUpdater()));

            if (!Round.IsStarted)
            {
                ev.Player.Broadcast(Plugin.Instance.Config.bcDuration, Plugin.Instance.Config.preRoundBroadcast, shouldClearPrevious: Plugin.Instance.Config.clearBroadcastsBeforeInstructions);
                Timing.CallDelayed(Plugin.Instance.Config.bcDuration, () => playersAfterHint.Add(ev.Player));
            }
            else
                ev.Player.Broadcast(Plugin.Instance.Config.bcDuration, Plugin.Instance.Config.startRoundBroadcast.Replace("{mode}", Plugin.Instance.Config.FFModeNames[Plugin.Instance.FFModeIndex]));
        }

        public IEnumerator<float> ResultsUpdater()
        {
            while(true)
            {
                if(!Round.IsStarted)
                {
                    List<Player> playersToBC = Player.List.Where(x => playersAfterHint.Contains(x)).ToList();

                    if (playersToBC.Count != 0)
                    {
                        playersToBC.ForEach(x => x.ClearBroadcasts());
                        playersToBC.ForEach(x => x.Broadcast(9999, Plugin.Instance.Config.preResultsBroadcast.Replace("{ffonvotes}", Plugin.Instance.FFOnVotes.ToString()).Replace("{ffoffvotes}", Plugin.Instance.FFOffVotes.ToString()).Replace("{ffreversevotes}", Plugin.Instance.FFReverseVotes.ToString())));   
                    }
                }
                yield return Timing.WaitForSeconds(1f);
            }
        }
    }
}
