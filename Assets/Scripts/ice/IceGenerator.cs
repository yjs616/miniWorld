using UnityEngine;

public class IceBallGenerator : MonoBehaviour
{
    public GameObject iceBallPrefab; // IceBall 프리팹을 연결하세요.

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
        {
            // 아이스볼을 카메라 앞 약간의 거리에서 생성
            GameObject iceball = Instantiate(iceBallPrefab, Camera.main.transform.position + Camera.main.transform.forward * 2f, Quaternion.identity) as GameObject;
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 worldDir = ray.direction;
            // 적절한 힘을 적용
            iceball.GetComponent<IceBallController>().Shoot(worldDir.normalized * 250f);
        }
    }
}
