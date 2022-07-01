using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
     private Animator Anim; 
    
    [SerializeField] Rigidbody2D EnemyRB;
    [SerializeField] Collider2D EnemyCD;
    [SerializeField] Transform player;
    [SerializeField] float strikingDistance;
    [SerializeField] Transform selfEnemy;
    [SerializeField] float JumpFactor;
    private Animator EAnim; 
    public int EnemyLives=4;
    public int ScoreP=0;
    bool check=false;
    bool OnGround=false;
    public bool IsDestroyed=false;
    public bool CollidedWithBall=false;
    
    // Start is called before the first frame update
    void Start()
    {
     EnemyCD=GetComponent<BoxCollider2D>();
     StartCoroutine(EnemySpawner());
     EnemyRB=GetComponent<Rigidbody2D>();  
     EAnim=GetComponent<Animator>();
    }

    // Update is called once per frame
   void Update()
    {
        CollidedWithBall=false;
        Isgrounded();
        if(OnGround==true){
            if((player.position-selfEnemy.position).magnitude > strikingDistance){
         if(check == false && selfEnemy.position.x> selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.GetComponent<MyJumpingPoints>().jumpUpPoint.position.x )
            {
                Debug.Log("Chilling Left");
                Vector3 newPosition = selfEnemy.position; 
                newPosition.x = newPosition.x - (0.05f); 
                transform.rotation=Quaternion.Euler(new Vector2(0,0));
                selfEnemy.position = newPosition; 
            }
            if(check == true && selfEnemy.position.x< selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.GetComponent<MyJumpingPoints>().jumpDownPoint.position.x)
            {
                Debug.Log("chilling Right");
                Vector3 newPosition = selfEnemy.position; 
                newPosition.x = newPosition.x + (0.05f); 
                transform.rotation=Quaternion.Euler(new Vector2(0,180));
                selfEnemy.position = newPosition; 
            }
            if(selfEnemy.position.x < selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.GetComponent<MyJumpingPoints>().jumpUpPoint.position.x )
            {
                check = true;
            }
            if(selfEnemy.position.x > selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.GetComponent<MyJumpingPoints>().jumpDownPoint.position.x )
            {
                check = false;
            }
        }
        else{
            if(null != player.GetComponent<MyPlatform>().standingOnPlatform && 
                null != selfEnemy.GetComponent<MyPlatform>().standingOnPlatform){
                    if(player.GetComponent<MyPlatform>().standingOnPlatform.gameObject.name ==
                        selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.gameObject.name){
                        // if(player.GetComponent<MyPlatform>().standingOnPlatform.position.y == 
                        // selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.position.y){
                            Debug.Log("Player: " + player.GetComponent<MyPlatform>().standingOnPlatform.position.y);
                            Debug.Log("Enemy: " + selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.position.y);
                            if(player.position.x < selfEnemy.position.x){
                                // move left
                                Debug.Log("need to move left");
                                Vector3 newPosition = selfEnemy.position; 
                                newPosition.x = newPosition.x - (0.05f); 
                                 transform.rotation=Quaternion.Euler(new Vector2(0,0));
                                selfEnemy.position = newPosition; 
                            }
                            else{
                                // move right
                                Vector3 newPosition = selfEnemy.position; 
                                newPosition.x = newPosition.x + (0.05f); 
                                selfEnemy.position = newPosition; 
                                transform.rotation=Quaternion.Euler(new Vector2(0,180));
                                Debug.Log("need to move right");
                            }
                        }
                        else if(player.GetComponent<MyPlatform>().standingOnPlatform.position.y < 
                        selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.position.y &&
                        selfEnemy.position.x> selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.GetComponent<MyJumpingPoints>().jumpUpPoint.position.x ){
                            // jump down
                            Debug.Log("need to jump down");
                            EnemyCD.enabled = false;
                            StartCoroutine(waiter());
                        }
                        else{
                            Debug.Log("standing on: " + selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.gameObject.name);
                            if(null!=selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.GetComponent<MyJumpingPoints>() &&
                            selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.GetComponent<MyJumpingPoints>().jumpUpPoint.position.x < 
                            selfEnemy.position.x){
                                // jump up
                                Debug.Log("need to jump up, after moving left");
                                //transform.position=Vector2.MoveTowards(selfEnemy.position,selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.GetComponent<MyJumpingPoints>().jumpDownPoint.position,2*Time.deltaTime);
                                if(Mathf.Abs(EnemyRB.velocity.y)==0){
                                    EnemyRB.AddForce(Vector2.up * JumpFactor,ForceMode2D.Impulse);
                                }
                            }
                            else if(null!=selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.GetComponent<MyJumpingPoints>() &&
                            selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.GetComponent<MyJumpingPoints>().jumpUpPoint.position.x > 
                            selfEnemy.position.x){
                                // jump up
                                Debug.Log("need to jump up, after moving right");
                                if(Mathf.Abs(EnemyRB.velocity.y)==0){
                                    EnemyRB.AddForce(Vector2.up * JumpFactor,ForceMode2D.Impulse);
                                }
                            }
                        }
                    }
                    else{
                        if(player.GetComponent<MyPlatform>().standingOnPlatform.position.y == 
                        selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.position.y){
                            if(player.position.x < selfEnemy.position.x){
                                // move left to fall down
                                Debug.Log("need to move left, to fall down");
                                Vector3 newPosition = selfEnemy.position; 
                                newPosition.x = newPosition.x - (0.05f); 
                                selfEnemy.position = newPosition; 
                            }
                            else{
                                // move right to fall down
                                Debug.Log("need to move right, to fall down");
                                Vector3 newPosition = selfEnemy.position; 
                                newPosition.x = newPosition.x + (0.05f); 
                                selfEnemy.position = newPosition; 
                            }
                        }

                    }
                }
        }
    }

 private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Platform"){
            //Debug.Log("OnPlatform");
            //EAnim.SetBool("On Floor",true);
        }
        else if(other.gameObject.tag=="SnowBall"){
            CollidedWithBall=true;
            EnemyLives--;
            if(EnemyLives==0){
                EnemyRB.constraints = RigidbodyConstraints2D.FreezePosition;
                Destroy(this.gameObject);
            }
        }
     }
     void Isgrounded(){
           if(Mathf.Abs(EnemyRB.velocity.y)==0){
             EAnim.SetBool("On Floor",true);
             OnGround=true;
             }
             else{
                 OnGround=false;
                 EAnim.SetBool("On Floor",false);
             }   
      }
       IEnumerator EnemySpawner()
    {
        //Debug.Log("E "+EnemyCD);
        EnemyCD.enabled = false;
        //Debug.Log("EF "+EnemyCD);
        yield return new WaitForSeconds(1f);
        EnemyCD.enabled = true;
        //Debug.Log("ET "+EnemyCD);
    }
    IEnumerator waiter()
    {
        yield return new WaitForSeconds(0.35f);
        EnemyCD.enabled = true;
       // Debug.Log("wait over");
    }
}