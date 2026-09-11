using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private static BulletPool _instance;
    public static BulletPool Instance => _instance;

    // 오브젝트 풀링이란: 오브젝트의 Pool(웅덩이: 창고)을 만들어두고,
    // 그 창고안에 게임 오브젝트를 미리 필요한 만큼 만들어두고,
    // 필료할 때마다 꺼내서 사용하고 필요가 없으면 반환하는 식으로    (활성화/비활성화)
    // 메모리 할당과(객체의 생성) 해제(파괴)를 최소화해서 성능 Up!

    // 필요 속성
    [Header("Bullet Prefabs")]
    [SerializeField] private Bullet[] _bulletPrefabs;

    [Header("Pool Size")]
    [SerializeField] private int _poolSize = 30;

    // 생성한 총알을 담아둘 풀
    private Bullet[,] _pool;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        // 창고를 창고 크기만큼 만든다
        _pool = new Bullet[_bulletPrefabs.Length, _poolSize];

        // 창고 크기 만큼 총알을 미리 만들어서 집어 넣는다
        for (int i = 0; i < _bulletPrefabs.Length; i++)
        {
            Bullet bulletPrefab = _bulletPrefabs[i];

            for (int j = 0; j < _poolSize; j++)
            {
                Bullet bullet = Instantiate(bulletPrefab, transform);
                bullet.gameObject.SetActive(false); // 당장 사용할 거 아니기에 비활성화
                _pool[i, j] = bullet;
            }
        }
    }

    public Bullet GetBullet(BulletType bulletType)
    {
        for (int i = 0; i < _pool.Length; i++)
        {
            if (_pool[i, 0].BulletType != bulletType)
            {
                continue;
            }

            for (int j = 0; j < _poolSize; j++)
            {
                Bullet bullet = _pool[i, j];

                // 비활성화 되어있는 (즉, 누가 빌려가지 않은) 총알 반환
                if (bullet.gameObject.activeSelf == false)
                {
                    bullet.gameObject.SetActive(true);
                    bullet.OnSpawn();
                    return bullet;
                }
            }
        }

        return null;
    }
}