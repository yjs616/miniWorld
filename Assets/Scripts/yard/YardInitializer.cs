using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class YardInitializer : MonoBehaviour
{
    public GameObject messagePanel;
    public TextMeshProUGUI messageText;
    public string endMessage = "드디어 끝났구나 이제 돌아가자!!";
    public GameObject successText;
    public YardPlayerController playerController;

    void Start()
    {
        successText.SetActive(false);
        StartCoroutine(DisplayEndMessage());
    }

    private IEnumerator DisplayEndMessage()
    {
        messagePanel.SetActive(true);
        messageText.text = endMessage;
        yield return new WaitForSeconds(2f);
        messagePanel.SetActive(false);
        playerController.canMove = true; // 텍스트 이후 움직일 수 있음
    }

    public void DisplaySuccess()
    {
        successText.SetActive(true);
        successText.GetComponent<RotationController>().StartRotation();
    }
}
