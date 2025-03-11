using UnityEngine;

[CreateAssetMenu(fileName = "Explosive Damage Improvement", menuName = "Improvements Files/Explosive Damage Improvement")]
public class ExplosiveDamageImprovement : Improvement
{
    public override void ApplyImprovement(GameObject ObjectToUpgrade, float UpgradeValue)
    {
        PlayerFollovingBullet movementScript = ObjectToUpgrade.GetComponent<PlayerFollovingBullet>();

        movementScript.SetNewDamage(UpgradeValue);
    }
}
