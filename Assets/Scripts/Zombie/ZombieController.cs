using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float detectionDistance = 5f;

    public LayerMask playerLayer; // �÷��̾� ���̾�
    private Transform player; // �÷��̾��� Transform
    private AStarGrid aStarGrid;
    private List<AStarNode> currentPath;
    private int moveSpeed = 1;
    private bool isChasingPlayer = false;

    private void Start()
    {
        aStarGrid = FindObjectOfType<AStarGrid>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // �÷��̾ Ž��
        currentPath = new List<AStarNode>();
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
}
