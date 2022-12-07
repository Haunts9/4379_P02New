using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainEncounter1 : BaseEncounterFunctionality
{
     protected override void Effect1()
    {
        SceneData.instanceRef.mainText.text = "Drinking the strange liquid reveals it to be raw healing potion! Despite sitting out for maybe decades it's still delicious!";
        foreach (BaseCharacterObject character in SceneData.instanceRef.CharactersInParty)
        {
            character.CurrentHP += 30;
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
