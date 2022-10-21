using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTBOYSCRIPT : MonoBehaviour
{
    TurnManager test;

    void Start()
    {
        test = SceneData.instanceRef.TurnManager.GetComponent<TurnManager>();
        test.InitializeTurnOrder();
    }

}
