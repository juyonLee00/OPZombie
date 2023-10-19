using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // ���� ������
    public Transform spawnPoint; // ���� ��ȯ�� ��ġ
    public float spawnInterval = 20.0f; // ��ȯ ���� (20��)

    private void Start()
    {
        // �ʱ⿡ �����ϰ� ���� �ֱ������� SpawnZombie �Լ��� ȣ��
        StartCoroutine(SpawnZombie());
    }

    private IEnumerator SpawnZombie()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // ���� ��ȯ�� ��ġ�� ���� �������� ����
            Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
