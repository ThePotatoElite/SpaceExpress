using UnityEngine;

public class MuteAudio : MonoBehaviour
{
    [SerializeField] AudioSource trainAudioSource;

    public void MuteHandler(bool mute)
    {
        if (trainAudioSource != null)
        {
            trainAudioSource.mute = mute;
        }
    }
}