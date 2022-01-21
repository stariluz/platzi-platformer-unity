using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Rigidbody2D enemyRb;
    Animator enemyAnimator;
    Transform enemyTransform;
    public float speed=1;
    public SpriteRenderer enemySR;
    Vector3 enemyPosition;
    ParticleSystem enemyParticle;
    AudioSource enemyAudio;
    Collider2D enemyCollider, playerCollider;
    float height;
    float width;
    public LayerMask layers;
    public bool enemyIsDead;
    // Start is called before the first frame update
    void Start()
    {   
        enemyTransform=GetComponent<Transform>();
        enemyPosition=enemyTransform.position;
        enemyRb=GetComponent<Rigidbody2D>();
        enemySR=GetComponent<SpriteRenderer>();
        enemyAnimator=GetComponent<Animator>();
        enemyParticle=GameObject.Find("EnemyParticle").GetComponent<ParticleSystem>();
        enemyAudio=GetComponentInParent<AudioSource>();
        enemyCollider=GetComponent<Collider2D>();
        playerCollider=GameObject.Find("Player").GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(enemyCollider, playerCollider);

        height=GetComponent<Collider2D>().bounds.extents.y;
        width=GetComponent<Collider2D>().bounds.extents.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enemyRb.velocity= new Vector2(speed, enemyRb.velocity.y);
        if(enemySR.flipX){
            if(MustTurnRight()){
                enemySR.flipX=!enemySR.flipX;
                speed*=-1;
            }
        }else {
            if(MustTurnLeft()){
                enemySR.flipX=!enemySR.flipX;
                speed*=-1;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag=="Enemy"){
            enemySR.flipX=!enemySR.flipX;
            speed*=-1;
        }
    }
    public void PlayerCollision(){
        enemyIsDead=true;
        enemyAudio.Play();
        enemyParticle.transform.position=transform.position;
        enemyParticle.Play();
        enemyAnimator.SetBool("isDead",true);
    }
    bool MustTurnLeft(){
        bool Check1 = Physics2D.Raycast(transform.position, Vector2.right, width+0.1f, layers);
        
        bool Check2 = Physics2D.Raycast(new Vector2(transform.position.x+width, transform.position.y+(height-0.1f)), Vector2.right, 0.1f, layers);

        bool Check3 = Physics2D.Raycast(new Vector2(transform.position.x+(width+0.2f), transform.position.y-height), Vector2.down, 3f, layers);

        ///Debug.Log((Check1, Check2, !Check3));
        return (Check1||Check2||!Check3);
    }
    bool MustTurnRight(){
        bool Check1 = Physics2D.Raycast(transform.position, Vector2.left, width+0.1f, layers);
        
        bool Check2 = Physics2D.Raycast(new Vector2(transform.position.x-width, transform.position.y+(height-0.1f)), Vector2.left, 0.1f, layers);

        bool Check3 = Physics2D.Raycast(new Vector2(transform.position.x-(width+0.2f), transform.position.y-height), Vector2.down, 3f, layers);

        ///Debug.Log((Check1, Check2, !Check3));
        return (Check1||Check2||!Check3);
    }
    
    public void DisableObject(){
        gameObject.SetActive(false);
        enemyTransform.position=enemyPosition;
        enemyTransform.rotation=Quaternion.Euler(0,0,0);
    }
}
