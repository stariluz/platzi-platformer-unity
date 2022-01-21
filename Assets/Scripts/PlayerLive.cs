using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;


public class PlayerLive : MonoBehaviour
{
    int lives=3;
    public Image[] hearts;
    public bool damageCooldown=false;
    SceneChanger changeScene;
    Animator playerAnimator;
    public AudioSource damagedAudio;
    public float cooldownTime=1f;
    Collider2D playerColider;
    Vector3 originalPosition;
    void Start(){
        changeScene=GameObject.Find("SceneChanger").GetComponent<SceneChanger>();
        playerAnimator=gameObject.GetComponent<Animator>();
        playerColider=GetComponent<Collider2D>();
        originalPosition=GetComponent<Transform>().position;
    }
    
    public void SubstractLives(){
        if(!damageCooldown){
            damagedAudio.Play();
            playerAnimator.SetBool("isDamaged", true);
            lives--;
            hearts[lives].gameObject.SetActive(false);
            if(lives>0){
                damageCooldown=true;
                StartCoroutine(Cooldown());
            }else{
                changeScene.ChangeSceneTo("LoseScene");
            }
        }
    }
    IEnumerator Cooldown(){
        yield return new WaitForSeconds(cooldownTime);
        damageCooldown=false;
        StopCoroutine(Cooldown());
        playerAnimator.SetBool("isDamaged", false);
    }
    
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag=="Fall"){
            transform.position=originalPosition;
            SubstractLives();
        }
    }
}
