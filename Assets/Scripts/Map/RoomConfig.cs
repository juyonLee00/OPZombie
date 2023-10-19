using UnityEngine;

[CreateAssetMenu(fileName = "RoomConfig", menuName = "ScriptableObjects/RoomConfig", order = 1)]
public class RoomConfig : ScriptableObject
{
    [Range(0, 1f)] public float monsterSpawnRate; // 몬스터가 스폰될 확률 (0~1)
    public GameObject[] itemPrefabs; // 이 방에서 생성될 수 있는 아이템 프리팹들

    // TODO: 실제 게임에서 사용할 몬스터와 아이템 클래스로 교체해야 합니다.
    public GameObject SpawnMonster()
    {
        if (Random.value < monsterSpawnRate)
        {
            // TODO: 실제 게임에서 사용할 몬스터 생성 로직으로 교체해야 합니다.
            return new GameObject("Monster");
        }

        return null;
    }

    public GameObject SpawnItem()
    {
        if (itemPrefabs.Length > 0)
        {
            int index = Random.Range(0, itemPrefabs.Length);
            // TODO: 실제 게임에서 사용할 아이템 생성 로직으로 교체해야 합니다.
            return Instantiate(itemPrefabs[index]);
        }

        return null;
    }
}
