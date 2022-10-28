using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorTaunt : MonoBehaviour
{
    [SerializeField] BaseCharacterObject Warrior;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Taunt Started");
        SceneData.instanceRef.isTaunting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Warrior.currentCooldown <= 2)
        {
            Debug.Log("Taunt Ended");
            SceneData.instanceRef.isTaunting = false;
            Destroy(gameObject);
        }
    }
}
