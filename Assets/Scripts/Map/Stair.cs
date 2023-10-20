using UnityEngine;
using UnityEngine.SceneManagement;

public class Stair : MonoBehaviour
{
    public bool isUpStair;

    private MapGenerator mapGenerator;

    private void Start()
    {
        mapGenerator = FindObjectOfType<MapGenerator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int currentFloorIndex = mapGenerator.floors.IndexOf(transform.parent.gameObject);

            if (isUpStair && currentFloorIndex != 0)
            {
                // �ö󰡴� ����̸� ���� ���� �� ����(�ε��� 0)�� �ƴ� ��� ���� �� �� �̵�
                mapGenerator.ActivateFloor(currentFloorIndex - 1);
                transform.parent.gameObject.SetActive(false);
            }
            else if (!isUpStair)
            {
                if (currentFloorIndex != mapGenerator.floors.Count - 1)
                {
                    // �������� ����̸� ���� ���� �� �Ʒ���(������ �ε���)�� �ƴ� ��� �Ʒ��� �� �� �̵�
                    mapGenerator.ActivateFloor(currentFloorIndex + 1);
                    transform.parent.gameObject.SetActive(false);
                }
                else
                {
                    // �� ������ �ε������� �������� ��ܿ� �����ϸ� GameClear �� �ε�
                    SceneManager.LoadScene("GameEnd");
                }
            }
        }
    }
}
