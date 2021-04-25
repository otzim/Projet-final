using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private int hitRange = 1;
    private int health = 100;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
            Debug.Log("Attack");
        }
    }
   private void Attack()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 origin = transform.position;

        Debug.DrawRay(origin, forward * hitRange, Color.red, .2f);

        if (Physics.Raycast(origin, forward, out hit, hitRange))
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<EnemyHealth>().TakeDamage(30);
            }
        }

    }

}
