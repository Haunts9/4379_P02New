using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    [SerializeField] BaseCharacterObject myTarget;
    [SerializeField] GameObject SelectorGraphic;
    // Start is called before the first frame update
    void Start()
    {
        SelectorGraphic.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SceneData.instanceRef.CurrentTurnAccessor != null && SelectorGraphic != null && myTarget != null)
        {
            if (SceneData.instanceRef.CurrentTurnAccessor.selectedTarget == myTarget)
            {
                SelectorGraphic.SetActive(true);
            }
            else
            {
                SelectorGraphic.SetActive(false);
            }
        }

    }
    public void SetTarget (BaseCharacterObject target)
    {
        myTarget = target;
    }
}
