using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    Player,
    NPC
}

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Character/CharacterData")]

public class CharacterData : ScriptableObject
{
    public string playerName;
    public string npcName;
    public int selectedCharacterID;
    public Vector2 characterPosition;
    public CharacterType characterType; // ĳ���� ���� (player�Ǵ�NPC)
}


