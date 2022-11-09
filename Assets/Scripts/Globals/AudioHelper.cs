using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHelper
{
    public static AudioSource PlayClip2D(AudioClip clip, float volume)
    {
        //create
        GameObject audioObject = new GameObject("Audio2D");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        //configure
        audioSource.clip = clip;
        audioSource.volume = volume;
        //activate
        audioSource.Play();
        Object.Destroy(audioObject, clip.length);
        //return
        return audioSource;
    }
    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
