using UnityEngine;
using GD.MinMaxSlider;

public class ClipRandomizer : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] bool randomizerPitch = true;
    [MinMaxSlider(0f, 3f)]
    [SerializeField] Vector2 pitch;


    AudioSource audioSource;
    
    private void Awake() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (audioSource.playOnAwake)
        {
            PlayRandomClip();
        }
    }

    public void PlayRandomClip()
    {
        float randomPitch = Random.Range(pitch.x, pitch.y);
        int clipIndex = Random.Range(0, clips.Length);
        
        audioSource.pitch = randomPitch;
        if(randomizerPitch) audioSource.PlayOneShot(clips[clipIndex]); 
        
        
    }
}
