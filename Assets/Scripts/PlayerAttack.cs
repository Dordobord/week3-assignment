using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float atkCooldwn;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;
    private Animator anima;
    private PlayerMovement playerMovement;
    private float cooldwnTimer = Mathf.Infinity;

    private void Awake()
    {
        // REFERENCES
        anima = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && cooldwnTimer > atkCooldwn && playerMovement.canAttack()) //Delay on each shot
            Attack();
        
        cooldwnTimer += Time.deltaTime;

    }

    private void Attack()
    {
        anima.SetTrigger("attack");
        cooldwnTimer = 0;
        
        fireballs[0].transform.position = firepoint.position;
        fireballs[0].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
}
