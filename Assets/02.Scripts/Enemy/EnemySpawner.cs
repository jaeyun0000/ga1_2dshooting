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
    [SerializeField] private Enemy[] _enemyPrefabs;

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
        // 50%: Downward
        // 30%: Aimed
        // 20%: Homing
        int spawnEnemy;
        int enemyIndex = 0;
        spawnEnemy = UnityEngine.Random.Range(1, 101);

        // Todo: Scrptable Object를 사용해서 리팩토링
        // 이유1: 배열을 사용했지만 각 아이템이 어떤 프리팹인지 알 수가 없음
        // 이유2: 각 Enemy 스폰 확률을 매직 넘버로 하드코딩해서 유지보수가 어렵고 가독성 저하
        if (spawnEnemy <= 20)
        {
            enemyIndex = 0;
        }
        else if (spawnEnemy <= 50)
        {
            enemyIndex = 1;
        }
        else if (spawnEnemy <= 100)
        {
            enemyIndex = 2;
        }
        Enemy enemy = Instantiate(_enemyPrefabs[enemyIndex]);
        enemy.transform.position = transform.position;
    }
}