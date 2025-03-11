using UnityEngine;

[CreateAssetMenu(fileName = "Explosive Bullet Recharge Improvement", menuName = "Improvements Files/Explosive Bullet Recharge Improvement")]
public class ExplosiveBulletDamageImprovement : Improvement
{
    public override void ApplyImprovement(GameObject ObjectToUpgrade, float UpgradeValue)
    {
        PlayerShooting movementScript = ObjectToUpgrade.GetComponent<PlayerShooting>();

        movementScript.SetNewFollowBulletRechargeTime(UpgradeValue);
    }
}
