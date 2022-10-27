using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClericSpecial : MonoBehaviour
{
    float FinalTime;
    int count;
    [SerializeField] GameObject specialBlip;
    BaseCharacterObject tempChar;
    int temp; 
    private void Start()
    {
        //Increase attack to heal more
        tempChar = Instantiate(SceneData.instanceRef.CurrentTurnAccessor);
        SceneData.instanceRef.CurrentTurnAccessor.Attack += SceneData.instanceRef.CurrentTurnAccessor.Attack;
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
            SceneData.instanceRef.CurrentTurnAccessor.Attack = tempChar.Attack;
            Destroy(tempChar);
            Destroy(gameObject);
        }
    }
}
