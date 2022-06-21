using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendlyFireVote
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        [Description("Broadcasted to every player upon round start and players joining mid-round")]
        public string startRoundBroadcast { get; set; } = "Friendly Fire mode for this round: {mode}";

        [Description("Will clear all broadcasts on joining the server, so the instructions are not interrupted by previous broadcasts")]
        public bool clearBroadcastsBeforeInstructions { get; set; } = true;

        [Description("Broadcasted to every player upon joining before the round starts.")]
        public string preRoundBroadcast { get; set; } = "<size=25>This server is using a voting-based Friendly Fire system. Click (~) to vote.\n.ff on - FF <color=red>Enabled</color>\n.ff off - FF <color=green>Disabled</color>\n.ff reversed - FF <color=yellow>Reversed</color>\n</size>";

        [Description("Broadcasted to every player before the round starts after preRoundHint stops displaying. Can use: {ffonvotes} {ffoffvotes} {ffreversevotes}")]
        public string preResultsBroadcast { get; set; } = "Current votes:\n<size=35><color=red>FF - ON</color>: {ffonvotes}\n<color=green>FF - OFF</color>: {ffoffvotes}\n<color=yellow>FF - Reversed</color>: {ffreversevotes}\n</size>";

        [Description("Response in game console to inform players they already voted.")]
        public string alreadyVotedResponse { get; set; } = "You have already voted!";

        [Description("Response in game console to inform players that the round already started.")]
        public string roundStartedResponse { get; set; } = "The round has already started!";

        [Description("Response given upon entering invalid / zero arguments")]
        public string wrongUsageResponse { get; set; } = "Usage: .ff off/on/reverse";

        [Description("Broadcast duration for every broadcast.")]
        public ushort bcDuration { get; set; } = 15;

        [Description("Displayed in place of {name} in startRoundBroadcast. *Order matters*")]
        public List<string> FFModeNames { get; set; } = new List<string>()
        {
            "<color=red>ON</color>",
            "<color=green>OFF</color>",
            "<color=yellow>Reversed</color>"
        };

        [Description("Default FF option if nobody votes.")]
        public string defaultFFMode { get; set; } = "reverse";
    }
}
