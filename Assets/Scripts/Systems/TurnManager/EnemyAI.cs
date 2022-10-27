using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    BaseCharacterObject enemy;
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
        int choice = Random.Range(1,100);
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
        Debug.Log(enemy.name + " Attacks!");
        EnemyAttack temp = gameObject.GetComponent<EnemyAttack>();
        temp.AttackTrigger();
        EndTurn();
    }
    private void Defend()
    {
        Debug.Log(enemy.name + " Defends!");
        enemy.isDefending = true;
        EndTurn();
    }
    private void Special()
    {
        Debug.Log(enemy.name + " Specials!");
        if (enemy.specialAbility != null)
        {
            Instantiate(enemy.specialAbility);
            EndTurn();
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
