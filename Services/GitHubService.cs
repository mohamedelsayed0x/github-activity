using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using GitHubActivity.Models;

namespace GitHubActivity.Services;

/// <summary>
/// Responsible solely for communicating with the GitHub REST API.
/// Contains no console/presentation logic.
/// </summary>
public sealed class GitHubService : IDisposable
{
  private const string BaseUrl = "https://api.github.com";

  private readonly HttpClient _httpClient;
  private readonly JsonSerializerOptions _jsonOptions;

  public GitHubService()
  {
    _httpClient = new HttpClient();
    _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("GitHubActivity-CLI/1.0");
    _httpClient.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));

    _jsonOptions = new JsonSerializerOptions
    {
      PropertyNameCaseInsensitive = true
    };
  }
  public async Task<List<GitHubEvent>> GetUserEventsAsync(string username)
  {
    if (string.IsNullOrWhiteSpace(username))
    {
      throw new ArgumentException("Username cannot be empty.", nameof(username));
    }

    string url = $"{BaseUrl}/users/{Uri.EscapeDataString(username)}/events";

    HttpResponseMessage response;

    try
    {
      response = await _httpClient.GetAsync(url);
    }
    catch (TaskCanceledException ex)
    {
      throw new GitHubServiceException(
          "The request to GitHub timed out. Please check your connection and try again.", ex);
    }
    catch (HttpRequestException ex)
    {
      throw new GitHubServiceException(
          "Unable to reach GitHub. Please check your internet connection.", ex);
    }

    using (response)
    {
      if (response.StatusCode == HttpStatusCode.NotFound)
      {
        throw new GitHubServiceException($"GitHub user '{username}' was not found.");
      }

      if ((int)response.StatusCode >= 500)
      {
        throw new GitHubServiceException(
            "GitHub API is currently unavailable. Please try again later.");
      }

      if (!response.IsSuccessStatusCode)
      {
        throw new GitHubServiceException(
            $"GitHub API returned an unexpected status code: {(int)response.StatusCode} {response.ReasonPhrase}.");
      }

      string content = await response.Content.ReadAsStringAsync();

      try
      {
        List<GitHubEvent>? events = JsonSerializer.Deserialize<List<GitHubEvent>>(content, _jsonOptions);
        return events ?? new List<GitHubEvent>();
      }
      catch (JsonException ex)
      {
        throw new GitHubServiceException(
            "Received an unexpected response format from GitHub.", ex);
      }
    }
  }

  public void Dispose()
  {
    _httpClient.Dispose();
  }
}
public sealed class GitHubServiceException : Exception
{
  public GitHubServiceException(string message) : base(message)
  {
  }

  public GitHubServiceException(string message, Exception innerException) : base(message, innerException)
  {
  }
}