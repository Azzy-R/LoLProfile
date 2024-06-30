using LoLProfile.Interfaces;
using LoLProfile.Models;
using Microsoft.Extensions.Options;
using System.Diagnostics;

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
        throw new Exception("response was null when grabbing PUUID");
    }

    public async Task<SummonerInfo> GetPlayerInfo(string puuiID)
    {
        var httpClient = new HttpClient();
        SummonerInfo? response = await httpClient.GetFromJsonAsync<SummonerInfo>($"https://na1.api.riotgames.com/lol/summoner/v4/summoners/by-puuid/{puuiID}?api_key={riotApiSettings.Value.ApiKey}");
        if (response is not null)
        {
            return response;
        }
        throw new Exception("response was null when Summoner Info");
    }

}

