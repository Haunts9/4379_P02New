using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SBOs/BaseEncounterObject", order = 1)]
public class BaseEncounterObject : ScriptableObject
{
    [SerializeField] public string encounterName;
    [SerializeField] public string encounterDescription;
    [SerializeField] public string[] encounterOptions;
    [SerializeField] public GameObject encounterProp;
    [SerializeField] public GameObject encounterPropUsed;
    [SerializeField] public GameObject encounterEffect;
}