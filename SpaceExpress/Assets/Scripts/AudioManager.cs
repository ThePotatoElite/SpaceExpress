using UnityEngine;

public class AudioManager : MonoBehaviour
{
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
    public AudioClip effectNumberFour;
    public AudioClip effectNumberFive;
    
    private static AudioManager _instance;

    private void Awake()
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
    
    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        soundEffectsSource.PlayOneShot(clip);
    }
}