using System.Text.Json;
using System.Text.Json.Serialization;

namespace GitHubActivity.Models;

/// <summary>
/// Represents a single public event returned by the GitHub Events API.
/// The payload varies by event type, so it is kept as a raw JsonElement
/// and interpreted later by EventFormatter.
/// </summary>
public sealed class GitHubEvent
{
  [JsonPropertyName("id")]
  public string Id { get; init; } = string.Empty;

  [JsonPropertyName("type")]
  public string Type { get; init; } = string.Empty;

  [JsonPropertyName("actor")]
  public GitHubActor? Actor { get; init; }

  [JsonPropertyName("repo")]
  public GitHubRepo? Repo { get; init; }

  [JsonPropertyName("payload")]
  public JsonElement Payload { get; init; }

  [JsonPropertyName("created_at")]
  public DateTimeOffset CreatedAt { get; init; }
}

public sealed class GitHubActor
{
  [JsonPropertyName("id")]
  public long Id { get; init; }

  [JsonPropertyName("login")]
  public string Login { get; init; } = string.Empty;
}

public sealed class GitHubRepo
{
  [JsonPropertyName("id")]
  public long Id { get; init; }

  [JsonPropertyName("name")]
  public string Name { get; init; } = string.Empty;
}