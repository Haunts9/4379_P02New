using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DollName : MonoBehaviour
{
    [SerializeField] BaseCharacterObject target;
    // Start is called before the first frame update
    void Start()
    {
    
        TextMeshPro temp = GetComponent<TextMeshPro>();
        temp.text = target.characterName;
    }
}
