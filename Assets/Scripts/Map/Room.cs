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

    // 각 방향에 따른 문을 설정합니다.
    public void SetDoor(Door door, int direction)
    {
        Doors[direction] = door;
        door.SetRoom(this);
    }

    private void Start()
    {
        if (config != null)
        {
            if (Random.value < config.monsterSpawnRate)
            {
                SpawnMonster();
            }

            foreach (GameObject itemPrefab in config.itemPrefabs)
            {
                CreateItem(itemPrefab);
            }
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

    private void CreateItem(GameObject itemPrefab)
    {
        Vector3 spawnPosition = GetRandomPositionInRoom();

        GameObject newItem = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        newItem.transform.SetParent(transform);
    }

    private Vector3 GetRandomPositionInRoom()
    {
        Bounds bounds = GetComponentInChildren<PolygonCollider2D>().bounds;
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(randomX, 0f, randomZ);
    }

    private GameObject GetRandomMonster()
    {
        if (config.zombies.Length == 0) return null;

        int randomIndex = Random.Range(0, config.zombies.Length);

        return config.zombies[randomIndex];
    }
}
