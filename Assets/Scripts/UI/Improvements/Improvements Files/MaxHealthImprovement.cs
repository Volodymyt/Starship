using UnityEngine;

[CreateAssetMenu(fileName = "Max HP Imrovement", menuName = "Improvements Files/Max HP Imrovement")]
public class MaxHealthImprovement : Improvement
{
    public override void ApplyImprovement(GameObject ObjectToUpgrade, float UpgradeValue)
    {
        Heals healthScript = ObjectToUpgrade.GetComponent<Heals>();

        healthScript.SetNewMaxHealth(UpgradeValue);
    }
}
