using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Character/CharacterData")]

public class CharacterData : ScriptableObject
{
    public string playerName;
    public int selectedCharacterID;
}
