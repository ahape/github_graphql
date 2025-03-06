## Goal

This program aims to _programmatically_ query [this repository](https://github.com/ahape/github_graphql/issues) for **issues** containing the **label** "bug".

This is acheived by using Github's [GraphQL API](https://docs.github.com/en/graphql/guides).

When **working**, it should log a JSON result of **one** issue.

## Prerequisites

- A Github account
- An IDE such as Visual Studio (recommended) or Visual Studio Code
- The latest .NET SDK/runtime (this comes with Visual Studio)

## Setup

1. Clone this repo
1. Create a [Github PAT](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/managing-your-personal-access-tokens#creating-a-personal-access-token-classic) that is given the following permissions: `public_repo` and `repo:status`. A classic PAT is more straightforward and preferred
1. Assign the value of the new PAT to the following environment variable: `GitHubKey=YOUR_ACCESS_TOKEN`. This environment variable must be accessible by the program

<details>
<summary>
4a. Loading and running in Visual Studio (recommended)
</summary>

- If you have **not** already downloaded and installed Visual Studio, you can download the latest (free) community edition [here](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
- In VS, go to `File` > `Open` > `Project/Solution...` and select the `.csproj` file from your cloned repo
- To load all the project's dependencies and generate/run the program executable, just hit `F5` (Run)

</details>

<details>
<summary>
4b. Loading and running in Visual Studio Code
</summary>

- If you have **not** already downloaded and installed Visual Studio Code, you can do so [here](https://code.visualstudio.com/download)
- If you have **not** already installed the C# extension (includes basic .NET SDK for running the program), you can do so [here](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
- In VS Code, go to `File` > `Open Folder...` and open the folder containing this repository
- To load all the project's dependencies and generate/run the program executable, just hit `F5` (Run)

</details>
