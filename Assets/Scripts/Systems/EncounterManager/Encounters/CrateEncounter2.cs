using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateEncounter2 : BaseEncounterFunctionality
{
     protected override void Effect1()
    {
        SceneData.instanceRef.mainText.text = "Breaking open the crates reveals nothing...";
        SceneData.instanceRef.EncounterManager.GetComponent<EncounterManager>().swapProp();
        SceneData.instanceRef.EncounterManager.GetComponent<EncounterManager>().hideOptions();
    }
    protected override void Effect2()
    {

    }
    protected override void Effect3()
    {

    }
}
