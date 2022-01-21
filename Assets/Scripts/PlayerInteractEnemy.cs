using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractEnemy : MonoBehaviour
{
    
    float height;
    float width;
    public float space=2f;
    public float rebound=10f;
    public float pushX=10f, pushY=10f;
    Rigidbody2D playerRb;
    public LayerMask layers;
    // Start is called before the first frame update
    void Start()
    {
        height=GetComponentInParent<PlayerMovement>().height;
        width=GetComponentInParent<PlayerMovement>().width;
        Physics2D.queriesHitTriggers=false;
        playerRb=GetComponentInParent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag=="Enemy"&&!collision.GetComponent<EnemyBehavior>().enemyIsDead){
            ///Debug.Log(("Touch enemy:"));
            if(PlayerIsOnEnemy()){
                ///Debug.Log(("Up"));
                playerRb.velocity=new Vector2(playerRb.velocity.y,rebound);
                collision.GetComponent<EnemyBehavior>().PlayerCollision();
            }else{
                ///Debug.Log(("Damage"));
                ForceToDamage(collision);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision){
        if(collision.tag=="Enemy"&&!collision.GetComponent<EnemyBehavior>().enemyIsDead){
            ForceToDamage(collision);
        }
    }
    void ForceToDamage(Collider2D enemy){
        if(!GetComponentInParent<PlayerLive>().damageCooldown){
            if(playerRb.velocity.x==0){
                if(enemy.GetComponent<Rigidbody2D>().velocity.x<0){
                    playerRb.velocity=new Vector2(-pushX, pushY);
                }else{
                    playerRb.velocity=new Vector2(pushX, pushY);
                }
            }else{
                if(playerRb.velocity.x<0){
                    playerRb.velocity=new Vector2(pushX, pushY);
                }else{
                    playerRb.velocity=new Vector2(-pushX, pushY);
                }
            }
            GetComponentInParent<PlayerLive>().SubstractLives();
        }
    }
    public bool PlayerIsOnEnemy(){
        bool Check1 = Physics2D.Raycast(new Vector2(
        transform.position.x,
        transform.position.y - height), Vector2.down, space, layers);
        
        bool Check2 = Physics2D.Raycast(new Vector2(
        transform.position.x + (width-0.1f),
        transform.position.y - height), Vector2.down, space, layers);

        bool Check3 = Physics2D.Raycast(new Vector2(
        transform.position.x + (width+0.1f),
        transform.position.y - height), Vector2.down, space, layers);
        
        bool Check4 = Physics2D.Raycast(new Vector2(
        transform.position.x - (width-0.1f),
        transform.position.y - height), Vector2.down, space, layers);

        bool Check5 = Physics2D.Raycast(new Vector2(
        transform.position.x - (width+0.1f),
        transform.position.y - height), Vector2.down, space, layers);

        return (Check1||Check2||Check3||Check4||Check5);
    }
}
