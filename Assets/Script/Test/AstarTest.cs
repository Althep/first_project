using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

[System.Serializable]
public class Node
{
    public int x, y, G, H;
    public bool isOpen;
    public Node ParentNode;
    // G : �������κ��� �̵��ߴ� �Ÿ�, H : |����|+|����| ��ֹ� �����Ͽ� ��ǥ������ �Ÿ�, F : G + H
    public Node(bool _isOpen, int _x, int _y) { isOpen = _isOpen; x = _x; y = _y; }
    public int F { get { return G + H; } }
}
public class AstarTest : MonoBehaviour
{
    [SerializeField] List<Node> OpenList;
    [SerializeField] List<Node> ClosedList;
    [SerializeField] public List<Node> FinalNodeList;
    [SerializeField] Node[,] NodeArray;
    List<GameObject> OtherObjList;
    FieldManager fieldManager;
    MonsterState monsterState;
    Node StartNode, TargetNode, CurNode;
    public Vector2Int bottomLeft, topRight, startPos, targetPos;
    int sizeX, sizeY;
    public bool isFindPath = false;
    [SerializeField] bool reFind = false;
    private void Awake()
    {
        fieldManager = GameObject.Find("FieldManager").GetComponent<FieldManager>();
        monsterState = this.gameObject.transform.GetComponent<MonsterState>();
    }

    public void PathFinding(Vector2 TargetObjPos)
    {
        var pos = TargetObjPos;
        targetPos = new Vector2Int((int)pos.x, (int)pos.y);
        pos = transform.position;
        startPos = new Vector2Int((int)pos.x, (int)pos.y);

        if (startPos.x > targetPos.x)
        {
            sizeX = (int)startPos.x - (int)targetPos.x + 1;
            bottomLeft.x = (int)targetPos.x;
            topRight.x = (int)startPos.x;
        }
        else
        {
            sizeX = (int)targetPos.x - (int)startPos.x + 1;
            bottomLeft.x = (int)startPos.x;
            topRight.x = (int)targetPos.x;
        }

        if (startPos.y > targetPos.y)
        {
            sizeY = (int)startPos.y - (int)targetPos.y + 1;
            bottomLeft.y = (int)targetPos.y;
            topRight.y = (int)startPos.y;
        }
        else
        {
            sizeY = (int)targetPos.y - (int)startPos.y + 1;
            bottomLeft.y = (int)startPos.y;
            topRight.y = (int)targetPos.y;
        }

        NodeArray = new Node[sizeX, sizeY];

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                bool isWall = false;
                foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(i + bottomLeft.x, j + bottomLeft.y), 0.4f))
                    if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Monster") isWall = true;

                NodeArray[i, j] = new Node(isWall, i + bottomLeft.x, j + bottomLeft.y);
            }
        }
        // ���۰� �� ���, ��������Ʈ�� ��������Ʈ, ����������Ʈ �ʱ�ȭ
        StartNode = NodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
        TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];

        OpenList = new List<Node>() { StartNode };
        ClosedList = new List<Node>();
        FinalNodeList = new List<Node>();
        while (OpenList.Count > 0)
        {
            // ��������Ʈ �� ���� F�� �۰� F�� ���ٸ� H�� ���� �� ������� �ϰ� ��������Ʈ���� ��������Ʈ�� �ű��
            CurNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
                if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H) CurNode = OpenList[i];

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);

            // ������
            if (CurNode == TargetNode)
            {
                Node TargetCurNode = TargetNode;
                while (TargetCurNode != StartNode)
                {
                    FinalNodeList.Add(TargetCurNode);
                    TargetCurNode = TargetCurNode.ParentNode;
                }
                FinalNodeList.Add(StartNode);
                FinalNodeList.Reverse();
                isFindPath = true;

                //for (int i = 0; i < FinalNodeList.Count; i++) print(i + "��°�� " + FinalNodeList[i].x + ", " + FinalNodeList[i].y);
                //StartCoroutine(EnemyMove());
                return;
            }
            // �� �� �� ��
            OpenListAdd(CurNode.x, CurNode.y + 1);
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y);
            OpenListAdd(CurNode.x + 1, CurNode.y + 1);
            OpenListAdd(CurNode.x - 1, CurNode.y + 1);
            OpenListAdd(CurNode.x + 1, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y - 1);
        }
    }
    void FindOtherObjForPath()
    {

        while (!isFindPath)
        {
            List<GameObject> FindObj = new List<GameObject>();
            Vector2 reObjPos;
            int temp = 0;
            OtherObjList.AddRange(GameObject.FindGameObjectsWithTag("Monster"));

            for (int i = 0; i < OtherObjList.Count; i++)
            {
                if (OtherObjList[i].GetComponent<AstarTest>().FinalNodeList != null)
                {
                    FindObj.Add(OtherObjList[i]);
                }
            }
            for (int i = 0; i < FindObj.Count; i++)
            {
                if (Vector2.Distance(FindObj[i].transform.position, targetPos) < Vector2.Distance(FindObj[temp].transform.position, targetPos))
                {
                    temp = i;
                }
            }
            reObjPos = FindObj[temp].transform.position;
            FindObj.RemoveAt(temp);
            PathFinding(reObjPos);
        }
    }

    void OpenListAdd(int checkX, int checkY)
    {
        // �����¿� ������ ����� �ʰ�, ���� �ƴϸ鼭, ��������Ʈ�� ���ٸ�
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1
            && !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isOpen && !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
        {
            // �̿���忡 �ְ�, ������ 10, �밢���� 14���
            Node NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
            int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);


            // �̵������ �̿����G���� �۰ų� �Ǵ� ��������Ʈ�� �̿���尡 ���ٸ� G, H, ParentNode�� ���� �� ��������Ʈ�� �߰�
            if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
            {
                NeighborNode.G = MoveCost;
                NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y)) * 10;
                NeighborNode.ParentNode = CurNode;

                OpenList.Add(NeighborNode);
            }
        }
    }
    void ReFindPath(Vector2 TargetObjPos)
    {
        reFind = true;
        if (FinalNodeList.Count == 0)
        {
            Vector3 pos = TargetObjPos;
            targetPos = new Vector2Int((int)pos.x, (int)pos.y);
            pos = transform.position;
            startPos = new Vector2Int((int)pos.x, (int)pos.y);
            if (startPos.x > targetPos.x)
            {
                sizeX = (int)startPos.x - (int)targetPos.x + 5;
                bottomLeft.x = (int)targetPos.x - 2;
                topRight.x = (int)startPos.x + 2;
            }
            else
            {
                sizeX = (int)targetPos.x - (int)startPos.x + 5;
                bottomLeft.x = (int)startPos.x - 2;
                topRight.x = (int)targetPos.x + 2;
            }

            if (startPos.y > targetPos.y)
            {
                sizeY = (int)startPos.y - (int)targetPos.y + 5;
                bottomLeft.y = (int)targetPos.y - 2;
                topRight.y = (int)startPos.y + 2;
            }
            else
            {
                sizeY = (int)targetPos.y - (int)startPos.y + 5;
                bottomLeft.y = (int)startPos.y - 2;
                topRight.y = (int)targetPos.y + 2;
            }

            NodeArray = new Node[sizeX, sizeY];

            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    bool isWall = false;
                    foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(i + bottomLeft.x, j + bottomLeft.y), 0.4f))
                        if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Monster") isWall = true;

                    NodeArray[i, j] = new Node(isWall, i + bottomLeft.x, j + bottomLeft.y);
                }
            }
            // ���۰� �� ���, ��������Ʈ�� ��������Ʈ, ����������Ʈ �ʱ�ȭ
            StartNode = NodeArray[startPos.x - bottomLeft.x + 2, startPos.y - bottomLeft.y + 2];
            TargetNode = NodeArray[targetPos.x - bottomLeft.x + 2, targetPos.y - bottomLeft.y + 2];

            OpenList = new List<Node>() { StartNode };
            ClosedList = new List<Node>();
            FinalNodeList = new List<Node>();
            while (OpenList.Count > 0)
            {
                // ��������Ʈ �� ���� F�� �۰� F�� ���ٸ� H�� ���� �� ������� �ϰ� ��������Ʈ���� ��������Ʈ�� �ű��
                CurNode = OpenList[0];
                for (int i = 1; i < OpenList.Count; i++)
                    if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H) CurNode = OpenList[i];

                OpenList.Remove(CurNode);
                ClosedList.Add(CurNode);

                // ������
                if (CurNode == TargetNode)
                {
                    Node TargetCurNode = TargetNode;
                    while (TargetCurNode != StartNode)
                    {
                        FinalNodeList.Add(TargetCurNode);
                        TargetCurNode = TargetCurNode.ParentNode;
                    }
                    FinalNodeList.Add(StartNode);
                    FinalNodeList.Reverse();
                    isFindPath = true;

                    //for (int i = 0; i < FinalNodeList.Count; i++) print(i + "��°�� " + FinalNodeList[i].x + ", " + FinalNodeList[i].y);
                    //StartCoroutine(EnemyMove());
                    return;
                }
                // �� �� �� ��
                OpenListAdd(CurNode.x, CurNode.y + 1);
                OpenListAdd(CurNode.x + 1, CurNode.y);
                OpenListAdd(CurNode.x, CurNode.y - 1);
                OpenListAdd(CurNode.x - 1, CurNode.y);
                OpenListAdd(CurNode.x + 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y + 1);
                OpenListAdd(CurNode.x + 1, CurNode.y - 1);
                OpenListAdd(CurNode.x - 1, CurNode.y - 1);
            }
        }
    }

    void OnDrawGizmos()
    {
        if (FinalNodeList.Count != 0)
        {
            for (int i = 0; i < FinalNodeList.Count - 1; i++)
                Gizmos.DrawLine(new Vector2(FinalNodeList[i].x, FinalNodeList[i].y), new Vector2(FinalNodeList[i + 1].x, FinalNodeList[i + 1].y));
        }
    }

    IEnumerator EnemyMove()
    {
        while (FinalNodeList.Count > 1)
        {
            Vector3 pos = new Vector3(FinalNodeList[0].x, FinalNodeList[0].y, 0);

            float dist = Vector3.Distance(pos, transform.position);

            transform.position = pos;
            yield return new WaitForSeconds(0.5f);
            FinalNodeList.RemoveAt(0);
        }
    }
}