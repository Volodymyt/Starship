using UnityEngine;

[CreateAssetMenu(fileName = "Energy Recharge Imrovement", menuName = "Improvements Files/Energy Recharge Imrovement")]
public class EnergyRechargeImprovement : Improvement
{
    public override void ApplyImprovement(GameObject ObjectToUpgrade, float UpgradeValue)
    {
        PlayerMovement healthScript = ObjectToUpgrade.GetComponent<PlayerMovement>();

        healthScript.SetNewEnergeRechargeSpeed(UpgradeValue);
    }
}
