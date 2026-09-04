using UnityEngine;

// 역할: 일정 시간마다 적을 생성해주고 싶다.
public class EnemySpawner : MonoBehaviour
{
    // 필여 속성
    // - 타이머
    [Header("스폰 간격")]
    [SerializeField] float _spawnInterval = 3f;

    private float _timer;

    // - 생성할 프리팹
    [Header("스폰할 적 프리팹")]
    [SerializeField] private Enemy _downwardEnemyPrefab;
    [SerializeField] private Enemy _aimedEnemyPrefab;
    [SerializeField] private Enemy _homingEnemyPrefab;

    private void Update()
    {
        _timer += Time.deltaTime;


        if (_timer >= _spawnInterval)
        {
            _timer = 0f;

            _spawnInterval = UnityEngine.Random.Range(1f, 3f); // 1 ~ 3
            // int randowInt = UnityEngine.Random.Range(1, 3);     // int = 1 ~ 2

            Spawn();
        }
    }

    private void Spawn()
    {
        int spawnEnemy;
        spawnEnemy = UnityEngine.Random.Range(1, 101);

        if (spawnEnemy <= 20)
        {
            Enemy enemy = Instantiate(_homingEnemyPrefab);
            enemy.transform.position = transform.position;
        }
        else if (spawnEnemy <= 50)
        {
            Enemy enemy = Instantiate(_aimedEnemyPrefab);
            enemy.transform.position = transform.position;
        }
        else if (spawnEnemy <= 100)
        {
            Enemy enemy = Instantiate(_downwardEnemyPrefab);
            enemy.transform.position = transform.position;
        }
    }
}