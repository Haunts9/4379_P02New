using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BlipScript : MonoBehaviour
{
    float blipSpeed;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip missSound;
    [SerializeField] float soundVolume = .5f;
    protected abstract void Hit();
    protected abstract void Miss();
    private void Awake()
    {
        blipSpeed = SceneData.instanceRef.blipSpeed * SceneData.instanceRef.CurrentTurnAccessor.beatModifier;
    }
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * blipSpeed * Time.deltaTime);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "BeatZone" && SceneData.instanceRef.BeatZoneActiveCheck == true)
        {
            Hit();
            feedback(hitSound);
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "BeatZone")
        {
            feedback(missSound);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BlipDeathZone")
        {
            Miss();
            Destroy(gameObject);
        }
    }
    void feedback(AudioClip clipToPlay)
    {
        if (clipToPlay != null)
        {
            AudioHelper.PlayClip2D(clipToPlay, soundVolume);
        }
    }
}
