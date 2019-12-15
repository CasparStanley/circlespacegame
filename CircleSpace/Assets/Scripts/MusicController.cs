using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioClip[] music;
    private AudioSource audSource;
    [SerializeField] private int currentSong;
    [SerializeField] private int amountOfSongsIndex;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        audSource = GetComponent<AudioSource>();

        StartMusic();
    }

    private void StartMusic ()
    {
        Debug.Log("<color=blue>Starting music</color>");
        currentSong = 0;
        audSource.clip = music[currentSong];
        audSource.Play();

        StartCoroutine(ChangeMusic());
    }

    private IEnumerator ChangeMusic ()
    {
        Debug.Log("<color=blue> Next Song: </color>" + currentSong + " - " + audSource.clip.name);
        yield return new WaitForSeconds(music[currentSong].length);

        currentSong++;

        if (currentSong > amountOfSongsIndex)
        {
            Debug.Log("<color=blue>Ran out of songs to play</color>");
            StartMusic();
        }

        else
            StartCoroutine(ChangeMusic());

        audSource.clip = music[currentSong];
        audSource.Play();
    }
}
