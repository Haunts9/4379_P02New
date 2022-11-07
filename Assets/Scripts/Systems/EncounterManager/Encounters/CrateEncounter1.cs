using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateEncounter1 : BaseEncounterFunctionality
{
     protected override void Effect1()
    {
        SceneData.instanceRef.mainText.text = "Breaking open the crates reveals a lavish feast! Everybody is able to heal a little.";
        foreach (BaseCharacterObject character in SceneData.instanceRef.CharactersInParty)
        {
            character.CurrentHP += 15;
            if (character.CurrentHP > character.MaxHP)
            {
                character.CurrentHP = character.MaxHP;
            }
        }
        SceneData.instanceRef.EncounterManager.GetComponent<EncounterManager>().swapProp();
        SceneData.instanceRef.EncounterManager.GetComponent<EncounterManager>().hideOptions();
    }
    protected override void Effect2()
    {

    }
    protected override void Effect3()
    {

    }
}
