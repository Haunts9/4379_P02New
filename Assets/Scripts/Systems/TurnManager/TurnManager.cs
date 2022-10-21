using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public BaseBattleObject Battle;
    GameObject SpecialText;
    [SerializeField] GameObject IconSpawn;
    List<BaseCharacterObject> TurnOrder = new List<BaseCharacterObject>();
    int AmountOfTurns = 0;
    int CurrentTurn = 0;
    bool midTurn = true;

    #region Player Turn
    [Header("Player Turn Variables")]
    [SerializeField] GameObject PlayerField;
    #endregion
    //Read from party and battle lists to create turn order and rotate
    private void FixedUpdate()
    {
        selector();
        if (midTurn == false && SceneData.instanceRef.SetBusy == false)
        {
            Debug.Log("next turn");
            NextTurn();
        }
  
    }
    public void InitializeTurnOrder()
    {
        //Init List
        foreach (BaseCharacterObject enemy in Battle.Enemies)
        {
            enemy.CurrentHP = enemy.MaxHP;
            TurnOrder.Add(enemy);
        }
        foreach (BaseCharacterObject character in SceneData.instanceRef.CharactersInParty)
        {
            TurnOrder.Add(character);
        }
        //End List Init
        //Sort List by Speed
        TurnOrder.Sort(SortBySpeed);
        AmountOfTurns = TurnOrder.Count;
        for (int k = 0; k < AmountOfTurns; k++)
        {
            TurnOrder[k].TurnPosition = k;
        }
        FirstTurn();
    }
    void FirstTurn()
    {
        SceneData.instanceRef.CurrentTurnAccessor = TurnOrder[CurrentTurn];
        SetIcon();
        midTurn = true;
        if (TurnOrder[CurrentTurn].isPlayer == true)
        {
            PlayerTurn();
        }
        else
        {
            EnemyTurn();
        }
    }
    public void NextTurn()
    {
        midTurn = true;
        //Debug.Log("Turn " + CurrentTurn+1 + ": " + TurnOrder[CurrentTurn]);
        if (CurrentTurn < AmountOfTurns-1)
        {
            CurrentTurn++;
        }
        else
        {
            CurrentTurn = 0;
        }
        SceneData.instanceRef.CurrentTurnAccessor = TurnOrder[CurrentTurn];
        SetIcon();
        if (TurnOrder[CurrentTurn].isPlayer == true)
        {
            PlayerTurn();
        }
        else
        {
            EnemyTurn();
        }
    }
    #region Player
    public void PlayerTurn()
    {
        SceneData.instanceRef.CurrentTurnAccessor.isDefending = false;
        if (SceneData.instanceRef.CurrentTurnAccessor.selectedTarget == null)
        {
            SceneData.instanceRef.CurrentTurnAccessor.selectedTarget = TurnOrder.Find(x => x.isPlayer == false);
        }
        Debug.Log("Currently selected " + SceneData.instanceRef.CurrentTurnAccessor.selectedTarget.characterName + " at turn position " + SceneData.instanceRef.CurrentTurnAccessor.selectedTarget.TurnPosition);
        PlayerField.SetActive(true);
        SpecialText = GameObject.FindGameObjectWithTag("SpecialButtonText");
        //change Special text
        TextMeshProUGUI temp = SpecialText.GetComponent<TextMeshProUGUI>();
        if (SceneData.instanceRef.CurrentTurnAccessor.currentCooldown != 0)
        {
            SceneData.instanceRef.CurrentTurnAccessor.currentCooldown--;
        }
        if (SceneData.instanceRef.CurrentTurnAccessor.currentCooldown <= 0)
        {
            temp.SetText(SceneData.instanceRef.CurrentTurnAccessor.specialAbilityName);
        }
        else
        {
            temp.text = ("Cooldown: (" + (SceneData.instanceRef.CurrentTurnAccessor.currentCooldown) + ")");
        }

    }
    void selector()
    {
        if(PlayerField.activeSelf == true)
        {
            if (Input.GetKeyDown("left"))
            {
                if (SceneData.instanceRef.CurrentTurnAccessor.selectedTarget.TurnPosition > 0)
                {
                    SceneData.instanceRef.CurrentTurnAccessor.selectedTarget = TurnOrder[SceneData.instanceRef.CurrentTurnAccessor.selectedTarget.TurnPosition - 1];
                }
                else
                {
                    SceneData.instanceRef.CurrentTurnAccessor.selectedTarget = TurnOrder[AmountOfTurns-1];
                }
                Debug.Log("Selected " + SceneData.instanceRef.CurrentTurnAccessor.selectedTarget.characterName + " at turn position " + SceneData.instanceRef.CurrentTurnAccessor.selectedTarget.TurnPosition);
            }
            if (Input.GetKeyDown("right"))
            {
                if (SceneData.instanceRef.CurrentTurnAccessor.selectedTarget.TurnPosition < AmountOfTurns-1)
                {
                    SceneData.instanceRef.CurrentTurnAccessor.selectedTarget = TurnOrder[SceneData.instanceRef.CurrentTurnAccessor.selectedTarget.TurnPosition + 1];
                }
                else
                {
                    SceneData.instanceRef.CurrentTurnAccessor.selectedTarget = TurnOrder[0];
                }
                Debug.Log("Selected " + SceneData.instanceRef.CurrentTurnAccessor.selectedTarget.characterName + " at turn position " + SceneData.instanceRef.CurrentTurnAccessor.selectedTarget.TurnPosition);
            }
        }
    }
    public void AttackButtonPress()
    {
        Debug.Log(TurnOrder[CurrentTurn].characterName + " Attacks " + SceneData.instanceRef.CurrentTurnAccessor.selectedTarget);
        PlayerField.SetActive(false);
        PlayerAttack attack = gameObject.GetComponent<PlayerAttack>();
        attack.AttackTrigger();
        StartCoroutine(WaitABitBeforeTurn());
    }
    public void DefendButtonPress()
    {
        Debug.Log(TurnOrder[CurrentTurn].characterName + " Defends!");
        PlayerField.SetActive(false);
        SceneData.instanceRef.CurrentTurnAccessor.isDefending = true;
        StartCoroutine(WaitABitBeforeTurn());
    }
    public void SpecialButtonPress()
    {

        //implement here
        if (SceneData.instanceRef.CurrentTurnAccessor.currentCooldown == 0)
        {
            Debug.Log(TurnOrder[CurrentTurn].characterName + " Specials!");
            PlayerField.SetActive(false);
            Instantiate(SceneData.instanceRef.CurrentTurnAccessor.specialAbility);
            StartCoroutine(WaitABitBeforeTurn());
            SceneData.instanceRef.CurrentTurnAccessor.currentCooldown = SceneData.instanceRef.CurrentTurnAccessor.specialCooldown;
        }
        else
        {
            Debug.Log(TurnOrder[CurrentTurn].characterName + " cant do that cuz on cooldown!");
        }

        //implement end

    }
    public void SkipButtonPress()
    {
        Debug.Log(TurnOrder[CurrentTurn].characterName + " Passes");
        PlayerField.SetActive(false);
        StartCoroutine(WaitABitBeforeTurn());
    }
    #endregion
    public void EnemyTurn()
    {
        StartCoroutine(WaitABitBeforeTurn());
    }
    #region SmallStuff
    static int SortBySpeed(BaseCharacterObject c1, BaseCharacterObject c2)
    {
        return -(c1.Speed.CompareTo(c2.Speed));
    }
    void SetIcon()
    {
        Destroy(GameObject.FindGameObjectWithTag("Icon"));
        GameObject Icon = TurnOrder[CurrentTurn].icon;
        Instantiate(Icon, IconSpawn.transform.position, IconSpawn.transform.rotation);
    }
    IEnumerator WaitABitBeforeTurn()
    {
        yield return new WaitForSeconds(.5f);
        midTurn = false;
    }
    #endregion
}
