using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundS : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip_k1;
    public AudioClip clip_k2;
    public AudioClip clip_k3;
    public AudioClip clip_n1;
    public AudioClip clip_n2;
    public AudioClip clip_n3;
    bool isStartedPlay;
    private void Awake()
    {
        isStartedPlay = false;
    }
    public void ChooseTypeAndPlay(bool isKrit)
    {
        if (isKrit)
        {
            int r = Random.Range(0, 3);
            if (r == 0)
            {
                audioSource.clip = clip_k1;
            }
            else if (r == 1)
            {
                audioSource.clip = clip_k2;
            }
            else //r == 2
            {
                audioSource.clip = clip_k3;
            }
        }
        else
        {
            int r = Random.Range(0, 3);
            if (r == 0)
            {
                audioSource.clip = clip_n1;
            }
            else if (r == 1)
            {
                audioSource.clip = clip_n2;
            }
            else //r == 2
            {
                audioSource.clip = clip_n3;
            }
        }
        audioSource.Play();
        isStartedPlay = true;
    }
    private void Update()
    {
        if (!audioSource.isPlaying
            && isStartedPlay)
        {
            Destroy(gameObject);
        }
    }
}
