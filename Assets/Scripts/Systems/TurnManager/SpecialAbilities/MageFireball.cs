using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageFireball : MonoBehaviour
{
    float FinalTime;
    int count;
    [SerializeField] GameObject specialBlip;
    BaseCharacterObject tempChar;
    private void Start()
    {
        //temp = Instantiate(SceneData.instanceRef.CurrentTurnAccessor);
        //Increase number of daggers
        tempChar = Instantiate(SceneData.instanceRef.CurrentTurnAccessor);
        SceneData.instanceRef.CurrentTurnAccessor.Attack *= 2;
        SceneData.instanceRef.CurrentTurnAccessor.attackAmount = 1;
        //Activate the thing
        SpecialTrigger();
    }
    public void SpecialTrigger()
    {
        SceneData.instanceRef.ToggleBeatZone(true);
        count = SceneData.instanceRef.CurrentTurnAccessor.attackAmount - 1;
        FinalTime = SceneData.instanceRef.AccessBeat.beatSpeed / SceneData.instanceRef.CurrentTurnAccessor.beatModifier;
        SceneData.instanceRef.SetBusy = true;
        StartCoroutine(SendBlip());
    }
    IEnumerator SendBlip()
    {
        //Make Blip;
        Instantiate(specialBlip, SceneData.instanceRef.BlipSpawner.transform.position, SceneData.instanceRef.BlipSpawner.transform.rotation);
        //Time for blips to move
        if (count == 0)
        {
            yield return new WaitForSeconds((SceneData.instanceRef.CurrentTurnAccessor.attackAmount / SceneData.instanceRef.CurrentTurnAccessor.beatModifier) + 1f);
        }
        else
        {
            yield return new WaitForSeconds(FinalTime * .5f);
        }
        //stop  after all blips have come and gone
        if (count > 0)
        {
            count--;
            StartCoroutine(SendBlip());
        }
        else
        {
            SceneData.instanceRef.SetBusy = false;
            SceneData.instanceRef.ToggleBeatZone(false);
            //reset
            SceneData.instanceRef.CurrentTurnAccessor.Attack = tempChar.Attack;
            SceneData.instanceRef.CurrentTurnAccessor.attackAmount = tempChar.attackAmount;
            Destroy(tempChar);
            Destroy(gameObject);
        }
    }
}
