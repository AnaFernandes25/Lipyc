using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/PerderVida")]
public class PerderVida : PowerupEffect
{
    public float amount;
    public override void Apply(GameObject target)
    {
        //target.GetComponent<Player>().vidasIniciais.value += amount;
    }

}
