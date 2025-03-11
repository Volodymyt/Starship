using UnityEngine;

[CreateAssetMenu(fileName = "Laser Damage Improvement", menuName = "Improvements Files/Laser Damage Improvement")]
public class LaserDamageImprovement : Improvement
{
    public override void ApplyImprovement(GameObject ObjectToUpgrade, float UpgradeValue)
    {
        Laser movementScript = ObjectToUpgrade.GetComponent<Laser>();

        movementScript.SetNewDamage(UpgradeValue);
    }
}
