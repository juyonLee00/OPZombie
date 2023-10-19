using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float detectionDistance = 5f;
     // �÷��̾� ���̾�

    private Transform player; // �÷��̾��� Transform
    private AStarGrid aStarGrid;
    private List<AStarNode> currentPath;
    public float moveSpeed = 0.5f;
    public float originalMoveSpeed = 0.5f;

    private bool isChasingPlayer = false;
    private GameObject[] spriteObjects; // ���� ��������Ʈ ���� ������Ʈ �迭
    private int currentSpriteIndex = 0; // ���� ��������Ʈ �ε���



    private void Start()
    {
        aStarGrid = FindObjectOfType<AStarGrid>();
        player = Player.Instance.transform; // �÷��̾ Ž�� (Player Ŭ������ ����Ϸ��� Player ��ũ��Ʈ�� �ʿ��մϴ�.)
        currentPath = new List<AStarNode>();

        // ���� ��������Ʈ ���� ������Ʈ�� �������� �ʱ�ȭ
        spriteObjects = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            spriteObjects[i] = transform.Find("Character_Zombie3_" + i).gameObject;
            spriteObjects[i].SetActive(false);
        }

        // �ʱ� ��������Ʈ Ȱ��ȭ
        spriteObjects[currentSpriteIndex].SetActive(true);
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionDistance)
            {
                AStarNode startNode = aStarGrid.GetNodeFromWorld(transform.position);
                AStarNode endNode = aStarGrid.GetNodeFromWorld(player.position);

                // ��� ã��
                currentPath = aStarGrid.pathfinder.CreatePath(startNode, endNode);

                if (currentPath != null && currentPath.Count > 1)
                {
                    isChasingPlayer = true;

                    // ���� �̵� ������ ������� ��������Ʈ ����
                    Vector3 moveDirection = (new Vector3(currentPath[1].xPos, currentPath[1].yPos, transform.position.z) - transform.position).normalized;

                    // ���⿡ ���� ��������Ʈ �ε��� ����
                    if (moveDirection.x > 0 && moveDirection.y > 0)
                    {
                        // ��� �밢��
                        currentSpriteIndex = 1;
                    }
                    else if (moveDirection.x < 0 && moveDirection.y > 0)
                    {
                        // �»� �밢��
                        currentSpriteIndex = 2;
                    }
                    else if (moveDirection.x < 0 && moveDirection.y < 0)
                    {
                        // ���� �밢��
                        currentSpriteIndex = 3;
                    }
                    else
                    {
                        // �� ���� ��� (���� �밢��)
                        currentSpriteIndex = 0;
                    }
                }
                else
                {
                    isChasingPlayer = false;
                }
            }
            else
            {
                isChasingPlayer = false;
            }
        }

        // ���⿡ ���� ��������Ʈ ����
        for (int i = 0; i < 4; i++)
        {
            if (i == currentSpriteIndex)
            {
                spriteObjects[i].SetActive(true);
            }
            else
            {
                spriteObjects[i].SetActive(false);
            }
        }

        if (isChasingPlayer)
        {
            MoveZombieAlongPath();
        }
    }

    private void MoveZombieAlongPath()
    {
        if (currentPath != null && currentPath.Count > 1)
        {
            // ���� ��� �������� �̵�
            Vector3 nextPosition = new Vector3(currentPath[1].xPos, currentPath[1].yPos, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);

            // ���� ��ġ�� ��ǥ�� ����� ������ ���� ���� �̵�
            if (Vector3.Distance(transform.position, nextPosition) < 0.1f)
            {
                currentPath.RemoveAt(0);
            }
        }
        else
        {
            isChasingPlayer = false;
        }
    }

    public IEnumerator SlowDownZombieForSeconds(float seconds)
    {
        // �̵� �ӵ��� 0���� ����
        moveSpeed = 0f;

        // ������ �ð�(1��) ���� ���
        yield return new WaitForSeconds(seconds);

        // ���� �̵� �ӵ��� ����
        moveSpeed = originalMoveSpeed;
    }
}
