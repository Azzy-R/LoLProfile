using LoLProfile.Interfaces;
using LoLProfile.Models;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Text.Json;

namespace LoLProfile.Clients;

public class RiotClient(IOptions<RiotApiSettings> riotApiSettings) : IRiotClient
{
    public async Task<PuuidResponse> GetPuuID(string gameName, string tagLine)
    {
        var httpClient = new HttpClient();
        PuuidResponse? response = await httpClient.GetFromJsonAsync<PuuidResponse>($"https://americas.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{gameName}/{tagLine}?api_key={riotApiSettings.Value.ApiKey}");
        if (response is not null)
        {
            return response;
        }
        throw new Exception("matchIds was null when grabbing PUUID");
    }

    public async Task<SummonerInfo> GetPlayerInfo(string puuiID)
    {
        var httpClient = new HttpClient();
        SummonerInfo? response = await httpClient.GetFromJsonAsync<SummonerInfo>($"https://na1.api.riotgames.com/lol/summoner/v4/summoners/by-puuid/{puuiID}?api_key={riotApiSettings.Value.ApiKey}");
        if (response is not null)
        {
            return response;
        }
        throw new Exception("matchIds was null when Summoner Info");
    }
    public async Task<List<string>> GetMatchHistory(string puuiID)
    {
        var httpClient = new HttpClient();
        List<string>? matchIds = await httpClient.GetFromJsonAsync<List<string>>($"https://americas.api.riotgames.com/lol/match/v5/matches/by-puuid/{puuiID}/ids?start=0&count=20&api_key={riotApiSettings.Value.ApiKey}");
        if (matchIds is not null)
        {
            return matchIds;
        }
        else
            throw new Exception("matchIds was null when trying to grab recent match history");
    }

    public async Task<MatchData> GetMatchData(string matchId)
    {
        var httpClient = new HttpClient();
        MatchData? match = await httpClient.GetFromJsonAsync<MatchData>($"https://americas.api.riotgames.com/lol/match/v5/matches/{matchId}?api_key={riotApiSettings.Value.ApiKey}");
        if (match is not null)
        {
            return match;
        }
        throw new Exception("failed to grab match");
    }
}


