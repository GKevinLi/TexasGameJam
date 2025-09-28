using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;

[Header("------Audio CLip--------")]
    public AudioClip background;

    private void Start(){
        musicSource.clip = background;
        musicSource.Play();
    }
}
