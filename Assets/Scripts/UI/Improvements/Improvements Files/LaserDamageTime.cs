using UnityEngine;

[CreateAssetMenu(fileName = "Laser Damage Time Improvement", menuName = "Improvements Files/Laser Damage Time Improvement")]
public class LaserDamageTime : Improvement
{
    public override void ApplyImprovement(GameObject ObjectToUpgrade, float UpgradeValue)
    {
        PlayerShooting movementScript = ObjectToUpgrade.GetComponent<PlayerShooting>();

        movementScript.SetNewLaserRechargeTime(UpgradeValue);
    }
}
