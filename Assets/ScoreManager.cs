using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] GameObject E1;
    [SerializeField] GameObject E2;
    [SerializeField] GameObject E3;
    [SerializeField] GameObject E4;
    [SerializeField] GameObject E5;
    int TotalScore=0;
    int Lives=0;
    [SerializeField] Text Score;
    // Start is called before the first frame update
    void Start()
    {
     Score.text = "Score: " + TotalScore.ToString();
    }

    // Update is called once per frame
    void Update(){
        if(E1.GetComponent<EnemyController>().CollidedWithBall||E2.GetComponent<EnemyController>().CollidedWithBall||
        E3.GetComponent<EnemyController>().CollidedWithBall||E4.GetComponent<EnemyController>().CollidedWithBall||
        E5.GetComponent<EnemyController>().CollidedWithBall){

        Lives=E1.GetComponent<EnemyController>().EnemyLives;
        if(Lives<=4 && Lives>0){
           TotalScore=TotalScore+10;
        }
        else{
           TotalScore=TotalScore+100;
        }
        Lives=E2.GetComponent<EnemyController>().EnemyLives;
        if(Lives<=4 && Lives>0){
           TotalScore=TotalScore+10;
        }
        else{
            TotalScore=TotalScore+100;
        }
        Lives=E3.GetComponent<EnemyController>().EnemyLives;
        if(Lives<=4 && Lives>0){
           TotalScore=TotalScore+10;
        }
       else{
           TotalScore=TotalScore+100;
        }
        Lives=E4.GetComponent<EnemyController>().EnemyLives;
       if(Lives<=4 && Lives>0){
           TotalScore=TotalScore+10;
       }
       else{
           TotalScore=TotalScore+100;
       }
       Lives=E5.GetComponent<EnemyController>().EnemyLives;
       if(Lives<=4 && Lives>0){
           TotalScore=TotalScore+10;
       }
       else{
           TotalScore=TotalScore+100;
        }
        }
    }
}
