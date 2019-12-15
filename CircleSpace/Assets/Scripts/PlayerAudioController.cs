using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip[] SoundEffects;
    private AudioSource audSource;

    private void Start()
    {
        audSource = GetComponent<AudioSource>();
        audSource.loop = true;
        audSource.Play();
    }

    private void Update()
    {
        if (Time.timeScale > 0) // If it is 0 then we are paused
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                if (audSource.clip == SoundEffects[0])
                {
                    audSource.clip = SoundEffects[1];
                    audSource.Play();
                }
            }

            else if (Input.GetAxisRaw("Horizontal") == 0 || Input.GetAxisRaw("Vertical") == 0)
            {
                if (audSource.clip == SoundEffects[1])
                {
                    audSource.clip = SoundEffects[0];
                    audSource.Play();
                }
            }
        }

        else
        {
            audSource.Stop();
        }
    }
}