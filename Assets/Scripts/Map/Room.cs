using UnityEngine;

public class Room : MonoBehaviour
{
    public RoomConfig config; // 이 방의 설정
    public Vector2 Position { get; private set; }
    public Door[] Doors { get; private set; }

    public Room(Vector2 position)
    {
        Position = position;
        Doors = new Door[4];
    }

    private void Start()
    {
        if (config != null)
        {
            if (Random.value < config.monsterSpawnRate)
            {
                SpawnMonster();
            }

            CreateItems(config.itemSpawnCount);
        }
    }

    private void SpawnMonster()
    {
        Vector3 spawnPosition = GetRandomPositionInRoom();
        GameObject monsterPrefab = GetRandomMonster();

        if (monsterPrefab != null && Random.Range(0, 100f) <= config.monsterSpawnRate)
        {
            GameObject newMonster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
            newMonster.transform.SetParent(transform);
        }
    }

    private void CreateItems(int maxItems)
    {
        // 아이템을 생성할 개수 결정 (1부터 maxItems까지)
        int itemCount = Random.Range(1, maxItems + 1);

        for (int i = 0; i < itemCount; i++)
        {
            // itemPrefabs 배열에서 무작위로 아이템 프리팹 선택
            GameObject itemPrefab = GetRandomItem();

            if (itemPrefab != null)
            {
                Vector3 spawnPosition = GetRandomPositionInRoom();
                GameObject spawnedItem = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
                spawnedItem.transform.SetParent(transform);
            }
        }
    }

    private GameObject GetRandomItem()
    {
        if (config.itemPrefabs.Length == 0) return null;

        int randomIndex = Random.Range(0, config.itemPrefabs.Length);

        return config.itemPrefabs[randomIndex];
    }

    private Vector3 GetRandomPositionInRoom()
    {
        GameObject parentObject = transform.Find("Grid").gameObject;
        Transform floorTransform = parentObject.transform.Find("Floor");

        if (floorTransform == null)
        {
            Debug.LogError("No child object named 'Floor' found.");
            return Vector3.zero;
        }

        GameObject floor = floorTransform.gameObject;

        PolygonCollider2D polyCollider = floor.GetComponent<PolygonCollider2D>();

        Bounds bounds = polyCollider.bounds;

        Vector3 randomPos = Vector3.zero;
        int attempts = 0;

        // 충돌체 내부에서 무작위 위치 찾기 (최대 100번 시도)
        while (attempts < 100)
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);

            randomPos = new Vector3(x, y, 0);

            if (polyCollider.OverlapPoint(randomPos))
                break;

            attempts++;
        }

        return randomPos;
    }

    private GameObject GetRandomMonster()
    {
        if (config.zombies.Length == 0) return null;

        int randomIndex = Random.Range(0, config.zombies.Length);

        return config.zombies[randomIndex];
    }
}
