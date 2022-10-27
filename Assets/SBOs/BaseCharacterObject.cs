
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SBOs/BaseCharacterObject", order = 1)]
public class BaseCharacterObject : ScriptableObject
{
    [Header("Important Stuff")]
    [SerializeField] public bool isPlayer = false;
    [SerializeField] public string characterName;
    [SerializeField] public GameObject doll;
    [SerializeField] public GameObject dollSpawn;
    [SerializeField] public GameObject icon;
    [Header("Special")]
    [SerializeField] public GameObject specialAbility;
    [SerializeField] public string specialAbilityName;
    [SerializeField] public int specialCooldown;
    [SerializeField] public int currentCooldown;
    [Header("Stats")]
    [SerializeField] public int attackAmount;
    [SerializeField] public float beatModifier = 1f;
    [SerializeField] public float beatZoneSlow = .1f;
    [SerializeField] public int MaxHP;
    [SerializeField] public int CurrentHP;
    [SerializeField] public int Speed;
    [SerializeField] public int Attack;
    [SerializeField] public int Defense;
    [Header("Enemy Only Stuff")]
    [SerializeField] public int[] ActionRange;
    [Header("Script Modifiers")]
    public bool isDefending = false;
    public BaseCharacterObject selectedTarget;
    public int TurnPosition;
}
