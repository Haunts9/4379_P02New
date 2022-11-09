using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SBOs/BaseLevelObject", order = 1)]
public class BaseLevelObject : ScriptableObject
{
    [Header("Main")]
    [SerializeField] public string floorName;
    [SerializeField] public AudioClip levelMusic;
    [SerializeField] public string[] floorplan;
    [Header("Visuals")]
    [SerializeField] public GameObject EntrancePanorama;
    [SerializeField] public GameObject[] Panoramas;
    [Header("Random Events")]
    [SerializeField] public BaseEncounterObject Entrance;
    [SerializeField] public BaseBattleObject[] possibleBattles;
    [SerializeField] public BaseBattleObject[] possibleBossBattles;
    [SerializeField] public BaseEncounterObject[] possibleEncounters;
}
