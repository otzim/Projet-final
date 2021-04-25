using UnityEngine;


public class EnnemyAttack : MonoBehaviour
{
    
    private float Distance;
    public Transform Target;
    public float chaseRange = 10;
    public float attackRange = 2.2f;
    public float attackRepeatTime = 1;
    private float attackTime;
    public float TheDammage;
    private UnityEngine.AI.NavMeshAgent agent;
    private float damage = 5f;

    public Animator animations;

    public GameObject enemy;

    private void Start()
    {
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        animations = gameObject.GetComponentInChildren<Animator>();
        attackTime = Time.time;
    }

    private void Update()
    {
        //agent.destination = Target.position;

        Target = GameObject.Find("Player").transform;

        Distance = Vector3.Distance(Target.position, transform.position);

        if(Distance > chaseRange)
        {
            idle();
        }

        if(Distance < chaseRange && Distance > attackRange)
        {
            chase();
        }

        if(Distance < attackRange)
        {
            attack();
        }

       

    }

    private void chase()
    {
        
        agent.destination = Target.position;
        animations.SetBool("walk", true);
        animations.SetBool("Idle", false);
           
    }

    private void attack()
    {
        agent.destination = transform.position;

        if (Time.time > attackTime)
        {
           
            Target.GetComponent<PlayerHealthController>().TakeDamage ((int)damage);
            Debug.Log("L'ennemi a envoyé" + TheDammage + "points de dégâts");
            attackTime = Time.time + attackRepeatTime;
            animations.SetTrigger("Attack");
            animations.SetBool("walk", false);
        }
    }

    private void idle()
    {
       
        animations.SetBool("walk", false);
        animations.SetBool("Idle", true);

    }

}
