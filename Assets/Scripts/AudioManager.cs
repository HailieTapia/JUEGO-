using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Bolas de Color")]
    public AudioClip yellowSound;
    public AudioClip redSound;
    public AudioClip blueSound;

    [Header("Power-ups")]
    public AudioClip paletteSound;
    public AudioClip rollerSound;
    public AudioClip brushSound;

    [Header("Ari SFX")]
    public AudioClip attackSound;
    public AudioClip jumpSound;
    public AudioClip hurtSound;
    public AudioClip dieSound;
    public AudioClip turretShootSound;

    [Header("Obstáculos")]
    public AudioClip obstacleSound;

    [Header("Música por nivel")]
    public AudioClip mainMenuMusic;
    public AudioClip nivel1Music;
    public AudioClip nivel2Music;
    public AudioClip nivel3Music;

    [Header("Victoria")]
    public AudioClip victorySound;

    private AudioSource sfxSource;
    private AudioSource musicSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SetupAudioSources();
            SceneManager.sceneLoaded += OnSceneLoaded;  //  LA LÍNEA MÁGICA
            PlayLevelMusic();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void SetupAudioSources()
    {
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.volume = 0.3f;

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.volume = 0.5f;
        musicSource.loop = true;
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null) sfxSource.PlayOneShot(clip);
    }

    // SE LLAMA AUTOMÁTICAMENTE CADA VEZ QUE CAMBIA DE NIVEL
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayLevelMusic();
    }

    public void PlayLevelMusic()
    {
        string scene = SceneManager.GetActiveScene().name;
        AudioClip clip = null;

        if (scene == "MainMenu") clip = mainMenuMusic;
        else if (scene == "Nivel1") clip = nivel1Music;
        else if (scene == "Nivel2") clip = nivel2Music;
        else if (scene == "Nivel3") clip = nivel3Music;

        if (clip != null && (musicSource.clip != clip || !musicSource.isPlaying))
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }
}