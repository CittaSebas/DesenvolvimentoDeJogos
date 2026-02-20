using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioSource Sfx;
    public AudioClip SfxClip;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Sfx.PlayOneShot(SfxClip);
    }
}