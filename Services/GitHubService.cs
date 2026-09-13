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
}