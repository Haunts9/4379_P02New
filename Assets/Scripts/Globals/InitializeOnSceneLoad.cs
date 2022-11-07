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
           GameObject doll = Instantiate(character.doll, character.dollSpawn.transform.position, character.dollSpawn.transform.rotation);
            HPBar hpBar = doll.GetComponentInChildren<HPBar>();
            AnimationController anim = doll.GetComponentInChildren<AnimationController>();
            hpBar.SetTarget(character);
            anim.SetTarget(character);
        }
    }

}
