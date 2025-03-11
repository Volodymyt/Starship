using UnityEngine;

[CreateAssetMenu(fileName = "Laser Recharge Improvement", menuName = "Improvements Files/Laser Recharge Improvement")]
public class LaserRechargeImprovement : Improvement
{
    public override void ApplyImprovement(GameObject ObjectToUpgrade, float UpgradeValue)
    {
        Laser movementScript = ObjectToUpgrade.GetComponent<Laser>();

        movementScript.SetNewRechargeTime(UpgradeValue);
    }
}
