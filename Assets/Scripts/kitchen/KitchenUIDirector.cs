using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KitchenUIDirector : MonoBehaviour
{

    public GameObject playerSayUI;
    public TextMeshProUGUI playerMessage;

    public bool isMessageConfirmed = false;  // 확인.


    //Start is called before the first frame update
    void Start()
    {
        playerSayUI.SetActive(false);
        
    }

    //플레이어 말
    public void ShowPlayerMessage(string message, float duration)
    {
        StartCoroutine(ShowPlayerMessageCoroutine(message, duration));
    }

    private IEnumerator ShowPlayerMessageCoroutine(string message, float duration)
    {
        playerSayUI.SetActive(true);
        playerMessage.text = message;
        yield return new WaitForSeconds(duration);
        playerSayUI.SetActive(false);
    }

    // 여러 메시지를 순차적으로 표시하는 메서드 추가
    public void ShowPlayerMessages(string[] messages, float duration)
    {
        StartCoroutine(ShowPlayerMessagesCoroutine(messages, duration));
    }

    private IEnumerator ShowPlayerMessagesCoroutine(string[] messages, float duration)
    {
        playerSayUI.SetActive(true);

        
        foreach (var message in messages)
        {
            playerMessage.text = message;
            yield return new WaitForSeconds(duration);
        }

        playerSayUI.SetActive(false);


        // 모든 메시지가 표시된 후 메시지 확인 상태 설정
        isMessageConfirmed = true;
    }


}

