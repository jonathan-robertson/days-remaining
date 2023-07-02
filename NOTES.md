# Notes

- [Notes](#notes)
  - [TODO (for another mod)](#todo-for-another-mod)
  - [Detailed Research](#detailed-research)
    - [Server Lobby](#server-lobby)
    - [Compass Day Time](#compass-day-time)
      - [What causes it to hide?](#what-causes-it-to-hide)
    - [Map Day Time](#map-day-time)
  - [Final Impressions](#final-impressions)
    - [Networking Impact](#networking-impact)
    - [Possible Extra Features \& Networking Impact](#possible-extra-features--networking-impact)

## TODO (for another mod)

- update map date
- update trader restock date to days remaining
- update vending machine rental to days remaining

## Detailed Research

This section contains a rundown of how things currently work in vanilla, what feeds their values, and some brief thoughts on why something could or couldn't be easily manipulated within the existing controllers.

### Server Lobby

Localizations.txt: `xuiDayTimeLong` -> `Day: {0} Time: {1:00}:{2:00}` (client-side)

`ValueDisplayFormatters.WorldTime((ulong)((long)_worldTime), Localization.Get("xuiDayTimeLong"))`

### Compass Day Time

`/windows/window[@name='windowCompass']/sprite/label`
*controller="CompassWindow"*

```xml
<label depth="2" width="300" height="30" text="{daytitle}: [{daycolor}]{day}[-] {timetitle}: {time}" font_size="28" pivot="center" effect="outline" upper_case="true" justify="center"/>
```

- `daytitle` -> `CompassWindow` Controller -> `XUiC_CompassWindow` ->`Localization.Get("xuiDay")` -> "Day"
- `daycolor` -> `CompassWindow` Controller -> `XUiC_CompassWindow` -> automatically set by BM day settings

- `day` -> `CompassWindow` Controller -> `XUiC_CompassWindow` -> `this.dayFormatter.Format(GameUtils.WorldTimeToDays(GameManager.Instance.World.worldTime));`
  - `_i.ToString()`
  - hooking into this formatter would be "THE BEST WAY TO MOD THIS" from a number into a word... that is: if it could be controlled/adjusted from the server's end. Since it can't, we'll need to find another approach and instead try to make this value invisible or completely replace the element with server-fed CVars.
- `timetitle` -> `CompassWindow` Controller -> `XUiC_CompassWindow` -> `Localization.Get("xuiTime")` -> "Time"

#### What causes it to hide?

### Map Day Time

This line is referenced under `/windows/window[@name='mapArea']/panel[@name='content']/rect[@name='content']/panel[@name='mapView']`
*controller="MapStats"*

```xml
<label depth="2" name="dayTimeLabel" pos="40,-18" width="250" height="28" text="{mapdaytimetitle}: [DECEA3]{mapdaytime}[-]" font_size="26"/>
```

- `mapdaytimetitle` -> `MapStats` Controller -> `XUiC_MapStats.cs` -> `Localization.Get("xuiDayTime")` -> "Day/Time"
- `mapdaytime` -> `MapStats` Controller -> `XUiC_MapStats.cs` -> `this.mapdaytimeFormatter.Format(GameManager.Instance.World.worldTime);`
  - `ValueDisplayFormatters.WorldTime(_worldTime, "{0}/{1:00}:{2:00}")`
  - this formatting method is run on the client and does not refer to the server; even though the server feeds the client `GameManager.Instance.World.worldTime`
  - hooking into this formatter would be "THE BEST WAY TO MOD THIS" from a number into a word... that is: if it could be controlled/adjusted from the server's end. Since it can't, we'll need to find another approach and instead try to make this value invisible or completely replace the element with server-fed CVars.

## Final Impressions

Since the goal is to replace numbers with text, the final solution will likely involve replacing the UI windows within the game with other windows that feed off of CVars that are regularly fed to clients via a server-side hook.

### Networking Impact

Component | Networking Overhead
--- | ---
Coroutine monitoring in-game time changes and updating players each minute | almost zero; cvar updates are very lightweight and spacing them out to hit each online player only 1 time per minute is in super-healthy territory for a server. The added CPU overhead of checking for time changes will be very simple and shouldn't put almost any strain on a server.

### Possible Extra Features & Networking Impact

Optional Components | Networking Overhead
--- | ---
Add Seconds to Time Display | 60x more overhead vs update-per-minute (60 more network calls per player), likely a low impact due to extremely small network package - but it all adds up over time
Disable/Enable time based on an equipped item/mod (think darkness falls' watch mod) | some networking overhead added
Update certain debuffs to effect time display: concussion, for example | zero networking overhead; this would be done in xml and processed locally by client
Add admin command to enable/disable time for all players | extremely simple
