# Days Remaining

[![🧪 Tested with 7DTD 1.2 (b27)](https://img.shields.io/badge/🧪%20Tested%20with-7DTD%201.2%20(b27)-blue.svg)](https://7daystodie.com/)
[![👍 Should Be Compatible with 7DTD 1.1 (b14)](https://img.shields.io/badge/👍%20Should%20Be%20Compatible%20with-7DTD%201.1%20(b14)-blue.svg)](https://7daystodie.com/)
[![👍 Should Be Compatible with 7DTD 1.0 (b333)](https://img.shields.io/badge/👍%20Should%20Be%20Compatible%20with-7DTD%201.0%20(b333)-blue.svg)](https://7daystodie.com/)

[![✅ Dedicated Servers Supported ServerSide](https://img.shields.io/badge/✅%20Dedicated%20Servers-Supported%20Serverside-blue.svg)](https://7daystodie.com/)
[![✅ Single Player and P2P Supported](https://img.shields.io/badge/✅%20Single%20Player%20and%20P2P-Supported-blue.svg)](https://7daystodie.com/)
[![📦 Automated Release](https://github.com/jonathan-robertson/days-remaining/actions/workflows/release.yml/badge.svg)](https://github.com/jonathan-robertson/days-remaining/actions/workflows/release.yml)

## Summary

7 Days to Die mod: update expiration date for vending machine rental from 'date of' to 'days until'.

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
