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
    protected Vector3 m_posOnPathStart, m_objective, m_initialPos;
    public Transform m_player;
    public Node startNode { get; set; }
    public Node goalNode { get; set; }
    public ArrayList pathArray;
    protected float elapsedTime = 0.0f;
    //Interval time between pathfinding
    public float intervalTime = 1.0f;

    public float distanciaAlNodo;
    public Vector3 m_currentDestination;

    public bool m_changeFinalDestination = true;
    protected void Start()
    {
        pathArray = new ArrayList();
        m_initialPos = transform.position;
        FindPath();
    }

    protected void Update()
    {
        Movement();
    }

    protected virtual void ChooseBehaviour()
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

        m_changeFinalDestination = false;
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

    protected void Movement()
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

        float distanceToFinalObjective = Vector3.Distance(transform.position, m_objective);

        if(distanceToFinalObjective <= .3f)
        {
            m_changeFinalDestination = true;
        }

        if (distanciaAlNodo <= .1f)
        {
            ReachedDestination();

            if (pathArray.Count > 0)
            {
                pathArray.RemoveAt(0);

                if (pathArray.Count > 0)
                    m_currentDestination = ((Node)pathArray[0]).m_position;
            }
        }
    }

    protected virtual void ReachedDestination()
    {

    }

    protected void FindPath()
    {
        m_posOnPathStart = transform.position;

        if (m_changeFinalDestination)
            ChooseBehaviour();

        startNode = new Node(GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(m_posOnPathStart)));
        goalNode = new Node(GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(m_objective)));
        pathArray = AStar.FindPath(startNode, goalNode);

        if (m_currentDestination == Vector3.zero)
            m_currentDestination = ((Node)pathArray[0]).m_position;
    }

    protected void OnDrawGizmos()
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
