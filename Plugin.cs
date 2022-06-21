using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEC;

namespace FriendlyFireVote
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance;
        public EventsHandler eventsHandler;

        public override string Author => "GBN";
        public override string Name => "FriendlyFireVoting";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(5, 2, 1);

        public List<string> votedIDs = new List<string>();

        public int FFOnVotes = 0;
        public int FFOffVotes = 0;
        public int FFReverseVotes = 0;

        public string FFMode = "";
        public int FFModeIndex = 0;
        public string winningFFMode = "";

        public List<CoroutineHandle> coroutines = new List<CoroutineHandle>();

        public override void OnEnabled()
        {
            Instance = this;
            eventsHandler = new EventsHandler();

            Exiled.Events.Handlers.Player.Verified += eventsHandler.OnVerified;
            Exiled.Events.Handlers.Server.RoundStarted += eventsHandler.OnStartRound;
            Exiled.Events.Handlers.Player.Hurting += eventsHandler.OnHurt;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            coroutines.Clear();

            Exiled.Events.Handlers.Player.Verified -= eventsHandler.OnVerified;
            Exiled.Events.Handlers.Server.RoundStarted -= eventsHandler.OnStartRound;
            Exiled.Events.Handlers.Player.Hurting -= eventsHandler.OnHurt;

            eventsHandler = null;
            Instance = null;

            base.OnDisabled();
        }
    }
}
