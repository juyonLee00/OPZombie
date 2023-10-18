using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] roomPrefabs; // �� ������ �迭
    public int stairRoomIndex;

    private int[,] mapGrid; // �� �׸���
    public int mapWidth; // �� ���� ũ��
    public int mapHeight; // �� ���� ũ��
    public int totalFloor;

    public List<GameObject> floors = new List<GameObject>();

    void Start()
    {
        mapGrid = new int[mapWidth, mapHeight];
        GenerateFloors(totalFloor);
        ActivateFloor(0);
    }

    void GenerateFloors(int floorCount)
    {
        for (int i = 0; i < floorCount; i++)
        {
            mapGrid = new int[mapWidth, mapHeight];
            GenerateMap();
            InstantiateRooms();
        }
    }

    void GenerateMap()
    {
        int stairX = Random.Range(1, mapWidth - 1);
        int stairY = Random.Range(1, mapHeight - 1);
        mapGrid[stairX, stairY] = stairRoomIndex;

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if (x == 0 || y == 0 || x == mapWidth - 1 || y == mapHeight - 1)
                {
                    // ���� ������ ó��
                    mapGrid[x, y] = -1;
                }
                else if (x != stairX || y != stairY) // �̹� ����� ��ġ�� ���� �����մϴ�.
                {
                    int roomIndex;

                    do
                    {
                        roomIndex = Random.Range(0, roomPrefabs.Length);
                    } while (roomIndex == stairRoomIndex);

                    mapGrid[x, y] = roomIndex;
                }
            }
        }
    }

    void InstantiateRooms()
    {
        GameObject floor = new GameObject("Floor");
        floor.transform.position = Vector3.zero;
        floors.Add(floor);

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if (mapGrid[x, y] != -1)
                {
                    Vector3 positionIsoMetric = new Vector3((x - y) * 4f, (-x - y) * 2f, y - x);
                    GameObject roomInstance = Instantiate(roomPrefabs[mapGrid[x, y]], positionIsoMetric, Quaternion.identity);
                    roomInstance.transform.parent = floor.transform;
                    UpdateSortingOrder(roomInstance);
                }
            }
        }

        floor.SetActive(false);
    }

    public void ActivateFloor(int index)
    {
        for (int i = 0; i < floors.Count; i++)
        {
            if (i == index) floors[i].SetActive(true);
            else floors[i].SetActive(false);
        }
    }

    void UpdateSortingOrder(GameObject instance)
    {
        // �ν��Ͻ� ��ü�� ���� Renderer ������Ʈ ã��
        Renderer renderer = instance.GetComponent<Renderer>();

        if (renderer != null)
        {
            // ���� sortingOrder�� Y ��ġ�� �����Ͽ� ���ο� sortingOrder ����
            renderer.sortingOrder += -Mathf.RoundToInt(instance.transform.position.y * 100);
        }

        // �ν��Ͻ��� �ڽĵ鿡 ���� ��� Renderer ������Ʈ ã��
        Renderer[] childRenderers = instance.GetComponentsInChildren<Renderer>();

        foreach (Renderer childRenderer in childRenderers)
        {
            if (childRenderer != renderer && childRenderer.name != "Floor")  // �θ� ������Ʈ�� �����ϱ� ���� üũ
            {
                // ���� sortingOrder�� Y ��ġ �� ����(depth)�� �����Ͽ� ���ο� sortingOrder ����. ���⼭ '10'�� ���Ƿ� ������ ������, ���� ������Ʈ������ �ʿ信 ���� �����ؾ� �մϴ�.
                childRenderer.sortingOrder += -Mathf.RoundToInt(childRenderer.transform.position.y * 100 + childRenderer.transform.position.z * 10);
            }
        }
    }
}