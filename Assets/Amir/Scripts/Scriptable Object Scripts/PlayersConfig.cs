using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayersConfig", menuName = "Scriptable Objects/PlayersConfig", order = 5)]
public class PlayersConfig : ScriptableObject
{
    public List<CheckersPlayer> Players { get; private set; }

    public void AddPlayer(Color player_color, bool is_player)
    {
        CheckersPlayer player = new CheckersPlayer();
        
        player.PlayerColor = player_color;
        player.IsPlayer = is_player;

        Players.Add(player);
    }

    public void RemovePlayers() { Players.Clear(); }
}
