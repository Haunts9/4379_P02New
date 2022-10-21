using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealBlipScript : BlipScript
{
    BaseCharacterObject target;
    int heal;
    private void Start()
    {
        target = SceneData.instanceRef.CurrentTurnAccessor.selectedTarget;
        //calculate damage
        heal = (SceneData.instanceRef.CurrentTurnAccessor.Attack);

        //set damage to be positive
        if (heal <= 0)
        {
            heal = 1;
        }
        //calculate damage end

    }
    protected override void Miss()
    {
        Debug.Log("Do Player Miss");
    }
    protected override void Hit()
    {
        Debug.Log("Clover heals for " + heal);
        target.CurrentHP += heal;
        if (target.CurrentHP > target.MaxHP)
        {
            target.CurrentHP = target.MaxHP;
        }
    }
}
