using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anima; 
    private BoxCollider2D boxCollider;
    private float horizontalInput;

    private void Awake()
    {
        // REFERENCES 
        body = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed , body.velocity.y);

        // Flip Character.
        if(horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if(horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1,1,1);

        if(Input.GetKey(KeyCode.Space) && isGrounded())
            Jump(); 

        anima.SetBool("run", horizontalInput != 0); // To know when the character is running it will do the animation.
        anima.SetBool("grounded", isGrounded());
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed); 
        anima.SetTrigger("jump");
        
    } 

    private bool isGrounded()
    {                                               // (origin, size, )
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {                                               // (origin, size, )
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall(); // only attack when colliding at the ground
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Powerup")
            Destroy(collision.gameObject);
            GetComponent<SpriteRenderer>().color = Color.blue;
            speed = 20f;
            StartCoroutine(ResetPower());
    }

    private IEnumerator ResetPower()
    {
        yield return new WaitForSeconds(5);
        speed = 10f;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
