using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPoints : MonoBehaviour
{
    // Audio
    [SerializeField] private AudioClip pointsSoundEffect;
    private AudioSource audSource;
    // Start is called before the first frame update
    void Start()
    {
        // Audio
        audSource = GetComponent<AudioSource>();
        audSource.clip = pointsSoundEffect;
        audSource.Play();
    }
}
