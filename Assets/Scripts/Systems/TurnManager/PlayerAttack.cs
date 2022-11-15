using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    float FinalTime;
    int count;

    public void AttackTrigger()
    {
        Debug.Log("Attacker: " + SceneData.instanceRef.CurrentTurnAccessor);
        SceneData.instanceRef.ToggleBeatZone(true);
        count = SceneData.instanceRef.CurrentTurnAccessor.attackAmount-1;
        FinalTime = SceneData.instanceRef.AccessBeat.beatSpeed / SceneData.instanceRef.CurrentTurnAccessor.beatModifier;
        SceneData.instanceRef.SetBusy = true;
        StartCoroutine(SendBlip());
    }
    IEnumerator SendBlip()
    {
        //Make Blip;
        Instantiate(SceneData.instanceRef.AttackBlip, SceneData.instanceRef.BlipSpawner.transform.position, SceneData.instanceRef.BlipSpawner.transform.rotation);
        //Time for blips to move
        if (count == 0)
        {
            yield return new WaitForSeconds((SceneData.instanceRef.CurrentTurnAccessor.attackAmount / SceneData.instanceRef.CurrentTurnAccessor.beatModifier) + .5f);
        }
        else
        {
            yield return new WaitForSeconds(FinalTime*.5f);
        }
        //stop  after all blips have come and gone
        if(count > 0)
        {
            count--;
            StartCoroutine(SendBlip());
        }
        else
        {
            Debug.Log("Attack Stopped");
            SceneData.instanceRef.SetBusy = false;
            SceneData.instanceRef.ToggleBeatZone(false);
        }
    }
}
