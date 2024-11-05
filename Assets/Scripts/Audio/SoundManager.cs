using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource soundEffects;

    public AudioClip background;
    public AudioClip death;
    public AudioClip jump;
    public AudioClip hurt;
    public AudioClip spell;
    public AudioClip pickable;

    private void Awake()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        soundEffects.PlayOneShot(clip);
    }

}
