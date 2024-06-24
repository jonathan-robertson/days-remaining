# Days Remaining

[![🧪 Tested On](https://img.shields.io/badge/🧪%20Tested%20On-1.0%20b308-blue.svg)](https://7daystodie.com/) [![📦 Automated Release](https://github.com/jonathan-robertson/days-remaining/actions/workflows/release.yml/badge.svg)](https://github.com/jonathan-robertson/days-remaining/actions/workflows/release.yml)

- [Days Remaining](#days-remaining)
  - [Summary](#summary)
  - [Features](#features)
  - [Sister Project](#sister-project)
  - [Compatibility](#compatibility)

## Summary

7 Days to Die mod: update expiration date for vending machine rental from 'date of' to 'days until'.

## Features

1. Vending Machine Rental Expiration: replaces the existing 'date of' expiration value with a new value representing the number of remaining 'days until' expiration.
   - NOTE: this mod does not alter the actual days within a server (many components in the game rely on server/world time increasing); it simply updates how the days are reported for the Rentable Vending Machine Expiration Date.
2. Expiration Reminders: a quick buff-powered reminder will display on your screen to ensure you're aware of the days until your vending machine expires:
   - when logging in
   - when a new day triggers (at midnight)

## Sister Project

This mod is designed to be used with another to complete your experience:

- [Days of the Week](https://github.com/jonathan-robertson/days-of-the-week)
  - Replace days count in UI with a day of the week
  - *be sure to use the `windows-days-remaining.xml` option in this mod to get the best experience*

## Compatibility

Environment | Compatible | Does EAC Need to be Disabled? | Who needs to install?
--- | --- | --- | ---
Dedicated Server | Yes | no | only server
Peer-to-Peer Hosting | Untested | N/A | N/A
Single Player Game | Untested | N/A | N/A
