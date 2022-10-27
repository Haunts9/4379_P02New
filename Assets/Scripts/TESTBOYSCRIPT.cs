using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTBOYSCRIPT : MonoBehaviour
{
    TravelManager test;

    void Start()
    {
        test = SceneData.instanceRef.TravelManager.GetComponent<TravelManager>();
        test.InitializeNextEvent();
    }

}
