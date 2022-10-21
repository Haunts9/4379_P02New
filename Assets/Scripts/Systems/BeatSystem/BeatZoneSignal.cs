using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatZoneSignal : MonoBehaviour
{
    bool coolDown = false;
    bool active = false;
    private Color defaultColor;
    [SerializeField] ParticleSystem triggerParticle;
    [SerializeField] AudioClip triggerSound;
    [SerializeField] float soundVolume;
    private void Awake()
    {
        Debug.Log("BeatZone Initialized");
        defaultColor = gameObject.GetComponent<Renderer>().material.color;
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown("space") && SceneData.instanceRef.BeatZoneUsableAccessor == true && coolDown == false)
        {
            Signal();
        }

        if (active == true)
        {
            SceneData.instanceRef.BeatZoneActiveCheck = true;
        }
        else
        {
            SceneData.instanceRef.BeatZoneActiveCheck = false;
        }

        if (SceneData.instanceRef.BeatZoneUsableAccessor == false)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (active == true)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = defaultColor;
        }
    }

    void Signal()
    {
        StartCoroutine(Tick());
        StartCoroutine(CoolDown());
    }

    IEnumerator CoolDown()
    {
        coolDown = true;
        yield return new WaitForSeconds(SceneData.instanceRef.CurrentTurnAccessor.beatZoneSlow);
        coolDown = false;
    }
    IEnumerator Tick()
    {
        active = true;
        Feedback();
        yield return new WaitForSeconds(.01f);
        active = false;
    }
      void Feedback()
    {
        //particles
        if (triggerParticle != null)
        {
            triggerParticle = Instantiate(triggerParticle, transform.position, Quaternion.identity);
        }
        //audio
        if (triggerSound != null)
        {
            AudioHelper.PlayClip2D(triggerSound, soundVolume);
        }
    }
}
