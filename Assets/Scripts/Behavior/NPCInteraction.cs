using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private GameObject popUpPanel;

    private void Start()
    {
        //시작 시 패널을 비활성화
        if ( popUpPanel != null )
        {
            popUpPanel.SetActive( false );
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //플레이어가 범위에 들어오면 패널 활성화
            if( popUpPanel != null )
            {
                popUpPanel.SetActive ( true );
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            //플레이어가 범위 밖으로 나가면 패널 비활성화
            if (popUpPanel != null )
            {
                popUpPanel.SetActive(false );
            }
        }
    }
}
