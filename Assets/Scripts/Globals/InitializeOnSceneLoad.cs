using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeOnSceneLoad : MonoBehaviour
{
    [SerializeField] List<BaseCharacterObject> Defaults;
    BaseCharacterObject tempChar;
    void Start()
    {
        ResetStats();
        //Load Character Dolls
        foreach (BaseCharacterObject character in SceneData.instanceRef.CharactersInParty)
        {
            character.Status = "Default";
            character.statusTimer = 0;
            character.currentCooldown = 0;
            character.CurrentHP = character.MaxHP;
            GameObject doll = Instantiate(character.doll, character.dollSpawn.transform.position, character.dollSpawn.transform.rotation);
            HPBar hpBar = doll.GetComponentInChildren<HPBar>();
            AnimationController anim = doll.GetComponentInChildren<AnimationController>();
            hpBar.SetTarget(character);
            anim.SetTarget(character);
        }
    }
    public void ResetStats()
    {
        foreach (BaseCharacterObject character in SceneData.instanceRef.CharactersInParty)
        {
            tempChar = Defaults.Find(x => x.characterName == character.characterName);
            character.MaxHP = tempChar.MaxHP;
            character.specialCooldown = tempChar.specialCooldown;
            character.attackAmount = tempChar.attackAmount;
            character.beatModifier = tempChar.beatModifier;
            character.beatZoneSlow = tempChar.beatZoneSlow;
            character.Speed = tempChar.Speed;
            character.Attack = tempChar.Attack;
            character.Defense = tempChar.Defense;
        }
    }
}

