using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoisonBlipScript : BlipScript
{
    BaseCharacterObject target;
    private void Start()
    {
        Target();
    }
    private void Target()
    {
        target = SceneData.instanceRef.CurrentTurnAccessor.selectedTarget;
    }
    protected override void Miss()
    {
        //Target();
        Debug.Log("Do Player Miss");
        target.statusTimer = 4;
        target.Status = "Poisoned";
        /*if (target.CurrentHP < 0)
        {
            target.CurrentHP = 0;
            SceneData.instanceRef.CurrentTurnAccessor.selectedTarget = SceneData.instanceRef.TurnOrder.Find(x => x.isPlayer == true && x.CurrentHP > 0);
        }*/
        Debug.Log(target.characterName + " is now poisoned!");
    }
    protected override void Hit()
    {
        //Target();
        //defense to reduce damage
        Debug.Log("Do Player Hit");
    }
}
