using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Questao2;

public static class Program
{
    public static void Main()
    {
        var teamName = "Paris Saint-Germain";
        var year = 2013;
        var totalGoals = GetTotalScoredGoals(teamName, year).Result;
        Console.WriteLine("Team " + teamName + " scored " + totalGoals + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = GetTotalScoredGoals(teamName, year).Result;
        Console.WriteLine("Team " + teamName + " scored " + totalGoals + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    private static async Task<int> GetTotalScoredGoals(string team, int year)
    {
        return await GetTotalScoredGoals(team, year, GamePlace.Home) +
               await GetTotalScoredGoals(team, year, GamePlace.Away);
    }

    private static async Task<int> GetTotalScoredGoals(string team, int year, GamePlace gamePlace)
    {
        using var httpClient = new HttpClient();
        const string baseUrl = "https://jsonmock.hackerrank.com/api/football_matches";
        var homeOrAwayTeam = gamePlace.Equals(GamePlace.Home) ? "team1" : "team2";
        var goals = 0;
        var currentPage = 0;
        FootballMatches? matches;
        do
        {
            currentPage++;
            var url = $"{baseUrl}?year={year}&{homeOrAwayTeam}={team}&page={currentPage}";
            var matchesResponse = await (await httpClient.GetAsync(url)).Content.ReadAsStringAsync();
            matches = JsonConvert.DeserializeObject<FootballMatches>(matchesResponse, GetJsonSerializerSettings());
            goals += matches?.Data?.Sum(x => gamePlace.Equals(GamePlace.Home) ? 
                int.Parse(x.Team1goals) : 
                int.Parse(x.Team2goals)) ?? 0;
        } while (currentPage < matches?.TotalPages);

        return goals;
    }

    private static JsonSerializerSettings GetJsonSerializerSettings()
    {
        return new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };
    }
}