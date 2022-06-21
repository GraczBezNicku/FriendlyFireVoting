using CommandSystem;
using Exiled.API.Features;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendlyFireVote.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class VoteCommand : ICommand
    {
        public string Command { get; } = "ff";

        public string[] Aliases { get; } = { };

        public string Description { get; } = "Lets players vote for Friendly Fire status in the current round.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!(sender is PlayerCommandSender playerSender))
            {
                response = "Only players can use this command.";
                return false;
            }

            if(Round.IsStarted)
            {
                response = Plugin.Instance.Config.roundStartedResponse;
                return false;
            }

            if (arguments.Count < 1)
            {
                response = Plugin.Instance.Config.wrongUsageResponse;
                return false;
            }

            Player pSender = Player.Get(sender);

            if(Plugin.Instance.votedIDs.Contains(pSender.RawUserId))
            {
                response = Plugin.Instance.Config.alreadyVotedResponse;
                return false;
            }

            string voteArg = arguments.At(0);

            switch(voteArg)
            {
                case "off":
                    Plugin.Instance.FFOffVotes++;
                    if (Plugin.Instance.FFOffVotes > Plugin.Instance.FFOnVotes && Plugin.Instance.FFOffVotes > Plugin.Instance.FFReverseVotes) Plugin.Instance.winningFFMode = "off";
                    Plugin.Instance.votedIDs.Add(pSender.RawUserId);
                    break;
                case "on":
                    Plugin.Instance.FFOnVotes++;
                    if (Plugin.Instance.FFOnVotes > Plugin.Instance.FFOffVotes && Plugin.Instance.FFOnVotes > Plugin.Instance.FFReverseVotes) Plugin.Instance.winningFFMode = "on";
                    Plugin.Instance.votedIDs.Add(pSender.RawUserId);
                    break;
                case "reverse":
                    Plugin.Instance.FFReverseVotes++;
                    if (Plugin.Instance.FFReverseVotes > Plugin.Instance.FFOnVotes && Plugin.Instance.FFReverseVotes > Plugin.Instance.FFOffVotes) Plugin.Instance.winningFFMode = "reverse";
                    Plugin.Instance.votedIDs.Add(pSender.RawUserId);
                    break;
                default:
                    response = Plugin.Instance.Config.wrongUsageResponse;
                    return false;
            }

            response = "Success!";
            return true;
        }
    }
}
