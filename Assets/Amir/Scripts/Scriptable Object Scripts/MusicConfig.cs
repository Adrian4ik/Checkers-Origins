using UnityEngine;

[CreateAssetMenu(fileName = "MusicConfig", menuName = "Scriptable Objects/MusicConfig", order = 4)]
public class MusicConfig : ScriptableObject
{
    public AudioClip CurrentMusic { get; private set; }
    public float CurrentPosition { get; private set; }

    public void SetNewMusic(AudioClip clip)
    {
        CurrentMusic = clip;
        CurrentPosition = 0;
    }
    
    public void SetCurrentPosition(float position)
    {
        CurrentPosition = position;
    }
}
