using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource sfxSource;
    public AudioSource bgmSource;

    [Header("Player")]
    public AudioClip jump;
    public AudioClip jumpHigh;
    public AudioClip hurt;

    [Header("Item")]
    public AudioClip coin;
    public AudioClip gem;

    [Header("Action")]
    public AudioClip bump;
    public AudioClip throwSound;

    [Header("UI")]
    public AudioClip select;

    private void Awake()
    {
        instance = this;
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayJump() => PlaySFX(jump);
    public void PlayJumpHigh() => PlaySFX(jumpHigh);
    public void PlayHurt() => PlaySFX(hurt);
    public void PlayCoin() => PlaySFX(coin);
    public void PlayGem() => PlaySFX(gem);
    public void PlayBump() => PlaySFX(bump);
    public void PlayThrow() => PlaySFX(throwSound);
    public void PlaySelect() => PlaySFX(select);
    public AudioClip bgm;

    void Start()
    {
        PlayBGM();
    }

    public void PlayBGM()
    {
        if (bgmSource == null || bgm == null) return;

        if (bgmSource.clip == bgm) return;

        bgmSource.clip = bgm;
        bgmSource.loop = true;
        bgmSource.volume = 0.5f;
        bgmSource.Play();
    }
}