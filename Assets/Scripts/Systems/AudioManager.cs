using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource main;
    AudioClip currentTrack;
    [SerializeField] float targetVolume = .25f;
    // Start is called before the first frame update
    void Awake()
    {
        main = GetComponent<AudioSource>();
    }
    public void StartTrack()
    {
        StartCoroutine(AudioHelper.StartFade(main, 2f, 0f));
        currentTrack = SceneData.instanceRef.CurrentLevel.levelMusic;
        Debug.Log("Now Playing: " + SceneData.instanceRef.CurrentLevel.levelMusic + " " + main.isPlaying);
        main.clip = currentTrack;
        main.Play();
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(AudioHelper.StartFade(main, 2f, targetVolume));
    }
}
