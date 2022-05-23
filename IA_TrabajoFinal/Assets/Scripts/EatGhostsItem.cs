using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatGhostsItem : SpecialItem
{
    GameManager m_gm;

    // Start is called before the first frame update
    void Start()
    {
        m_gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ItemAction()
    {
        m_gm.RunAwayTime();

        Destroy(this.gameObject);
    }
}
