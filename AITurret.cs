using UnityEngine;

public class AITurret : MonoBehaviour, ITurretController
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform attackPoint;

    [SerializeField] private float attackDistance = 60;
    [SerializeField] private bool isActive = true;
    
    public Quaternion GetTurretRotation()
    {
        if(isActive)
        {
            Vector3 targetDirection = _target.position - transform.position;
    
            float barelIncline = (attackDistance - targetDirection.magnitude) / attackDistance * 8 + 9;

            Quaternion target = Quaternion.LookRotation(targetDirection, Vector3.up);
            target.eulerAngles = new Vector3(barelIncline, target.eulerAngles.y, 0);
            return target;
        }
        return transform.rotation;
    }

    public bool IsShoot()
    {
        if(isActive)
        {
            RaycastHit hit;
            Vector3 direction = _target.position - attackPoint.position;
            Vector3 barrelIncline = attackPoint.forward;

            float angle = Vector3.Angle(Vector3.ProjectOnPlane(direction, attackPoint.right), attackPoint.forward);

            barrelIncline = Quaternion.AngleAxis(angle, attackPoint.right) * barrelIncline;

            if(Physics.Raycast(attackPoint.position, barrelIncline.normalized, out hit, attackDistance) 
                && hit.transform.gameObject.tag == "Player" )
            {
                Debug.DrawRay(attackPoint.position, barrelIncline.normalized * hit.distance, Color.red);
                return true;
            }
            else
            {
                Debug.DrawRay(attackPoint.position, barrelIncline.normalized * hit.distance, Color.green);
            }
        }
        return false;
    }
}
