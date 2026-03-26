using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource sfxSource;
    public AudioSource bgmSource;

    [Header("Player SFX")]
    public AudioClip jump;
    public AudioClip jumpHigh;
    public AudioClip hurt;
    public AudioClip death;

    [Header("Item SFX")]
    public AudioClip coin;

    [Header("UI SFX")]
    public AudioClip select;

    [Header("BGM")]
    public AudioClip bgm;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("AudioManager khởi tạo thành công!");
    }
    private void Start()
    {
        PlayBGM();
    }

    public void PlaySFX(AudioClip clip)
    {
        Debug.Log($"PlaySFX | clip={clip?.name} | sfxSource={sfxSource}");
        if (clip == null || sfxSource == null) return;
        sfxSource.PlayOneShot(clip);
    }

    public void PlayJump() => PlaySFX(jump);
    public void PlayJumpHigh() => PlaySFX(jumpHigh);
    public void PlayHurt() => PlaySFX(hurt);
    public void PlayDeath() => PlaySFX(death);
    public void PlayCoin() => PlaySFX(coin);
    public void PlaySelect() => PlaySFX(select);

    public void PlayBGM()
    {
        Debug.Log($"PlayBGM | bgmSource={bgmSource} | bgm={bgm?.name}");
        if (bgmSource == null || bgm == null) return;
        if (bgmSource.clip == bgm && bgmSource.isPlaying) return;
        bgmSource.clip = bgm;
        bgmSource.loop = true;
        bgmSource.volume = 0.5f;
        bgmSource.Play();
    }
}
