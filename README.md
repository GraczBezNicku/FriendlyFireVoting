# FriendlyFireVoting
A simple EXILED plugin to give your players more control over the game!
# Requirements
EXILED 5.2.1
# Usage
Open the console while in lobby, press `~` and type in `.ff on/off/reverse`
# Default Config
```
friendly_fire_voting:
  is_enabled: true
  # Broadcasted to every player upon round start and players joining mid-round
  start_round_broadcast: 'Friendly Fire mode for this round: {mode}'
  # Will clear all broadcasts on joining the server, so the instructions are not interrupted by previous broadcasts
  clear_broadcasts_before_instructions: true
  # Broadcasted to every player upon joining before the round starts.
  pre_round_broadcast: >-
    <size=25>This server is using a voting-based Friendly Fire system. Click (~) to vote.

    .ff on - FF <color=red>Enabled</color>

    .ff off - FF <color=green>Disabled</color>

    .ff reversed - FF <color=yellow>Reversed</color>

    </size>
  # Broadcasted to every player before the round starts after preRoundHint stops displaying. Can use: {ffonvotes} {ffoffvotes} {ffreversevotes}
  pre_results_broadcast: >-
    Current votes:

    <size=35><color=red>FF - ON</color>: {ffonvotes}

    <color=green>FF - OFF</color>: {ffoffvotes}

    <color=yellow>FF - Reversed</color>: {ffreversevotes}

    </size>
  # Response in game console to inform players they already voted.
  already_voted_response: You have already voted!
  # Response in game console to inform players that the round already started.
  round_started_response: The round has already started!
  # Response given upon entering invalid / zero arguments
  wrong_usage_response: 'Usage: .ff off/on/reverse'
  # Broadcast duration for every broadcast.
  bc_duration: 15
  # Displayed in place of {name} in startRoundBroadcast. *Order matters*
  f_f_mode_names:
  - <color=red>ON</color>
  - <color=green>OFF</color>
  - <color=yellow>Reversed</color>
  # Default FF option if nobody votes.
  default_f_f_mode: reverse
  ```
