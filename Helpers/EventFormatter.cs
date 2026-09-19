using System.Text.Json;
using GitHubActivity.Models;

namespace GitHubActivity.Helpers;

public static class EventFormatter
{
  public static string Format(GitHubEvent gitHubEvent)
  {
    string repoName = gitHubEvent.Repo?.Name ?? "an unknown repository";
    JsonElement payload = gitHubEvent.Payload;

    return gitHubEvent.Type switch
    {
      "PushEvent" => FormatPushEvent(payload, repoName),
      "IssuesEvent" => FormatIssuesEvent(payload, repoName),
      "IssueCommentEvent" => $"Commented on an issue in {repoName}",
      "WatchEvent" => $"Starred {repoName}",
      "CreateEvent" => FormatCreateEvent(payload, repoName),
      "ForkEvent" => $"Forked {repoName}",
      "PullRequestEvent" => FormatPullRequestEvent(payload, repoName),
      "PullRequestReviewEvent" => $"Reviewed a pull request in {repoName}",
      "DeleteEvent" => FormatDeleteEvent(payload, repoName),
      "ReleaseEvent" => $"Published a new release in {repoName}",
      _ => $"Activity: {gitHubEvent.Type} on {repoName}"
    };
  }

}