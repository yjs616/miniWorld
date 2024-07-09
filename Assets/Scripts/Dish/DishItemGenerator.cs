using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishItemGenerator : MonoBehaviour
{
    public GameObject[] breadPrefabs; // 여러 종류의 빵 프리팹을 여기에 할당
    public GameObject forkPrefab; // 한 종류의 포크 프리팹을 여기에 할당
    
    float span = 1.0f;
    float delta = 0;
    int ratio = 2;
    float speed = -0.03f;

    private float[] xPositions = { 11.5f, 13.5f, 15.5f };
    private float[] zPositions = { -1.5f, -3.5f, -5.5f };

    //다른 클래스에서 호출할 수 있도록 public으로 설정
    public void SetParameter(float span, float speed, int ratio)
    {
        this.span = span;
        this.speed = speed;
        this.ratio = ratio;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;     // 전 frame과 현재 frame 간 시간 차이
        if (this.delta > this.span)
        { // span 시간이 지나면
            this.delta = 0; // 다시 시간 초기화

            GameObject item;

            int dice = Random.Range(1, 11); // 1~10까지의 정수 반환

            if (dice <= this.ratio)
            {
                // 비율에 따라 포크 프리팹 생성
                item = Instantiate(forkPrefab) as GameObject;
            }
            else
            {
                // 비율에 따라 빵 프리팹 생성
                int randomIndex = Random.Range(0, breadPrefabs.Length);
                item = Instantiate(breadPrefabs[randomIndex]) as GameObject;
            }

            float x = xPositions[Random.Range(0, xPositions.Length)];
            float z = zPositions[Random.Range(0, zPositions.Length)];
            item.transform.position = new Vector3(x, 18.0f, z);

            item.GetComponent<DishItemController>().dropSpeed = this.speed;
        }
    }
}
