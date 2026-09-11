using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 40;
    public float moveSpeed = 8f;

    private AudioSource _audioSource;

    [SerializeField] private BulletType _bulletType;
    public BulletType BulletType => _bulletType;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnSpawn()
    {
        // 프리팹이 풀에 의해서 활성화 될 때마다
        // 초기화 하는 코드들이 들어간다.
        PlaySound();
    }

    // 활성화 될 때마다 자동으로 호출되는 이벤트 함수
    private void PlaySound()
    {
        if (_audioSource != null)
        {
            Debug.Log("총알 활성화");
            _audioSource.pitch = UnityEngine.Random.Range(0.6f, 1.4f);
            _audioSource.Play();
        }
    }

    private void Update()
    {
        Vector2 direction = Vector2.up; // new Vector2(1, 0);
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }


    // 트리거 관련 이벤트
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // 나 죽고
            // Destroy(this.gameObject);
            gameObject.SetActive(false); // 비활성화

            // GetComponent<타입> -> 게임 오브젝트가 가지고 있는 컴포넌트를 참조
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            // 응집도는 높히고, 결합도는 낮춰라
            // 결합도란 묻는 거.. 매번 묻는 거..
            // 무적모드 검사하고
            // 방어력 검사..
            enemy.TakeDamage(damage);
        }
    }


    // 충돌 관련 이벤트 (Enter -> Stay -> Exit)
    // 충돌이 시작되면 호출되는 이벤트
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("충돌 했다");
    }
}