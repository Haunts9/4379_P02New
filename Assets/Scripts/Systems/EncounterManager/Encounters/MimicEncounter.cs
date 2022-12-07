using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicEncounter : BaseEncounterFunctionality
{
    [SerializeField] BaseBattleObject encounter;
     protected override void Effect1()
    {
        
        SceneData.instanceRef.mainText.text = "Opening the chest reveals a trap most sinister!";
        SceneData.instanceRef.EncounterManager.GetComponent<EncounterManager>().swapProp();
        SceneData.instanceRef.EncounterManager.GetComponent<EncounterManager>().PsuedoCleanUp();
        SceneData.instanceRef.CurrentBattle = encounter;
        SceneData.instanceRef.TurnManager.GetComponent<TurnManager>().enabled = false;
        SceneData.instanceRef.TurnManager.GetComponent<TurnManager>().enabled = true;
        SceneData.instanceRef.TurnManager.GetComponent<TurnManager>().InitializeTurnOrder();
    }
    protected override void Effect2()
    {

    }
    protected override void Effect3()
    {

    }
}
