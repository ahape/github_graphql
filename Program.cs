using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Octokit;

namespace GithubGraphql
{
    public class GraphQLRequest
    {
        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("variables")]
        public Dictionary<string, object> Variables { get; } = [];

        public string ToJsonText() => JsonConvert.SerializeObject(this);
    }

    class Program
    {
        internal const string PagedIssueQuery =
@"query ($repo_name: String!,  $start_cursor:String) {
  repository(owner: ""ahape"", name: $repo_name) {
    issues(last: 5, before: $start_cursor) {
      pageInfo {
        hasPreviousPage
        startCursor
      }
      nodes {
        title
        number
        createdAt
      }
    }
  }
}
";

        private static async Task Main()
        {
            var key = Environment.GetEnvironmentVariable("GitHubKey") ??
                throw new InvalidOperationException("Key not found");

            var client = new GitHubClient(new Octokit.ProductHeaderValue("IssueQueryDemo"))
            {
                Credentials = new Octokit.Credentials(key)
            };

            await foreach (var issue in RunPagedQueryAsync(client, PagedIssueQuery, "github_graphql"))
            {
                if (issue["labels"]["nodes"].Any(n => (string)n["name"] == "bug"))
                    Console.WriteLine(issue);
            }
        }

        private static async IAsyncEnumerable<JToken> RunPagedQueryAsync(
            GitHubClient client,
            string queryText,
            string repoName)
        {
            var issueAndPRQuery = new GraphQLRequest
            {
                Query = queryText
            };
            issueAndPRQuery.Variables["repo_name"] = repoName;

            bool hasMorePages = true;
            var (uri, respType) = (new Uri("https://api.github.com/graphql"), "application/json");

            for (var pagesReturned = 0; hasMorePages && pagesReturned < 10; pagesReturned++)
            {
                var response = await client.Connection.Post<string>(uri, issueAndPRQuery.ToJsonText(), respType, respType);
                var results = JObject.Parse(response.HttpResponse.Body.ToString());

                hasMorePages = (bool)PageInfo(results)["hasPreviousPage"];
                issueAndPRQuery.Variables["start_cursor"] = PageInfo(results)["startCursor"].ToString();

                foreach (JObject issue in Issues(results)["nodes"])
                    yield return issue;
            }

            JObject PageInfo(JObject result) => (JObject)Issues(result)["pageInfo"];
            JObject Issues(JObject result) => (JObject)result["data"]["repository"]["issues"];
        }
    }
}
