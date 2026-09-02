namespace GitHubActivity.Models;

public class GitHubEvent
{
  public string Id { get; set; } = string.Empty;

  public string Type { get; set; } = string.Empty;

  public string RepositoryName { get; set; } = string.Empty;
}
