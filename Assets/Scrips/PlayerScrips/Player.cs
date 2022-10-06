using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    public GameObject fxSmoke;
    #region Variable
    [Header("1-OBJECT")]
    public Joystick joystick; //joystick
    public bool x2Jump;
    [Space]
    public Rigidbody2D rb;
    [HideInInspector] public Animator anim;

    private BoxCollider2D Collider;
    public enum State { idle, running, jumping, falling, hurt }
    public State state = State.idle;
    

    //Imspector variable : bien co the tinh chinh
    [SerializeField] private LayerMask ground;
    [Space]
    [Header("2-SPEED")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 25f;
    [SerializeField] private const float hurtForce = 18f;

    [Space]
    //Audio
    [Header("3-AUDIO")]
    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioSource jump;
    [SerializeField] private AudioSource death;
    [SerializeField] private AudioSource up;
    [SerializeField] private AudioSource Hurt;

    private bool m_FacingRight;
    #endregion //bien khoi tao //bien khoi tao

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Collider = GetComponent<BoxCollider2D>();
        footstep = GetComponent<AudioSource>();

        Application.targetFrameRate = Screen.currentResolution.refreshRate;
        //Application.targetFrameRate = 90;
    }
    private void Start()
    {
        ManagerUI.perm.healthAmount.text = ManagerUI.perm.health.ToString();
    }
    private void Update()
    {
        if (state != State.hurt)
        {
            Movement();
        }
        AnimationState();
        anim.SetInteger("state", (int)state); //set animation trong enum state : dat cac trang thai trong enum animation
    }
    private IEnumerator ResetPower()// wait 10s return entry
    {
        yield return new WaitForSeconds(10);
        jumpForce-=3;
        //speed = 3.3f;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    #region TOUCH
    //Touch Tag
    private void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.CompareTag("Conllectable"))// play sound + destroy + 1 Mr + dispaly Mr
        {
            ManagerUI.perm.score += 10;
            ManagerUI.perm.scoreText.text = ManagerUI.perm.score.ToString();
            Cherry destroy = collison.gameObject.GetComponent<Cherry>();
            destroy.Touch();
            //cherry.Play();
            //Destroy(collison.gameObject);

        }
        if (collison.CompareTag("Up")) // if collison destroy + jumforce  10 -> 35 , change color -> red ;
        {
            up.Play();
            Destroy(collison.gameObject);
            ManagerUI.perm.health += 1;
            ManagerUI.perm.healthAmount.text = ManagerUI.perm.health.ToString();
        }
        if (collison.tag == "Acorn")
        {
            Destroy(collison.gameObject);
            jumpForce += 3;
            //speed = 1f;
            GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(ResetPower());
        }
    }
    //Touch Enemy
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (state == State.falling)
            {
                enemy.JumpOn();
                Jump();
            }
            else
            {
                {
                    state = State.hurt;
                    HandleHeath(); // -heath , display heath , reset heath if = 0 
                    if (other.gameObject.transform.position.x > transform.position.x)
                    {
                        rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                    }
                    else
                    {
                        rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                    }
                }
            }
        }
        //Touch Bullet
        if (other.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            bullet.OnBullet();
            state = State.hurt;
            HandleHeath();

            if (other.gameObject.transform.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(hurtForce, rb.velocity.y);
            }
        }
        if (other.gameObject.CompareTag("Trap"))
        {
            state = State.hurt;
            HandleHeath();
            if (other.gameObject.transform.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(hurtForce, rb.velocity.y);
            }

        }
    }
    #endregion
    private void HandleHeath()
    {
        ManagerUI.perm.health -= 1;
        ManagerUI.perm.healthAmount.text = ManagerUI.perm.health.ToString();
        if (ManagerUI.perm.health <= 0)
        {
            anim.SetTrigger("Death");
            rb.velocity = Vector2.zero;
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
        }
    }
    private void Death()
    {
        ManagerUI.perm.Reset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        transform.Rotate(0f, 180f, 0f);
    }
    #region Move
    [HideInInspector] public float hDirection;
    private bool Jumpx2;

    private void Movement() //Move
    {
        hDirection = CrossPlatformInputManager.GetAxis("Horizontal");

        rb.velocity = new Vector2(hDirection*speed, rb.velocity.y);
        
        //Moving Left
        if (hDirection < 0 && !m_FacingRight)
        {
            Flip();
        }
        //Moving Right
        else if (hDirection > 0 && m_FacingRight)
        {
            Flip();
        }
        if (CrossPlatformInputManager.GetButton("Jump"))
        {
            if (Collider.IsTouchingLayers(ground))
            {
                Jump();
                Jumpx2 = true;
            }
            else
            {
                //Jumpx2
                if (Jumpx2 && x2Jump)
                {
                    Jumpx2 = false;
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    Jumpx2x();
                }
            }
        }

        if (Input.GetButton("Jump"))
        {
            
            if (Collider.IsTouchingLayers(ground))
            {
                Jump();
                Jumpx2 = true;
            }
            else
            {
                //Jumpx2
                if (Jumpx2 && x2Jump)
                {
                    Jumpx2 = false;
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    Jumpx2x();
                }
            }
        }  
    }
    private void Jumpx2x()
    {
        rb.velocity = new Vector2(rb.velocity.x, 18f);
        state = State.jumping;
    }


    #endregion
    //Jump
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
        Instantiate(fxSmoke, transform.position, Quaternion.identity);
    }
    #region Animation
    //Animation 
    private void AnimationState()
    {
       if (state == State.jumping)
        {
            if(rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if(state == State.falling)
        {
            if (Collider.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if(state == State.hurt)
        {
            if(Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }
    #endregion
    #region Audio
    private void Footstep() // footstep play 
    {
        footstep.Play();
    }
    private void deading() // jump play 
    {
        this.death.Play();
    }
    private void Jumping() // jump play 
    {
        this.jump.Play();
    }
    private void Hurting()
    {
        this.Hurt.Play();
    }
    #endregion

    

}
