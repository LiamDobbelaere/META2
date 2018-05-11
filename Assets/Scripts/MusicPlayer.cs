using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    private bool mustStop = false;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.isPlaying)
        {
            if (!mustStop)
            {
                audioSource.volume = Mathf.Lerp(audioSource.volume, 1f, 1f * Time.deltaTime);
            }
            else
            {
                audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, 1f * Time.deltaTime);
                if (audioSource.volume < 0.01f) audioSource.Stop();
            }
        }
    }

    public void Play(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.volume = 0f;
        audioSource.Play();
        mustStop = false;
    }

    public void Stop()
    {
        mustStop = true;
    }
}
