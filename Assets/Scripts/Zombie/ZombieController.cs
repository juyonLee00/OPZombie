<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float detectionDistance = 25f;

    public LayerMask playerLayer; // �÷��̾� ���̾�
    private Transform player; // �÷��̾��� Transform
    private AStarGrid aStarGrid;
    private List<AStarNode> currentPath;
    private float moveSpeed = 0.5f;
    private bool isChasingPlayer = false;

    private void Start()
    {
        aStarGrid = FindObjectOfType<AStarGrid>();
        player = Player.Instance.transform;  // �÷��̾ Ž��
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
            if (Vector3.Distance(transform.position, nextPosition) < 0.2f)
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
=======
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ZombieController : MonoBehaviour
//{
//    public float detectionDistance = 25f;

//    public LayerMask playerLayer; // �÷��̾� ���̾�
//    private Transform player; // �÷��̾��� Transform
//    private AStarGrid aStarGrid;
//    private List<AStarNode> currentPath;
//    private float moveSpeed = 0.5f;
//    private bool isChasingPlayer = false;

//    private void Start()
//    {
//        aStarGrid = FindObjectOfType<AStarGrid>();
//        player = Player.Instance.transform;  // �÷��̾ Ž��
//        currentPath = new List<AStarNode>();
//    }

//    private void Update()
//    {
//        if (player != null)
//        {
//            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

//            if (distanceToPlayer <= detectionDistance)
//            {
//                AStarNode startNode = aStarGrid.GetNodeFromWorld(transform.position);
//                AStarNode endNode = aStarGrid.GetNodeFromWorld(player.position);

//                // ��� ã��
//                currentPath = aStarGrid.pathfinder.CreatePath(startNode, endNode);

//                if (currentPath != null && currentPath.Count > 1)
//                {
//                    isChasingPlayer = true;
//                }
//                else
//                {
//                    isChasingPlayer = false;
//                }
//            }
//            else
//            {
//                isChasingPlayer = false;
//            }
//        }

//        if (isChasingPlayer)
//        {
//            MoveZombieAlongPath();
//        }
//    }

//    private void MoveZombieAlongPath()
//    {
//        if (currentPath != null && currentPath.Count > 1)
//        {
//            // ���� ��� �������� �̵�
//            Vector3 nextPosition = new Vector3(currentPath[1].xPos, currentPath[1].yPos, transform.position.z);
//            transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);

//            // ���� ��ġ�� ��ǥ�� ����� ������ ���� ���� �̵�
//            if (Vector3.Distance(transform.position, nextPosition) < 0.2f)
//            {
//                currentPath.RemoveAt(0);

//            }
//        }
//        else
//        {
//            isChasingPlayer = false;
//        }
//    }
//}
>>>>>>> parent of 1cfe001 (Revert "[ADD]아이템 및 좀비 생성 구현, 맵 바깥쪽 문 막기, 방 추가")
