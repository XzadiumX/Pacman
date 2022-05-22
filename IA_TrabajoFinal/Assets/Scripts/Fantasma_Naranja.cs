using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fantasma_Naranja : FantasmaPadre
{
    [Header("Patroll locations")]
    [SerializeField] List<GameObject> v_corners = new List<GameObject>();

    [SerializeField] List<Transform> v_wayPoints = new List<Transform>();

    public Transform m_currentWayPoint;

    private void Awake()
    {
        
    }

    protected new void Start()
    {
        GetWaypoints();

        base.Start();
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }

    public override void SpecialBehaviour()
    {
        int randomValue = Random.Range(0, v_wayPoints.Count);

        m_objective = v_wayPoints[randomValue].position;
        m_currentWayPoint = v_wayPoints[randomValue];

    }

    void GetWaypoints()
    {
        int randomValue = Random.Range(0, v_corners.Count);

        Transform[] waypoints = v_corners[randomValue].GetComponentsInChildren<Transform>();

        if (waypoints.Length > 0)
        {
            for (int i = 1; i < waypoints.Length; i++)
            {
                v_wayPoints.Add(waypoints[i]);
            }
        }
    }
}
