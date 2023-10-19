using UnityEngine;

[CreateAssetMenu(fileName = "RoomConfig", menuName = "ScriptableObjects/RoomConfig", order = 1)]
public class RoomConfig : ScriptableObject
{
    [Range(0, 100f)] public float monsterSpawnRate; // ���Ͱ� ������ Ȯ�� (0~1)
    public GameObject[] zombies;
    public GameObject[] itemPrefabs; // �� �濡�� ������ �� �ִ� ������ �����յ�
}
