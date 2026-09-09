using UnityEngine;

public class Player : MonoBehaviour
{
    // 캡슐화
    // - 데이터 은닉
    // - 메서드를 통한 상태 변경
    [SerializeField] private int _health = 100;

    [SerializeField] private GameObject _deathEffectPrefab;

    public int Health => _health;   // 람다식 문법을 활용한 읽기 전용 프로퍼티
    // {
    //     // set
    //     // {
    //     //     if (value < 0) return;
    //     //     _health = value;
    //     // }
    //     get
    //     {
    //         return _health;
    //     }
    // }


    // 잘 설계된 클래스는
    // - 필드 (인스턴스 변수)
    // - 필드에 잘못된 값이 할당되지 않게 막고, 정상적으로 동작하는 메서드

    // getter/setter : 특정 데이터를 get/set 해주는 메서드
    // public int GetHealth()
    // {
    //     return _health;
    // }

    // public void SetHealth(int value)
    // {
    //     // 무결성 검사를 해야한다.
    //     // 무결성 : 잘못된 데이터가 들어가지 않게 하는 것
    //     // - 최대 체력보다 체력은 적어야 한다..
    //     _health = value;
    // }

    private AudioSource _damagedAudioSource;

    private void Awake()
    {
        _damagedAudioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _damagedAudioSource.Play();
        if (_health <= 0)
        {
            SpawnDeathEffect();
            Destroy(gameObject);
        }
    }

    public void AddHealth(int heal)
    {
        _health += heal;
        Debug.Log($"현재 체력: {_health}");
    }

    private void SpawnDeathEffect()
    {
        Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);
    }

    // GetComponent<PlayerMove>().변수 = 수정;
}