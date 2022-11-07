using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterMenuScript : MonoBehaviour
{
    [Header("Characters")]
    [SerializeField] BaseCharacterObject character1;
    [SerializeField] BaseCharacterObject character2;
    [SerializeField] BaseCharacterObject character3;
    [SerializeField] BaseCharacterObject character4;
    [Header("Text Boxes")]
    [SerializeField] TextMeshProUGUI NameText;
    [SerializeField] TextMeshProUGUI DescriptionText;
    [SerializeField] TextMeshProUGUI HpText;
    [SerializeField] TextMeshProUGUI SpeedText;
    [SerializeField] TextMeshProUGUI AttackText;
    [SerializeField] TextMeshProUGUI DefenseText;
    [SerializeField] TextMeshProUGUI SpecialText;
    [SerializeField] TextMeshProUGUI SpecialDescriptionText;
    private void Start()
    {
        ButtonPress(character1);
    }
    public void ButtonPress(BaseCharacterObject character)
    {
        NameText.text = ("Name: " + character.characterName);
        DescriptionText.text = (character.characterDescription);
        HpText.text = ("HP: " + character.CurrentHP + "/" + character.MaxHP);
        SpeedText.text = ("Speed: " + character.Speed);
        AttackText.text = ("Attack: " + character.Attack);
        DefenseText.text = ("Defense: " + character.Defense);
        SpecialText.text = ("Special: " + character.specialAbilityName);
        SpecialDescriptionText.text = (character.specialAbilityDescription);
    }

}
