using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KitchenInitializer : MonoBehaviour
{
    public GameObject player;
    public string[] messages = {
        "이제 하나만 더 찾으면 돼!",
        "어디에 있을까..",
        "설마 냉장고..?",
        "에이 설마.. 한번 가봐?",
    };

    private KitchenPlayerController playerController;
    private KitchenUIDirector uiDirector;

    void Start()
    {

        playerController = player.GetComponent<KitchenPlayerController>();
        uiDirector = FindObjectOfType<KitchenUIDirector>(); // UIDirector 인스턴스 찾기

        // 플레이어 이동 비활성화
        playerController.EnableMovement(false);

        StartCoroutine(ShowMessagesAndEnablePlayer());
        
    }

    private IEnumerator ShowMessagesAndEnablePlayer()
    {
        yield return new WaitForSeconds(2f);

        uiDirector.ShowPlayerMessages(messages, 2.0f);

        yield return new WaitUntil(() => uiDirector.isMessageConfirmed);

        playerController.EnableMovement(true);
    }
}

