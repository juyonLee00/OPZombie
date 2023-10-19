using UnityEngine;

[CreateAssetMenu(fileName = "RoomConfig", menuName = "ScriptableObjects/RoomConfig", order = 1)]
public class RoomConfig : ScriptableObject
{
    [Range(0, 1f)] public float monsterSpawnRate; // ���Ͱ� ������ Ȯ�� (0~1)
    public GameObject[] itemPrefabs; // �� �濡�� ������ �� �ִ� ������ �����յ�

    // TODO: ���� ���ӿ��� ����� ���Ϳ� ������ Ŭ������ ��ü�ؾ� �մϴ�.
    public GameObject SpawnMonster()
    {
        if (Random.value < monsterSpawnRate)
        {
            // TODO: ���� ���ӿ��� ����� ���� ���� �������� ��ü�ؾ� �մϴ�.
            return new GameObject("Monster");
        }

        return null;
    }

    public GameObject SpawnItem()
    {
        if (itemPrefabs.Length > 0)
        {
            int index = Random.Range(0, itemPrefabs.Length);
            // TODO: ���� ���ӿ��� ����� ������ ���� �������� ��ü�ؾ� �մϴ�.
            return Instantiate(itemPrefabs[index]);
        }

        return null;
    }
}
