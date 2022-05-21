using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyBehaviour
{
    Agressive,
    RunAway,
    Special
}

public class FantasmaPadre : MonoBehaviour
{
    [Header("Behaviour")]
    public EnemyBehaviour m_behaviour;

    [Header("Pathfinding")]
    private Vector3 m_posOnPathStart, m_objective, m_initialPos;
    public Transform m_player;
    public Node startNode { get; set; }
    public Node goalNode { get; set; }
    public ArrayList pathArray;
    private float elapsedTime = 0.0f;
    //Interval time between pathfinding
    public float intervalTime = 1.0f;

    public float distanciaAlNodo;
    Vector3 m_currentDestination;

    void Start()
    {
        pathArray = new ArrayList();
        m_initialPos = transform.position;
        FindPath();
    }

    void Update()
    {
        Movement();
    }

    protected void ChooseBehaviour()
    {
        switch (m_behaviour)
        {
            case EnemyBehaviour.Agressive:
                AgressiveBehaviour();
                break;
            case EnemyBehaviour.RunAway:
                RunAwayBehaviour();
                break;
            case EnemyBehaviour.Special:
                SpecialBehaviour();
                break;
            default:
                break;
        }
    }

    public virtual void AgressiveBehaviour()
    {
        m_objective = m_player.position;
    }

    public virtual void RunAwayBehaviour()
    {
        m_objective = m_initialPos;
    }

    public virtual void SpecialBehaviour()
    {
        
    }

    private void Movement()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= intervalTime)
        {
            elapsedTime = 0.0f;
            FindPath();
        }

        if (m_currentDestination != Vector3.zero)
        {
            distanciaAlNodo = Vector3.Distance(transform.position, m_currentDestination);

            transform.position = Vector3.MoveTowards(transform.position, m_currentDestination, 4 * Time.deltaTime);
        }


        if (distanciaAlNodo <= .1f)
        {
            pathArray.RemoveAt(0);
            m_currentDestination = ((Node)pathArray[0]).m_position;
        }
    }

    void FindPath()
    {
        m_posOnPathStart = transform.position;
        ChooseBehaviour();

        startNode = new Node(GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(m_posOnPathStart)));
        goalNode = new Node(GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(m_objective)));
        pathArray = AStar.FindPath(startNode, goalNode);

        if (m_currentDestination == null)
            m_currentDestination = ((Node)pathArray[0]).m_position;
    }

    void OnDrawGizmos()
    {
        if (pathArray == null)
            return;

        if (pathArray.Count > 0)
        {
            int index = 1;
            foreach (Node node in pathArray)
            {
                if (index < pathArray.Count)
                {
                    Node nextNode = (Node)pathArray[index];
                    Debug.DrawLine(node.m_position, nextNode.m_position,
                    Color.green);
                    index++;
                }
            }
        }
    }
}
