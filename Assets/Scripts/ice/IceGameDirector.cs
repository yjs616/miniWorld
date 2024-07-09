using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class IceGameDirector : MonoBehaviour
{
    public TextMeshProUGUI hitText; // 맞춘 갯수를 표시할 텍스트
    public Image hitGauge; // 진행 상황을 표시할 UI 이미지
    public IceCameraController cameraController; // 카메라 컨트롤러 스크립트
    private int hitCount = 0; // 맞춘 갯수

    public GameObject gameOverText;    //게임 오버 텍스트
    public GameObject successText;    //게임 성공 텍스트
    public TextMeshProUGUI timerText; // 제한 시간을 표시할 텍스트
    public float timer;
    public ParticleSystem successParticles; // 성공 시 재생할 파티클 시스템
    private bool isSuccess = false;

    void Start()
    {
        // 초기 UI 설정
        this.hitText = GameObject.Find("hitText").GetComponent<TextMeshProUGUI>(); 
        this.hitGauge = GameObject.Find("hitGauge").GetComponent<Image>();

        this.gameOverText = GameObject.Find("GameOverText"); // 게임 오버 텍스트
        this.gameOverText.SetActive(false);

        this.successText = GameObject.Find("SuccessText"); // 게임 오버 텍스트
        this.successText.SetActive(false);

        this.timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();

        timer = 20f; // 제한 시간 초기화(20초)
        
    }

    private void Update() {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Ceil(timer).ToString();

            if (timer <= 0 && !isSuccess)
            {
                GameOver();
            }
        } 
    }

    public void TargetHit(GameObject targetIce)
    {
        hitCount++;
        //hitText.text = hitCount;        //맞힌 갯수
        hitGauge.fillAmount += 0.1f;

        this.hitText.GetComponent<TMPro.TextMeshProUGUI>().text = this.hitCount.ToString();

        AdjustCameraSpeed();

        if (hitCount >= 10)
        {
            isSuccess = true;
            GameSuccess(targetIce);
        }
    }

    private void AdjustCameraSpeed()
    {
        if (hitCount % 2 == 0 && hitCount <= 8)
        {
            cameraController.moveSpeed += 10f;
        }

        if (hitCount == 8)
        {
            cameraController.moveSpeed -= 10f;
        }

        if (hitCount == 9)
        {
            cameraController.moveSpeed -= 5f;
        }
    }

    private void GameSuccess(GameObject targetIce)
    {
        //성공했음 텍스트 활성화하고 씬 전환하기
        //게임 성공 시 처리
        this.successText.SetActive(true);
        successParticles.Play();

        // 타겟 아이스 파괴
        Destroy(targetIce);

        // 2초 후 씬 전환
        StartCoroutine(SuccessSequence());
    }

    private IEnumerator SuccessSequence()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("yard");
    }


    //제한 시간 추가해서 게임 오버 처리도 해야 될 듯
    private void GameOver()
    {
        // 게임 오버 시 처리
        gameOverText.SetActive(true);
        // 필요한 추가 작업 수행
    }

}

