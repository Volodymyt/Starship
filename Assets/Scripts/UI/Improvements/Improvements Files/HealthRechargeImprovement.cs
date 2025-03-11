using UnityEngine;


[CreateAssetMenu(fileName = "HP Recharge Imrovement", menuName = "Improvements Files/HP Recharge Imrovement")]
public class HealthRechargeImprovement : Improvement
{
    public override void ApplyImprovement(GameObject ObjectToUpgrade, float UpgradeValue)
    {
        Heals healthScript = ObjectToUpgrade.GetComponent<Heals>();

        healthScript.SetNewRechargeSpeed(UpgradeValue);
    }
}
