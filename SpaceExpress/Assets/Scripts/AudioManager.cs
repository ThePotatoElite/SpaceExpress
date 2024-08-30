using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("------- Audio Mixer -------")]
    [SerializeField] AudioMixer audioMixer;
    
    [Header("------- Audio Source -------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource soundEffectsSource;
    
    [Header("------- Audio Clip -------")]
    public AudioClip backgroundMusic;
    public AudioClip levelTimeOut;
    public AudioClip levelDone;
    public AudioClip start;
    public AudioClip restart;
    public AudioClip pickup;
    public AudioClip place;
    public AudioClip rotate;
    public AudioClip destroy;
    public AudioClip completeAll;
    public AudioClip getHit;
    public AudioClip explode;
    public AudioClip effectNumberOne;
    public AudioClip effectNumberTwo;
    public AudioClip effectNumberThree;
    
    private static AudioManager _instance;
    private bool _isMuted = false;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        soundEffectsSource.PlayOneShot(clip);
    }
    
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<AudioManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }
    
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        soundEffectsSource.volume = volume;
    }
    
    public void ToggleMute()
    {
        _isMuted = !_isMuted;
        audioMixer.SetFloat("MasterVolume", _isMuted ? -80f : 0f);
    }
}