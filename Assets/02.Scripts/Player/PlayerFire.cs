using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 목표: 스페이스바를 누를 때마다 총알을 생성해서 발사하고 싶다
    // 필요 속성
    // - 총알 프리팹
    public GameObject BulletPrefab;
    // - 생성 위치(총구)
    public Transform LeftFirePoint;
    public Transform RightFirePoint;

    private float cooldown = 1f;
    private float cooldownTimer = 0f;

    private int autoAttack = 0;
    
    
    private void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (autoAttack == 0)
            {
                autoAttack = 1;
            }
            else if (autoAttack == 1)
            {
                autoAttack = 0;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && autoAttack == 0 && cooldownTimer <= 0)
        {
            // 2. 총알 프리팹을 생성한다
            // Instantiate = 프리팹을 복사해서 (Monobehaviour를 상속받은)게임 오브젝트를 생성하고 씬에 넣어주는 기능
            GameObject leftBullet = Instantiate(BulletPrefab);
            GameObject rightBullet = Instantiate(BulletPrefab);
            leftBullet.transform.position = LeftFirePoint.position; // 생성한 총알의 위치를 총구의 위치로
            rightBullet.transform.position = RightFirePoint.position;
            
            cooldownTimer = cooldown;
        }
        
        if (autoAttack == 1 && cooldownTimer <= 0)
        {
            GameObject leftBullet = Instantiate(BulletPrefab);
            GameObject rightBullet = Instantiate(BulletPrefab);
            
            leftBullet.transform.position = LeftFirePoint.position;
            rightBullet.transform.position = RightFirePoint.position;
            
            cooldownTimer = cooldown;
        }
    }
}