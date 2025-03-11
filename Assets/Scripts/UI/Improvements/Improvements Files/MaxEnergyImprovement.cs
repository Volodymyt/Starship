using UnityEngine;

[CreateAssetMenu(fileName = "Max Energy Imrovement", menuName = "Improvements Files/Max Energy Imrovement")]
public class MaxEnergyImprovement : Improvement
{
    public override void ApplyImprovement(GameObject ObjectToUpgrade, float UpgradeValue)
    {
        PlayerMovement healthScript = ObjectToUpgrade.GetComponent<PlayerMovement>();

        healthScript.SetNewMaxEnergy(UpgradeValue);
    }
}
