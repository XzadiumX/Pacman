using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWallsItem : SpecialItem
{
    public override void ItemAction()
    {
        //- Hacer q pacman rompa paredes con su script de mov
        // + Tal vez metiendo

        Destroy(this.gameObject);
    }
}
