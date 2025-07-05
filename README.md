# Days Remaining [![Status: 💟 End of Life](https://img.shields.io/badge/💟%20Status-End%20of%20Life-blue.svg)](#support)

[![🧪 Tested with 7DTD 1.3 (b9)](https://img.shields.io/badge/🧪%20Tested%20with-7DTD%201.3%20(b9)-blue.svg)](https://7daystodie.com/)
[![🧪 Tested with 7DTD 1.2 (b27)](https://img.shields.io/badge/🧪%20Tested%20with-7DTD%201.2%20(b27)-blue.svg)](https://7daystodie.com/)
[![👍 Should Be Compatible with 7DTD 1.1 (b14)](https://img.shields.io/badge/👍%20Should%20Be%20Compatible%20with-7DTD%201.1%20(b14)-blue.svg)](https://7daystodie.com/)
[![👍 Should Be Compatible with 7DTD 1.0 (b333)](https://img.shields.io/badge/👍%20Should%20Be%20Compatible%20with-7DTD%201.0%20(b333)-blue.svg)](https://7daystodie.com/)

[![✅ Dedicated Servers Supported ServerSide](https://img.shields.io/badge/✅%20Dedicated%20Servers-Supported%20Serverside-blue.svg)](https://7daystodie.com/)
[![✅ Single Player and P2P Supported](https://img.shields.io/badge/✅%20Single%20Player%20and%20P2P-Supported-blue.svg)](https://7daystodie.com/)
[![📦 Automated Release](https://github.com/jonathan-robertson/days-remaining/actions/workflows/release.yml/badge.svg)](https://github.com/jonathan-robertson/days-remaining/actions/workflows/release.yml)

## Summary

7 Days to Die mod: update expiration date for vending machine rental from 'date of' to 'days until'.

> 💟 This mod has reached [End of Life](#support) and will not be directly updated to support 7 Days to Die 2.0 or beyond. Because this mod is [MIT-Licensed](LICENSE) and open-source, it is possible that other modders will keep this concept going in the future.
>
> Searching [NexusMods](https://nexusmods.com) or [7 Days to Die Mods](https://7daystodiemods.com) may lead to discovering other mods either built on top of or inspired by this mod.

### Support

💟 This mod has reached its end of life and is no longer supported or maintained by Kanaverum ([Jonathan Robertson](https://github.com/jonathan-robertson) // me). I am instead focused on my own game studio ([Calculating Chaos](https://calculatingchaos.com), if curious).

❤️ All of my public mods have always been open-source and are [MIT-Licensed](LICENSE); please feel free to take some or all of the code to reuse, modify, redistribute, and even rebrand however you like! The code in this project isn't perfect; as you update, add features, fix bugs, and otherwise improve upon my ideas, please make sure to give yourself credit for the work you do and publish your new version of the mod under your own name :smile: :tada:

## Features

1. Vending Machine Rental Expiration: replaces the existing 'date of' expiration value with a new value representing the number of remaining 'days until' expiration.
   - NOTE: this mod does not alter the actual days within a server (many components in the game rely on server/world time increasing); it simply updates how the days are reported for the Rentable Vending Machine Expiration Date.
2. Expiration Reminders: a quick buff-powered reminder will display on your screen to ensure you're aware of the days until your vending machine expires:
   - when logging in if at least one in-game day has passed since the player was last online
   - when a new day triggers (at midnight)

### Signup Screenshots

![signup buff badge](https://github.com/jonathan-robertson/days-remaining/raw/media/signup-notification-1.png)

![signup notification](https://github.com/jonathan-robertson/days-remaining/raw/media/signup-notification-2.png)

![signup info panel](https://github.com/jonathan-robertson/days-remaining/raw/media/signup-notification-3.png)

### Midnight / Logon Screenshots

![midnight or logon buff badge](https://github.com/jonathan-robertson/days-remaining/raw/media/midnight-or-logon-notification-1.png)

![midnight or logon notification](https://github.com/jonathan-robertson/days-remaining/raw/media/midnight-or-logon-notification-2.png)

### Expiration Screenshots

![expiration screenshot buff badge](https://github.com/jonathan-robertson/days-remaining/raw/media/expiration-notification-1.png)

![expiration screenshot notification](https://github.com/jonathan-robertson/days-remaining/raw/media/expiration-notification-2.png)

![expiration screenshot info panel](https://github.com/jonathan-robertson/days-remaining/raw/media/expiration-notification-3.png)

## Sister Project

This mod is designed to be used with another to complete your experience:

- [Days of the Week](https://github.com/jonathan-robertson/days-of-the-week)
  - Replace days count in UI with a day of the week
  - *be sure to use the `windows-days-remaining.xml` option in this mod to get the best experience*

## Compatibility

| Environment          | Compatible | Does EAC Need to be Disabled? | Who needs to install? |
| -------------------- | ---------- | ----------------------------- | --------------------- |
| Dedicated Server     | Yes        | no                            | only server           |
| Peer-to-Peer Hosting | Yes        | only for host                 | only host             |
| Single Player Game   | Yes        | Yes                           | self (of course)      |

## Acknowledgements

- Much thanks to `@Oozle` (Discord handle) for reporting a bug causing the vending machine rental to kick remote players out of the trade window any time items are added for sale
