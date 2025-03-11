using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Speed Imrovement", menuName = "Improvements Files/Speed Imrovement")]
public class SpeedImprovement : Improvement
{
    public override void ApplyImprovement(GameObject ObjectToUpgrade, float UpgradeValue)
    {
        PlayerMovement movementScript = ObjectToUpgrade.GetComponent<PlayerMovement>();

        movementScript.SetNewSpeed(UpgradeValue);
    }
}
