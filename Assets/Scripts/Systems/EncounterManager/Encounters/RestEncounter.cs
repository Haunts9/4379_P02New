using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestEncounter : BaseEncounterFunctionality
{
     protected override void Effect1()
    {
        SceneData.instanceRef.mainText.text = "After resting for a spell, the party recuperates enough to continue onwards...";
        foreach (BaseCharacterObject character in SceneData.instanceRef.CharactersInParty)
        {
            character.CurrentHP += (character.MaxHP/2);
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
        
        foreach (BaseCharacterObject character in SceneData.instanceRef.CharactersInParty)
        {
            int choice = Random.Range(1, 5);
            switch (choice)
            {
                case 1:
                    character.MaxHP += (character.MaxHP / 10);
                    character.CurrentHP += (character.MaxHP / 10);
                    if (character.CurrentHP > character.MaxHP)
                    {
                        character.CurrentHP = character.MaxHP;
                    }
                    break;
                case 2:
                    character.Speed++;
                    break;
                case 3:
                    character.Attack++;
                    break;
                case 4:
                    character.Defense++;
                    break;
                default:
                    break;
            }
        }
        SceneData.instanceRef.mainText.text = "Using the time to their advantage, the party manages to improve somewhat for the trials ahead...";
        SceneData.instanceRef.EncounterManager.GetComponent<EncounterManager>().swapProp();
        SceneData.instanceRef.EncounterManager.GetComponent<EncounterManager>().hideOptions();
    }
    protected override void Effect3()
    {

    }
}
