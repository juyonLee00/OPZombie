using UnityEngine;

[CreateAssetMenu(fileName = "RoomConfig", menuName = "ScriptableObjects/RoomConfig", order = 1)]
public class RoomConfig : ScriptableObject
{
    [Range(0, 100f)] public float monsterSpawnRate; // 몬스터가 스폰될 확률 (0~1)
    public GameObject[] zombies;
    public GameObject[] itemPrefabs; // 이 방에서 생성될 수 있는 아이템 프리팹들
}
