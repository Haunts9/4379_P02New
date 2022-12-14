using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBlipScript : BlipScript
{
    BaseCharacterObject target;
    int damage;
    private void Start()
    {
        //Target();
    }
    private void Target()
    {
        target = SceneData.instanceRef.CurrentTurnAccessor.selectedTarget;
        //Debug.Log("Current Target: " + target);
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
        Debug.Log("Do Player Miss");
    }
    protected override void Hit()
    {
        Target();
        Debug.Log("Do Player Hit");
        target.CurrentHP -= damage;
        if (target.CurrentHP < 0)
        {
            target.CurrentHP =0;
            Debug.Log("Switch Target");
            if (SceneData.instanceRef.TurnOrder.Find(x => x.isPlayer == false && x.CurrentHP > 0) != null)
                SceneData.instanceRef.CurrentTurnAccessor.selectedTarget = SceneData.instanceRef.TurnOrder.Find(x => x.isPlayer == false && x.CurrentHP > 0);
    
        }
        Debug.Log(target.characterName + "'s HP is now" + target.CurrentHP);
    }
}
