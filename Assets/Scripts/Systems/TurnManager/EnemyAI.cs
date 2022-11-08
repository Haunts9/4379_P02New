using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    BaseCharacterObject enemy;
    [Header("Attack>Defend>Special")]
    int AttackRange;
    int DefendRange;
    public void Initialize()
    {
        enemy = SceneData.instanceRef.CurrentTurnAccessor;
        enemy.isDefending = false;
        AttackRange = enemy.ActionRange[0];
        DefendRange = enemy.ActionRange[1];
        ChooseAction();
    }
    private void ChooseAction()
    {
        int choice = Random.Range(1,101);
        if (choice <= AttackRange)
        {
            Attack();
        }
        else if (choice > AttackRange && choice <= DefendRange)
        {
            Defend();
        }
        else
        {
            Special();
        }
    }
    private void Attack()
    {
        //Debug.Log(enemy.name + " Attacks!");
        SceneData.instanceRef.mainText.text = (enemy.characterName + " attacks " + enemy.selectedTarget.characterName + "!");
        EnemyAttack temp = gameObject.GetComponent<EnemyAttack>();
        temp.AttackTrigger();
        EndTurn();
    }
    private void Defend()
    {
        SceneData.instanceRef.mainText.text = (enemy.characterName + " defends!");
        enemy.isDefending = true;
        EndTurn();
    }
    private void Special()
    {
        
        if (enemy.specialAbility != null)
        {
            if (enemy.currentCooldown == 0)
            {
                SceneData.instanceRef.mainText.text = (enemy.characterName + " uses " + enemy.specialAbilityName + "!");
                enemy.currentCooldown = enemy.specialCooldown;
                Instantiate(enemy.specialAbility);
                EndTurn();
            }
            else
            {
                enemy.currentCooldown--;
                Attack();
            }
            
        }
        else
        {
            Attack();
        }
    }
    void EndTurn()
    {

    }
}
