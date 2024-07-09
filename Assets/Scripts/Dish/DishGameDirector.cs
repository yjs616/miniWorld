using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DishGameDirector : MonoBehaviour
{

    GameObject breadText;       //먹은 빵 개수 텍스트
    GameObject hpText;          //체력 텍스트
    GameObject gameOverText;    //게임 오버 텍스트

    float bread = 0;            //먹은 빵 갯수
    float hp = 100;             //남은 체력 갯수

    public Image hpGauge; // HP 게이지 UI 요소
    public Image breadGauge; // Bread 게이지 UI 요소
    
    //bool gameOver = false;

    GameObject generator;

    //0으로 안끝나고 25로 끝남
    public void DecreaseHP(){
        this.hp -= 25f;          //체력 1/4 줄어듦
        this.hpGauge.fillAmount -= 0.25f;    //이미지 감소
        if (hpGauge.fillAmount < 0)
        {
            hpGauge.fillAmount = 0;
        }
        if(hp <=0){
            GameOver();
        }
    }

    public void IncreaseBread(){
        
        this.bread += 1f;        //갯수 한개씩 카운트
        this.breadGauge.fillAmount += 0.1f;      //이미지 증가

        if (breadGauge.fillAmount > 1)
        {
            breadGauge.fillAmount = 1;
        }

        if (bread >= 10)
        {
            StartCoroutine(Success());
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        this.generator = GameObject.Find("DishItemGenerator");
        this.breadText = GameObject.Find("breadCnt");   //텍스트
        this.hpText = GameObject.Find("hpCnt");         //텍스트
        this.hpGauge = GameObject.Find("hpGauge").GetComponent<Image>();    //이미지
        this.breadGauge = GameObject.Find("breadGauge").GetComponent<Image>();     //이미지

        this.gameOverText = GameObject.Find("GameOverText"); // 게임 오버 텍스트
        this.gameOverText.SetActive(false);
        

    }

    // Update is called once per frame
    void Update()
    {
        float fillAmount = breadGauge.fillAmount; // 게이지의 Fill Amount 값을 가져옴

        // 게이지에 따른 난이도 조절
        if(fillAmount < 0.3f){      //1,2
            this.generator.GetComponent<DishItemGenerator>().SetParameter(2f, -0.03f, 6);
        }else if(fillAmount >= 0.3f && fillAmount < 0.6f){
            this.generator.GetComponent<DishItemGenerator>().SetParameter(1f, -0.04f, 4);
        }else if(fillAmount >= 0.6f && fillAmount < 0.8f){
            this.generator.GetComponent<DishItemGenerator>().SetParameter(1f, -0.06f, 2);
        }else if(fillAmount >= 0.8f && fillAmount < 0.9f){
            this.generator.GetComponent<DishItemGenerator>().SetParameter(1f, -0.05f, 4);
        }else if(fillAmount >= 0.9f){
            this.generator.GetComponent<DishItemGenerator>().SetParameter(2f, -0.04f, 3);
        }

        this.breadText.GetComponent<TMPro.TextMeshProUGUI>().text = this.bread.ToString();
        this.hpText.GetComponent<TMPro.TextMeshProUGUI>().text = this.hp.ToString();
    }

    void GameOver()
    {
        this.generator.GetComponent<DishItemGenerator>().enabled = false; // 프리팹 생성 중지
        this.gameOverText.SetActive(true); // 게임 오버 텍스트 활성화
    }

    IEnumerator Success()
    {
        yield return new WaitForSeconds(2f);
        PlayerPrefs.SetInt("FromDishGame", 1);
        SceneManager.LoadScene("DiningRoom");
    }

}
