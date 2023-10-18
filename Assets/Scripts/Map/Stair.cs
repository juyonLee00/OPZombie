using UnityEngine;

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

            if (isUpStair && currentFloorIndex < mapGenerator.floors.Count - 1)
            {
                // �ö󰡴� ����̸� ���� ���� �� ������ �ƴ� ��� ���� ������ �̵�
                mapGenerator.ActivateFloor(currentFloorIndex + 1);
                transform.parent.gameObject.SetActive(false);
            }
            else if (!isUpStair && currentFloorIndex > 0)
            {
                // �������� ����̸� ���� ���� �� �Ʒ����� �ƴ� ��� ���� ������ �̵�
                mapGenerator.ActivateFloor(currentFloorIndex - 1);
                transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
