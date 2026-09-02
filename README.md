# GitHub Activity CLI

A small command-line tool that pulls a GitHub user's recent activity and prints it straight to your terminal — no browser tabs, no dashboards, just a quick look at what someone's been up to on GitHub.

This is my take on the [GitHub User Activity](https://roadmap.sh/projects/github-user-activity) project from roadmap.sh, built with C# / .NET.

## Why I built this

I wanted a hands-on way to practice consuming a real-world REST API, parsing JSON without leaning on a bunch of helper libraries, and structuring a .NET console app properly instead of throwing everything into `Program.cs`. This project hits all three.

## What it does

Give it a GitHub username, and it will:

- Call the GitHub Events API for that user
- Parse the response
- Print a clean, human-readable summary of their recent activity (pushes, issues, stars, forks, and more)

If the username doesn't exist or the API call fails for any reason, it tells you what went wrong instead of crashing.

## Getting started

### Requirements

- [.NET SDK](https://dotnet.microsoft.com/download) (8.0 or later recommended)

### Run it

```bash
git clone <your-repo-url>
cd GitHubActivity
dotnet run <username>
```

Example:

```bash
dotnet run kamranahmedse
```

Sample output:

```
- Pushed 3 commits to kamranahmedse/developer-roadmap
- Opened a new issue in kamranahmedse/developer-roadmap
- Starred kamranahmedse/developer-roadmap
```

## Project structure

```
GitHubActivity/
│
├── Program.cs                 # Entry point — reads the username argument and kicks things off
├── Models/
│   └── GitHubEvent.cs         # Represents a single GitHub event
├── Services/
│   └── GitHubService.cs       # Handles the actual API call to GitHub
├── Helpers/
│   └── EventFormatter.cs      # Turns raw event data into readable terminal output
├── GitHubActivity.csproj
└── .gitignore
```

I kept the API-calling logic, the data model, and the output formatting in separate files on purpose — it made the code a lot easier to test and reason about than having it all in one place.

## A constraint that made this more interesting

Per the project requirements, I didn't use any external libraries for fetching or parsing the GitHub data — just what .NET ships with out of the box (`HttpClient` and `System.Text.Json`). It forced me to actually understand what's happening under the hood instead of letting a package do it for me.

## Notes / limitations

- Uses the unauthenticated GitHub API, so it's subject to GitHub's [rate limits](https://docs.github.com/en/rest/activity/events?apiVersion=2022-11-28) for anonymous requests.
- Only covers the event types returned by the `/users/<username>/events` endpoint.

## What I might add later

- Filtering activity by event type
- Caching responses to avoid hitting rate limits during testing
- A `--limit` flag to control how many events are shown

## Credits

Project idea from [roadmap.sh](https://roadmap.sh/projects/github-user-activity).
