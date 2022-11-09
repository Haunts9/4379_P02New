using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InitializeOnSceneLoad : MonoBehaviour
{
    [SerializeField] Image fade;
    [SerializeField] List<BaseCharacterObject> Defaults;
    BaseCharacterObject tempChar;
    void Start()
    {
        SceneData.instanceRef.isDead = false;
        SceneData.instanceRef.isWin = false;
        Color32 black = new Color32(0, 0, 0, 255);
        Color32 clear = new Color32(0, 0, 0, 0);
        StartCoroutine(3f.TweengC((p) => fade.color = p, black, clear));
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
            SceneData.instanceRef.PartyDolls.Add(doll);
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

    private void FixedUpdate()
    {
        if (SceneData.instanceRef.isWin == true)
        {
            Debug.Log("win triggered" + SceneData.instanceRef.isWin);
            Color32 black = new Color32(0, 0, 0, 255);
            StartCoroutine(1.5f.TweengC((p) => fade.color = p, fade.color, black));
            StartCoroutine(wait(3));
        }

        if (SceneData.instanceRef.isDead == true)
        {
            Debug.Log("loss triggered" + SceneData.instanceRef.isDead);
            Color32 black = new Color32(0, 0, 0, 255);
            StartCoroutine(1.5f.TweengC((p) => fade.color = p, fade.color, black));
            StartCoroutine(wait(2));
        }
    }
    IEnumerator wait(int scene)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(scene);
    }
}

