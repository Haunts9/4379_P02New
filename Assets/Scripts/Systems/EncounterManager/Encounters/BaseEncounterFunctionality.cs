using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEncounterFunctionality : MonoBehaviour
{
    protected abstract void Effect1();
    protected abstract void Effect2();
    protected abstract void Effect3();
    // Start is called before the first frame update
    private void Start()
    {
        EncounterEffect(SceneData.instanceRef.choice);
    }
    public void EncounterEffect(int buttonPressed)
    { 
        switch(buttonPressed)
        {
            case 1:
                Effect1();
                break;
            case 2:
                Effect2();
                break;
            case 3:
                Effect3();
                break;
        }
    }

}
