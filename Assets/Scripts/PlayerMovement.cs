using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerRb;
    float movementHorizontal;
    public float speed=500;
    public float jumpForce=300;
    public Collider2D ground;
    public Animator playerAnimator;
    public AudioSource jumpAudio, walkAudio;
    RaycastHit2D hit;
    public float height,width;
    // Start is called before the first frame update
    void Start()
    {
        playerRb=GetComponent<Rigidbody2D>();
        ground=GameObject.Find("Ground").GetComponent<Collider2D>();
        height=GetComponent<Collider2D>().bounds.extents.y;
        width=GetComponent<Collider2D>().bounds.extents.x;
        Physics2D.queriesHitTriggers=false;
        // jumpAudio=GetComponent<AudioSource>();
        // walkAudio=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        movementHorizontal= Input.GetAxis("Horizontal");
        movementHorizontal+=CrossPlatformInputManager.GetAxis("Horizontal");
        playerRb.velocity= new Vector2(
            (movementHorizontal*speed), playerRb.velocity.y
        );
        if(movementHorizontal==0){
            playerAnimator.SetBool("isWalking", false);
            StopAudio(walkAudio);
        }else if(movementHorizontal>0){
            GetComponent<SpriteRenderer>().flipX=false;
            playerAnimator.SetBool("isWalking", true);
            PlayerWalking();
        }else if(movementHorizontal<0){
            GetComponent<SpriteRenderer>().flipX=true;
            playerAnimator.SetBool("isWalking", true);
            PlayerWalking();
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            PlayerJump();
        }
    }
    public void PlayerJump(){
        if(PlayerIsOnGround()){
            StopAudio(walkAudio);
            jumpAudio.Play();
            playerRb.AddForce(Vector2.up*jumpForce);
            playerAnimator.SetBool("isJumping",true);
        }
    }
    public bool PlayerIsOnGround(){
        int layer=1<<LayerMask.NameToLayer("Ground");
        bool groundCheck1 = Physics2D.Raycast(transform.position, -Vector2.up, height+0.1f, layer);

        bool groundCheck2 = Physics2D.Raycast(new Vector2(
        transform.position.x + (width - 0.05f),
        transform.position.y), -Vector2.up, height+0.1f, layer);

        bool groundCheck3 = Physics2D.Raycast(new Vector2(
        transform.position.x - (width - 0.05f),
        transform.position.y), -Vector2.up, height+0.1f, layer);

        return (groundCheck1||groundCheck2||groundCheck3);
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag=="Ground"){
            PlayerWalking();
        }
    }
    bool PlayerWalking(){
        if(PlayerIsOnGround()){
            playerAnimator.SetBool("isJumping",false);
            if(movementHorizontal!=0){
                playerAnimator.SetBool("isWalking",true);
            }else{
                playerAnimator.SetBool("isWalking",false);
            }
            PlayAudio(walkAudio);
            return true;
        }else{
            StopAudio(walkAudio);
            return false;
        }
    }
    void PlayAudio(AudioSource audio){
        if(!audio.isPlaying){
            audio.Play();
        }
    }
    void StopAudio(AudioSource audio){
        if(audio.isPlaying){
            audio.Stop();
        }
    }
}
