using System.Collections;
using UnityEngine;


public class DiningRoomInitializer : MonoBehaviour
{
    public GameObject player;
    public string[] messages = {
        "으챠 의자 올라오는 것도 힘드네.",
        "약이 액자에 있네!?",
        "더 높게 점프할 힘이 필요하겠는데",
        "오케이 빵을 먹어야겠어."
    };

    private DiningRoomPlayerController playerController;
    private DiningRoomUIDirector uiDirector;

    void Start()
    {
        // PlayerPrefs 값 확인
        //int hasEnteredDiningRoom = PlayerPrefs.GetInt("HasEnteredDiningRoom", 0);
        //Debug.Log("HasEnteredDiningRoom 값: " + hasEnteredDiningRoom);

        //PlayerPrefs.SetInt("HasEnteredDiningRoom", 1);
        //Debug.Log("PlayerPrefs.SetInt(\"HasEnteredDiningRoom\", 1) 호출됨");

        playerController = player.GetComponent<DiningRoomPlayerController>();
        uiDirector = FindObjectOfType<DiningRoomUIDirector>(); // UIDirector 인스턴스 찾기

        Debug.Log("visited_f 값: 1" + PlayerPrefs.GetInt("visited_f", 0));
        // 처음 진입인지 확인
        if (PlayerPrefs.GetInt("visited_f", 0) == 0)
        {
            Debug.Log("visited_f 값:2 " + PlayerPrefs.GetInt("visited_f", 0));
            
            // 처음 진입인 경우
            PlayerPrefs.SetInt("visited_f", 1);

            // 플레이어 위치 설정
            //player.transform.position = new Vector3(4.25f, 3.9f, -3.15f);
            //player.transform.rotation = Quaternion.identity;

            // 플레이어 이동 비활성화
            playerController.EnableMovement(false);

            StartCoroutine(ShowMessagesAndEnablePlayer());
        }else{
            Debug.Log("실행됨2");
            // DishGame에서 돌아온 경우
            playerController.EnableMovement(true);
        }
    }

    private IEnumerator ShowMessagesAndEnablePlayer()
    {
        uiDirector.ShowPlayerMessages(messages, 2.0f);

        yield return new WaitUntil(() => uiDirector.isMessageConfirmed);

        playerController.EnableMovement(true);
    }
}
