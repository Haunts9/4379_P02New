using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnManager : MonoBehaviour
{
    [Header("Generic Turn Variables")]
    bool combat;
    public BaseBattleObject Battle;
    GameObject SpecialText;
    [SerializeField] GameObject IconSpawn;
    int enemyDollSpawn = 0;
    List<BaseCharacterObject> TurnOrder = new List<BaseCharacterObject>();
    List<BaseCharacterObject> Party = new List<BaseCharacterObject>();
    int AmountOfTurns = 0;
    int CurrentTurn = 0;
    bool midTurn = true;

    #region Player Turn
    [Header("Player Turn Variables")]
    [SerializeField] GameObject PlayerField;
    int DownedCharacters;
    #endregion
    #region Enemy Turn
    [Header("Enemy Turn Variables")]
    EnemyAI EnemyChoice;
    int maxEnemies;
    int curEnemies;
    #endregion
    //Read from party and battle lists to create turn order and rotate
    private void FixedUpdate()
    {
        if (combat == true)
        {
            selector();
        }
    }
    public void InitializeTurnOrder()
    {
        //Init List
        enemyDollSpawn = 0;
        curEnemies = 0;
        combat = true;
        foreach (BaseCharacterObject enemy in Battle.Enemies)
        {
            BaseCharacterObject temp = Instantiate(enemy);
            InitializeEnemy(temp);
            maxEnemies = Battle.Enemies.Length;
        }
        foreach (BaseCharacterObject character in SceneData.instanceRef.CharactersInParty)
        {
            TurnOrder.Add(character);
            Party.Add(character);
            character.selectedTarget = null;
        }
        //End List Init
        //Sort List by Speed
        TurnOrder.Sort(SortBySpeed);
        AmountOfTurns = TurnOrder.Count;
        for (int k = 0; k < AmountOfTurns; k++)
        {
            TurnOrder[k].TurnPosition = k;
        }
        //Initialize Enemy AI
        EnemyChoice = gameObject.GetComponent<EnemyAI>();
        //End AI Init
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
    private void NextTurn()
    {
        StopCoroutine(EndTurn());
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
        Debug.Log("It's now " + SceneData.instanceRef.CurrentTurnAccessor.name + "'s turn");
        //SetIcon();
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
    private void PlayerTurn()
    {
        SceneData.instanceRef.CurrentTurnAccessor.isDefending = false;
        if (SceneData.instanceRef.CurrentTurnAccessor.CurrentHP > 0)
        {
            SetIcon();
            if (SceneData.instanceRef.CurrentTurnAccessor.selectedTarget == null || SceneData.instanceRef.CurrentTurnAccessor.selectedTarget.CurrentHP ==0)
            {
                SceneData.instanceRef.CurrentTurnAccessor.selectedTarget = TurnOrder.Find(x => x.isPlayer == false && x.CurrentHP >0);
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
        else
        {
            midTurn = false;
            StartCoroutine(EndTurn());
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
        StartCoroutine(EndTurn());
    }
    public void DefendButtonPress()
    {
        Debug.Log(TurnOrder[CurrentTurn].characterName + " Defends!");
        PlayerField.SetActive(false);
        SceneData.instanceRef.CurrentTurnAccessor.isDefending = true;
        StartCoroutine(WaitABitBeforeTurn());
        StartCoroutine(EndTurn());
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
            StartCoroutine(EndTurn());
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
        StartCoroutine(EndTurn());
        //StartCoroutine(WaitABitBeforeTurn());
    }
    #endregion
    #region Enemy
    private void EnemyTurn()
    {
        if (SceneData.instanceRef.CurrentTurnAccessor.CurrentHP > 0)
        {
            SetIcon();
            int choice = Random.Range(0, TurnOrder.Count);
            while (TurnOrder[choice].isPlayer == false)
            {
                choice = Random.Range(0, TurnOrder.Count);
            }
            SceneData.instanceRef.CurrentTurnAccessor.selectedTarget = TurnOrder[choice];
            EnemyChoice.Initialize();        
            StartCoroutine(WaitABitBeforeTurn());
            StartCoroutine(EndTurn());
        }
        else
        {
            midTurn = false;
            StartCoroutine(EndTurn());
        }
    }
    private void InitializeEnemy(BaseCharacterObject enemy)
    {
        enemy.dollSpawn = SceneData.instanceRef.EnemyDollSpawnLocations[enemyDollSpawn];
        enemyDollSpawn++;
        enemy.CurrentHP = enemy.MaxHP;
        GameObject doll = Instantiate(enemy.doll, enemy.dollSpawn.transform);
        Selector select = doll.GetComponent<Selector>();
        HPBar hpBar = doll.GetComponentInChildren<HPBar>();
        select.SetTarget(enemy);
        hpBar.SetTarget(enemy);
        // spawn
        TurnOrder.Add(enemy);
    }
    #endregion
    private void DoBattleCheck()
    {
        midTurn = true;
        curEnemies = 0;
        DownedCharacters = 0;
        foreach (BaseCharacterObject turn in TurnOrder)
        {
            if (turn.isPlayer != true && turn.CurrentHP == 0)
            {
                curEnemies++;
            }
        }
        foreach (BaseCharacterObject character in Party)
        {
            if (character.CurrentHP == 0)
            {
                DownedCharacters++;
            }
        }

        if (DownedCharacters == SceneData.instanceRef.CharactersInParty.Length)
        {
            Debug.Log("combat lost");
            //doLossThing
            midTurn = false;
            combat = false;
            CleanUp();
        }
        else if (curEnemies >= maxEnemies)
        {
            Debug.Log("combat won");
            //doEndVictoryThing
            midTurn = false;
            combat = false;
            CleanUp();
        }
        else
        {
            Debug.Log("next turn");
            NextTurn();
        }
    }
    #region SmallStuff
    static int SortBySpeed(BaseCharacterObject c1, BaseCharacterObject c2)
    {
        return -(c1.Speed.CompareTo(c2.Speed));
    }
    void SetIcon()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("Icon");
        foreach (GameObject icon in list)
        {
            Destroy(icon);
        }
        GameObject Icon = TurnOrder[CurrentTurn].icon;
        Instantiate(Icon, IconSpawn.transform.position, IconSpawn.transform.rotation);
    }
    IEnumerator WaitABitBeforeTurn()
    {

        yield return new WaitForSeconds(.5f);
        midTurn = false;
    }
    IEnumerator EndTurn()
    {
        if (combat == true)
        {
            selector();
            if (midTurn == false && SceneData.instanceRef.SetBusy == false)
            {

                DoBattleCheck();
            }
            else
            {
                yield return new WaitForSeconds(.5f);
                StartCoroutine(EndTurn());
            }
        }
    }
    void CleanUp()
    {
        foreach (BaseCharacterObject turn in TurnOrder)
        {
            if (turn.isPlayer == false)
            {
                Destroy(turn);
            }
        }
    }
    #endregion
}
