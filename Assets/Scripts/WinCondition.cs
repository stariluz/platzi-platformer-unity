using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{
    GameObject winScreen,gameScreen,player;
    GameObject mobileControls;

    void Start(){
        winScreen=GameObject.Find("WinCanvas");
        winScreen.SetActive(false);
        gameScreen=GameObject.Find("GameCanvas");
        player=GameObject.Find("Player");
        mobileControls=GameObject.Find("MobileSingleStickControl");
    }
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag=="Player"){
            gameScreen.SetActive(false);
            winScreen.SetActive(true);
            player.SetActive(false);
            mobileControls.SetActive(false);
        }
    }
}
