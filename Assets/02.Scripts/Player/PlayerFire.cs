using UnityEngine;
using System;

public class PlayerFire : MonoBehaviour
{
    // 목표: 스페이스바를 누를 때마다 총알을 생성해서 발사하고 싶다
    // 필요 속성
    // - 총알 프리팹
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _subBulletPrefab;

    // - 생성 위치(총구)
    // public Transform[] firePoint;
    [SerializeField] private Transform _leftFirePoint;
    [SerializeField] private Transform _rightFirePoint;
    [SerializeField] private Transform _leftSubFirePoint;
    [SerializeField] private Transform _rightSubFirePoint;

    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private float _cooldownTimer = 0f;
    public float AttackCooldown => _attackCooldown;

    public int autoAttack = 1;


    private void Update()
    {
        if (_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;
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

        if (Input.GetKeyDown(KeyCode.Space) && autoAttack == 0 && _cooldownTimer <= 0)
        {
            Fire();
        }

        if (autoAttack == 1 && _cooldownTimer <= 0)
        {
            Fire();
        }
    }

    private void Fire()
    {
        // 2. 총알 프리팹을 생성한다
        // Instantiate = 프리팹을 복사해서 (Monobehaviour를 상속받은)게임 오브젝트를 생성하고 씬에 넣어주는 기능

        Bullet leftBullet = BulletPool.Instance.GetBullet(BulletType.Main);
        Bullet rightBullet = BulletPool.Instance.GetBullet(BulletType.Main);

        Bullet leftSubBullet = BulletPool.Instance.GetBullet(BulletType.Sub);
        Bullet rightSubBullet = BulletPool.Instance.GetBullet(BulletType.Sub);

        leftBullet.transform.position = _leftFirePoint.position;
        rightBullet.transform.position = _rightFirePoint.position;

        leftSubBullet.transform.position = _leftSubFirePoint.position; // 생성한 총알의 위치를 총구의 위치로
        rightSubBullet.transform.position = _rightSubFirePoint.position;

        _cooldownTimer = _attackCooldown;
    }

    public void AddAttackSpeed(float attack)
    {
        if (_attackCooldown > 0.1f)
        {
            // 사사오입 반올림 (변수를, ?자리까지, 사사오입을 사용해서)
            _attackCooldown = (float)Math.Round(_attackCooldown - attack, 2, MidpointRounding.AwayFromZero);
            ;
            Debug.Log($"현재 공격 속도: {_attackCooldown}");
        }
        else
        {
            Debug.Log($"최대 공격 속도 {_attackCooldown}");
        }
    }
}