using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunData : ScriptableObject
{
    public Resource ammoType;
    public float damage;
    public float pelletsCount;
    public float spreadRadius;
    public float projectileSpeed;
    public float projectileExplosionRadius;
    public float range;
    public float fireRate;
    public int clipSize;
    public float reloadTime;
}
