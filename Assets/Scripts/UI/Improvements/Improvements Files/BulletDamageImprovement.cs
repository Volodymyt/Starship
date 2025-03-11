using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Damage Improvement", menuName = "Improvements Files/Bullet Damage Improvement")]
public class BulletDamageImprovement : Improvement
{
    public override void ApplyImprovement(GameObject ObjectToUpgrade, float UpgradeValue)
    {
        Bullet movementScript = ObjectToUpgrade.GetComponent<Bullet>();

        movementScript.SetNewDamage(UpgradeValue);
    }
}
