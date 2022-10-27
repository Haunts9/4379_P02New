using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeOnSceneLoad : MonoBehaviour
{

    void Start()
    {
        //Load Character Dolls
        foreach (BaseCharacterObject character in SceneData.instanceRef.CharactersInParty)
        {
            character.currentCooldown = 0;
            character.CurrentHP = character.MaxHP;
           Instantiate(character.doll, character.dollSpawn.transform.position, character.dollSpawn.transform.rotation);
        }
    }

}
