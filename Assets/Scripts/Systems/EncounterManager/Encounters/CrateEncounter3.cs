using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateEncounter3 : BaseEncounterFunctionality
{
     protected override void Effect1()
    {
        SceneData.instanceRef.mainText.text = "Breaking open the crates reveals an explosive rat! Everybody is blown away.";
        foreach (BaseCharacterObject character in SceneData.instanceRef.CharactersInParty)
        {
            character.CurrentHP -= 15;
            if (character.CurrentHP < 0)
            {
                character.CurrentHP = 0;
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
