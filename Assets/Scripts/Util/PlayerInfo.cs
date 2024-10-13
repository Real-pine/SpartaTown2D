using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public TextMeshProUGUI nameText;

    public void SetPlayerName(string name)
    {
        if ( nameText != null )
        {
            nameText.text = name;
        }
    }
}
