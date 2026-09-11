using UnityEngine;

public class ItemPool : MonoBehaviour
{
    private static ItemPool _instance;
    public static ItemPool Instance => _instance;

    [Header("Item Prefabs")]
    [SerializeField] private Item[] _itemPrefabs;

    [Header("Pool Size")]
    [SerializeField] private int _poolSize = 30;

    private Item[,] _pool;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        _pool = new Item[_itemPrefabs.Length, _poolSize];

        // 창고 크기 만큼 총알을 미리 만들어서 집어 넣는다
        for (int i = 0; i < _itemPrefabs.Length; i++)
        {
            Item itemPrefabs = _itemPrefabs[i];

            for (int j = 0; j < _poolSize; j++)
            {
                Item item = Instantiate(itemPrefabs, transform);
                item.gameObject.SetActive(false); // 당장 사용할 거 아니기에 비활성화
                _pool[i, j] = item;
            }
        }
    }

    public Item GetItem(ItemType itemType)
    {
        for (int i = 0; i < _pool.GetLength(0); i++)
        {
            if (_pool[i, 0].ItemType != itemType)
            {
                continue;
            }

            for (int j = 0; j < _poolSize; j++)
            {
                Item item = _pool[i, j];

                // 비활성화 되어있는 (즉, 누가 빌려가지 않은) 총알 반환
                if (item.gameObject.activeSelf == false)
                {
                    item.gameObject.SetActive(true);
                    return item;
                }
            }
        }

        return null;
    }
}