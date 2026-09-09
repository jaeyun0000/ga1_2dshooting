using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private Transform _bombLocation;

    [SerializeField] private float _cooldown = 10f;
    [SerializeField] private float _cooldownTimer = 0f;

    private void Update()
    {
        if (_cooldownTimer > 0f)
        {
            _cooldownTimer -= Time.deltaTime;
        }

        if (_cooldownTimer <= 0f && Input.GetKeyDown(KeyCode.B))
        {
            Bomb();

            _cooldownTimer = _cooldown;
        }
    }

    private void Bomb()
    {
        Debug.Log("폭탄 투하");
        GameObject bomb = Instantiate(_bombPrefab);
        bomb.transform.position = _bombLocation.position;
    }
}
