# Inventory System

A console-based inventory and shop system in C# (.NET 10). This repository is our
team's debugged version of the Start IT "Bug Hunt" exercise: the original project
was deliberately seeded with bugs, and our job was to find, explain, and fix them.

## Stack

C# / .NET 10, console application.

## Run

```
dotnet run
```

Menu options: show shop, buy item, sell item, show inventory, show gold, search,
show history, exit.

## Bugs and improvements

20 logical bugs were found and fixed. After feedback from the teacher, 8 further
quality and architecture improvements were made — separation of concerns (all console
I/O lives in a single `View` class), encapsulation, and a transaction history log.
The full breakdown — with file, location, original error, and fix — is in
[BUGFIXES.md](BUGFIXES.md).

## Credits

Original exercise from the Start IT course. Debugged as a team.
