## Task

Reveal all of the issues in this repository that have the label "bug", using this command line program.

## Setup

1. Download + install [dotnet cli](https://dotnet.microsoft.com/en-us/download) if it's not already. Ensure `dotnet` is in your $PATH
1. Clone this repo into the folder `github_graphql`
1. `cd` into `github_graphql/`
1. Create a Github PAT that is given the following permissions: `public_repo` and `repo:status`
1. Assign the value of the new PAT to the following environment variable: `GitHubKey=YOUR_ACCESS_TOKEN`
1. Call `dotnet run`

