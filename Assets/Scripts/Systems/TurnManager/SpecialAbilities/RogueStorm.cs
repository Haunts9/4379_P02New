using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueStorm : MonoBehaviour
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
        SceneData.instanceRef.CurrentTurnAccessor.attackAmount = 10;
        SceneData.instanceRef.CurrentTurnAccessor.beatModifier = .25f;
        //Activate the thing
        SpecialTrigger();
    }
    public void SpecialTrigger()
    {
        SceneData.instanceRef.ToggleBeatZone(true);
        count = SceneData.instanceRef.CurrentTurnAccessor.attackAmount - 1;
        FinalTime = SceneData.instanceRef.AccessBeat.beatSpeed * SceneData.instanceRef.CurrentTurnAccessor.beatModifier;
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
            yield return new WaitForSeconds(FinalTime + (1f + (.5f * SceneData.instanceRef.CurrentTurnAccessor.attackAmount) * SceneData.instanceRef.CurrentTurnAccessor.beatModifier));
        }
        else
        {
            yield return new WaitForSeconds(FinalTime);
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
            SceneData.instanceRef.CurrentTurnAccessor.attackAmount = tempChar.attackAmount;
            SceneData.instanceRef.CurrentTurnAccessor.beatModifier = tempChar.beatModifier;
            SceneData.instanceRef.CurrentTurnAccessor.Attack = tempChar.Attack;
            Destroy(tempChar);
            Destroy(gameObject);
        }
    }
}
