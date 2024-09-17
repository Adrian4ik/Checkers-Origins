using UnityEngine;

public class CheckerSpawner : MonoBehaviour
{
    [SerializeField] PlayersConfig _playersConfig;
    [SerializeField] GameObject _checkerPrefab;
    [SerializeField] float _radius;



    private void Awake()
    {
        WallConstructor wc = gameObject.GetComponent<WallConstructor>();


        /*foreach (CheckersPlayer player in _playersConfig.Players)
        {
            GameObject checker = Instantiate(_checkerPrefab, transform);

            //MarkAppearance appearance = checker.GetComponent<MarkAppearance>();
            //appearance.SetColor(player.PlayerColor);

            if (player.IsPlayer)
                checker.AddComponent<PointerSystemController>();
        }*/

        // start turn system
    }
}
