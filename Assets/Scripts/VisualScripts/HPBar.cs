using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField] BaseCharacterObject character;
    float scale;
    void FixedUpdate()
    {

        scale = (float)character.CurrentHP / (float)character.MaxHP;
        gameObject.transform.localScale = new Vector3(scale, 1, 1);

    }
}
