using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Recharge Improvement", menuName = "Improvements Files/Bullet Recharge Improvement")]
public class BulletRechargeImprovement : Improvement
{
    public override void ApplyImprovement(GameObject ObjectToUpgrade, float UpgradeValue)
    {
        PlayerShooting[] Guns = ObjectToUpgrade.GetComponentsInChildren<PlayerShooting>();

        for (int i = 0; i < Guns.Length; i++)
        {
            if (Guns[i].gameObject.name != "Player")
            {
                Guns[i].SetNewRechargeTime(UpgradeValue);
            }
        }
    }
}
