using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 목표: 스페이스바를 누를 때마다 총알을 생성해서 발사하고 싶다
    // 필요 속성
    // - 총알 프리팹
    public GameObject BulletPrefab;
    // - 생성 위치(총구)
    public Transform FirePoint_1;
    public Transform FirePoint_2;
    
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 2. 총알 프리팹을 생성한다
            // Instantiate = 프리팹을 복사해서 (Monobehaviour를 상속받은)게임 오브젝트를 생성하고 씬에 넣어주는 기능
            GameObject bullet1 = Instantiate(BulletPrefab);
            GameObject bullet2 = Instantiate(BulletPrefab);
            bullet1.transform.position = FirePoint_1.position; // 생성한 총알의 위치를 총구의 위치로
            bullet2.transform.position = FirePoint_2.position;
        }
    }
}
