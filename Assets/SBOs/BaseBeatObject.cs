
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SBOs/BaseBeatObject", order = 1)]
public class BaseBeatObject : ScriptableObject
{
    [SerializeField] public float beatSpeed = 1f;
}
