using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] roomPrefabs; // �� ������ �迭
    public int stairRoomIndex;

    public GameObject aStarGridPrefab; // AStarGrid ������ �߰�

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

            // ��� ���� -1�� �ʱ�ȭ�մϴ�.
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    mapGrid[x, y] = -1;
                }
            }

            GenerateMap();
            InstantiateRooms();

            GameObject aStarGridInstance = Instantiate(aStarGridPrefab, Vector3.zero, Quaternion.identity);
            aStarGridInstance.transform.parent = floors[i].transform;

            Vector3 gridPosition = new Vector3(0.5f, -0.25f, 0f);
            aStarGridInstance.transform.position = gridPosition;
        }
    }

    void GenerateMap()
    {
        // ��� ������ ���� �ε����� ����Ʈ�� ����ϴ�.
        List<int> availableRoomIndices = new List<int>();
        for (int i = 0; i < roomPrefabs.Length; i++)
        {
            if (i != stairRoomIndex)
            {
                availableRoomIndices.Add(i);
            }
        }

        int stairX = Random.Range(0, mapWidth);
        int stairY = Random.Range(0, mapHeight);
        mapGrid[stairX, stairY] = stairRoomIndex;

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if (!(x == stairX && y == stairY) && availableRoomIndices.Count > 0) // �̹� ����� ��ġ�� ���� ��� ������ ���� ���� ���� �����մϴ�.
                {
                    int randomIndexInList = Random.Range(0, availableRoomIndices.Count);
                    int roomIndexInPrefabArray = availableRoomIndices[randomIndexInList];
                    availableRoomIndices.RemoveAt(randomIndexInList); // ���õ� �� �ε����� ����Ʈ���� �����մϴ�.

                    mapGrid[x, y] = roomIndexInPrefabArray;
                }
            }
        }
    }


    void InstantiateRooms()
    {
        GameObject floor = new GameObject("Floor");
        floor.transform.position = Vector3.zero;
        floors.Add(floor);

        float roomSpacingX = 0.5f;
        float roomSpacingY = 0.25f;

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if (mapGrid[x, y] != -1)
                {
                    Vector3 positionIsoMetric = new Vector3((x - y) * (4f + roomSpacingX), (-x - y) * (2f + roomSpacingY), 0);
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
            // ���� sortingOrder�� Y ��ġ�� ����Ͽ� ���ο� sortingOrder ����
            renderer.sortingOrder += -Mathf.RoundToInt(instance.transform.position.y * 100);
        }

        // �ν��Ͻ��� �ڽĵ鿡 ���� ��� Renderer ������Ʈ ã��
        Renderer[] childRenderers = instance.GetComponentsInChildren<Renderer>();

        foreach (Renderer childRenderer in childRenderers)
        {
            if (childRenderer != renderer && childRenderer.name != "Floor")  // �θ� ������Ʈ�� �����ϱ� ���� üũ
            {
                // ���� sortingOrder�� Y ��ġ �� ����(depth)�� ����Ͽ� ���ο� sortingOrder ����. ���⼭ '10'�� ���Ƿ� ������ ������, ���� ������Ʈ������ �ʿ信 ���� �����ؾ� �մϴ�.
                childRenderer.sortingOrder += -Mathf.RoundToInt(childRenderer.transform.position.y * 100 + childRenderer.transform.position.z * 10);
            }
        }
    }
}
