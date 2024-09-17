using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class BGMManager : MonoBehaviour
{
    [SerializeField] AudioClip _currentSceneMusic;
    [SerializeField] MusicConfig _currentMusicSO;

    private AudioSource _audioSource;

    private void Awake()
    {
        Utils.LoadComponent(gameObject, out _audioSource);

        if (_currentMusicSO.CurrentMusic != _currentSceneMusic)
            _currentMusicSO.SetNewMusic(_currentSceneMusic);

        _audioSource.clip = _currentMusicSO.CurrentMusic;
        _audioSource.Play();
        _audioSource.time = _currentMusicSO.CurrentPosition;
    }

    private void OnDisable()
    {
        _currentMusicSO.SetCurrentPosition(_audioSource.time);
    }

    private void OnApplicationQuit()
    {
        _currentMusicSO.SetNewMusic(null);
    }
}
