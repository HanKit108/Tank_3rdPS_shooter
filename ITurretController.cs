using UnityEngine;

public interface ITurretController
{
    public Quaternion GetTurretRotation();
    public bool IsShoot();
}
