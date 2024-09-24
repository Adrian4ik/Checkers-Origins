using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
	[SerializeField] PlayersConfig _config;



    #region Singleton
    private static PlayerSelector _instance;
    public static PlayerSelector Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<PlayerSelector>();

            return _instance;
        }
    }
    #endregion



    public void SetPlayers()
    {
        CheckersPlayer new_player = new CheckersPlayer();

        new_player.PlayerColor = Color.green;
        new_player.IsPlayer = true;

        _config.Players.Add(new_player);

        for (int i = 1; i < 4; i++)
        {
			new_player = new CheckersPlayer();

			new_player.PlayerColor = Color.white;
			new_player.IsPlayer = false;

            _config.Players.Add(new_player);
        }
	}
}
