using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DefendBlipScript : BlipScript
{
    BaseCharacterObject target;
    int damage;
    private void Start()
    {

    }
    private void Target()
    {
        target = SceneData.instanceRef.CurrentTurnAccessor.selectedTarget;
        //calculate damage
        if (target.isDefending == true)
        {
            damage = (SceneData.instanceRef.CurrentTurnAccessor.Attack - (target.Defense * 2));
        }
        else
        {
            damage = (SceneData.instanceRef.CurrentTurnAccessor.Attack - target.Defense);
        }
        //set damage to be positive
        if (damage <= 0)
        {
            damage = 1;
        }
        //calculate damage end
    }
    protected override void Miss()
    {
        Target();
        Debug.Log("Do Player Miss");
        target.CurrentHP -= damage;
        if (target.CurrentHP < 0)
        {
            target.CurrentHP = 0;
            SceneData.instanceRef.CurrentTurnAccessor.selectedTarget = SceneData.instanceRef.TurnOrder.Find(x => x.isPlayer == true && x.CurrentHP > 0);
        }
        Debug.Log(target.characterName + "'s HP is now" + target.CurrentHP);
    }
    protected override void Hit()
    {
        Target();
        //defense to reduce damage
        Debug.Log("Do Player Hit");
        if (target.isDefending == true)
        {
            damage = damage - (target.Defense * 2);
        }
        else
        {
            damage -= (target.Defense);
        }
        if (damage <= 0)
        {
            damage = 1;
        }
        //continue as planned
        target.CurrentHP -= damage;
        if (target.CurrentHP < 0)
        {
            target.CurrentHP = 0;
            if (SceneData.instanceRef.TurnOrder.Find(x => x.isPlayer == true && x.CurrentHP > 0) != null)
                SceneData.instanceRef.CurrentTurnAccessor.selectedTarget = SceneData.instanceRef.TurnOrder.Find(x => x.isPlayer == true && x.CurrentHP > 0);
        }
        Debug.Log(target.characterName + "'s HP is now" + target.CurrentHP);
    }
}
