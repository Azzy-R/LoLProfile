using LoLProfile.Models;

namespace LoLProfile.Interfaces;

public interface IRiotClient
{
    Task<PuuidResponse> GetPuuID(string gameName, string tagLine);
    Task<SummonerInfo> GetPlayerInfo(string PuuiID);
}

