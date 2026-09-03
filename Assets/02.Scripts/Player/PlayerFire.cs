using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 목표: 스페이스바를 누를 때마다 총알을 생성해서 발사하고 싶다
    // 필요 속성
    // - 총알 프리팹
    public GameObject bulletPrefab;
    public GameObject subBulletPrefab;

    // - 생성 위치(총구)
    // public Transform[] firePoint;
    public Transform leftFirePoint;
    public Transform rightFirePoint;
    public Transform leftSubFirePoint;
    public Transform rightSubFirePoint;

    public float cooldown = 1f;
    public float cooldownTimer = 0f;

    public int autoAttack = 0;


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
            Fire();
        }

        if (autoAttack == 1 && cooldownTimer <= 0)
        {
            Fire();
        }
    }

    private void Fire()
    {
        // 2. 총알 프리팹을 생성한다
        // Instantiate = 프리팹을 복사해서 (Monobehaviour를 상속받은)게임 오브젝트를 생성하고 씬에 넣어주는 기능
        GameObject leftBullet = Instantiate(bulletPrefab);
        GameObject rightBullet = Instantiate(bulletPrefab);
        GameObject leftSubBullet = Instantiate(subBulletPrefab);
        GameObject rightSubBullet = Instantiate(subBulletPrefab);

        leftBullet.transform.position = leftFirePoint.position;
        rightBullet.transform.position = rightFirePoint.position;
        leftSubBullet.transform.position = leftSubFirePoint.position; // 생성한 총알의 위치를 총구의 위치로
        rightSubBullet.transform.position = rightSubFirePoint.position;


        cooldownTimer = cooldown;
    }
}