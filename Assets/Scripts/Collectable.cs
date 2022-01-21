using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    public static int collectableQuantity=0;
    Text collectableText;
    ParticleSystem collectableParticle;
    AudioSource collectableAudio;
    Animator collectableAnimator;
    bool disappear=false;

    // Start is called before the first frame update
    void Start()
    {
        collectableQuantity=0;
        collectableText=GameObject.Find("DiamondCollectableQuantityText").GetComponent<Text>();
        collectableParticle=GameObject.Find("CollectableParticle").GetComponent<ParticleSystem>();
        collectableAudio=GetComponentInParent<AudioSource>();
        collectableAnimator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision){
        if(!disappear){
            if (collision.tag=="Player"){
                disappear=true;
                collectableAnimator.SetBool("Disappear", true);
                collectableParticle.transform.position=transform.position;
                collectableParticle.Play();
                collectableAudio.Play();
                collectableQuantity++;
                if(collectableQuantity<10){
                    collectableText.text="0"+collectableQuantity.ToString();
                }else{
                    collectableText.text=collectableQuantity.ToString();
                }
            }
        }
    }
    public void DisableObject(){
        gameObject.SetActive(false);
    }
}
