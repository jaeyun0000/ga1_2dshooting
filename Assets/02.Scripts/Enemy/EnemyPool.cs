using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    private static EnemyPool _instance;
    public static EnemyPool Instance => _instance;

    [Header("Enemy Prefabs")]
    [SerializeField] private Enemy[] _enemyPrefabs;

    [Header("Pool Size")]
    [SerializeField] private int _poolSize = 30;

    private Enemy[,] _pool;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        _pool = new Enemy[_enemyPrefabs.Length, _poolSize];

        // 창고 크기 만큼 총알을 미리 만들어서 집어 넣는다
        for (int i = 0; i < _enemyPrefabs.Length; i++)
        {
            Enemy enemyPrefabs = _enemyPrefabs[i];

            for (int j = 0; j < _poolSize; j++)
            {
                Enemy enemy = Instantiate(enemyPrefabs, transform);
                enemy.gameObject.SetActive(false); // 당장 사용할 거 아니기에 비활성화
                _pool[i, j] = enemy;
            }
        }
    }

    public Enemy GetEnemy(EnemyType enemyType)
    {
        for (int i = 0; i < _pool.GetLength(0); i++)
        {
            if (_pool[i, 0].EnemyType != enemyType)
            {
                continue;
            }

            for (int j = 0; j < _poolSize; j++)
            {
                Enemy enemy = _pool[i, j];

                // 비활성화 되어있는 (즉, 누가 빌려가지 않은) 총알 반환
                if (enemy.gameObject.activeSelf == false)
                {
                    enemy.gameObject.SetActive(true);
                    return enemy;
                }
            }
        }

        return null;
    }
}