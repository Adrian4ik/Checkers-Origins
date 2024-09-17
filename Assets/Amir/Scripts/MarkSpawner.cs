using UnityEngine;

public class MarkSpawner : MonoBehaviour
{
	[SerializeField] PlayersConfig _playersConfig;
    [SerializeField] GameObject _markerPrefab;
    [SerializeField] MarkAppearance _markerScriptPrefab;
    [SerializeField] float _markerGap;

    private void Start()
    {
        byte i = 0;

        if (_playersConfig.Players == null)
            return;

        transform.DestroyChildren();

        foreach (CheckersPlayer player in _playersConfig.Players)
        {
            MarkAppearance marker = Instantiate(_markerScriptPrefab, transform);
            marker.SetColor(player.PlayerColor);

            if (player.IsPlayer)
                marker.SetText("Pl");
            else
                marker.SetText("AI " + ++i);
        }
    }
}
