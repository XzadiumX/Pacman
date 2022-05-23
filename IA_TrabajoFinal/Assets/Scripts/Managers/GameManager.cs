using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Enemigos")]

    [SerializeField] List<FantasmaPadre> v_ghosts = new List<FantasmaPadre>();

    void Start()
    {
        
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //    RunAwayTime();

        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //    GoBack();
    }

    public void RunAwayTime()
    {
        for (int i = 0; i < v_ghosts.Count; i++)
        {
            v_ghosts[i].m_runningAway = true;

            v_ghosts[i].m_behaviourBeforeRunning = v_ghosts[i].m_behaviour;
            v_ghosts[i].m_behaviour = EnemyBehaviour.RunAway;

            v_ghosts[i].m_changeFinalDestination = true;
            v_ghosts[i].Repath();
        }
    }

    public void GoBack()
    {
        for (int i = 0; i < v_ghosts.Count; i++)
        {
            v_ghosts[i].m_runningAway = false;

            v_ghosts[i].m_behaviour = v_ghosts[i].m_behaviourBeforeRunning;

            v_ghosts[i].m_changeFinalDestination = true;
            v_ghosts[i].Repath();
        }
    }
}
