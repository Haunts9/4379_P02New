
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SBOs/BaseBattleObject", order = 1)]
public class BaseBattleObject : ScriptableObject
{
    [SerializeField] public string Description;
    [SerializeField] public BaseCharacterObject[] Enemies;
}

