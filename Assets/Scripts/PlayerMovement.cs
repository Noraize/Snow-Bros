using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float MovementSpeed;
    [SerializeField] float JumpFactor;
    [SerializeField] Transform PlayerFace;
    [SerializeField] Text txtlives;
    [SerializeField] GameObject Ball;
    private Rigidbody2D PlayerRB;
    private Animator Anim;  
    private float Horizontal;
    private float Vertical;
    private bool ContactWithLevel;
    Vector2 Facing;
    Vector3 SnowBallStartOffSet;
    int lives=3;
    // Start is called before the first frame update
    void Start()
    {
        PlayerRB=GetComponent<Rigidbody2D>();
        Anim=GetComponent<Animator>();
        Facing= new Vector2(0,0);
    }
    private void Update() {
         Anim.SetBool("AttackOn",false);
        Horizontal=Input.GetAxis("Horizontal");  //Attack
         if(Input.GetButtonDown("Fire1")){
              //Debug.Log(Facing.x.ToString());
          if(Facing.x==1){
              SnowBallStartOffSet.x=Mathf.Abs(SnowBallStartOffSet.x);
          }
          else if(Facing.x==-1){
              SnowBallStartOffSet.x=Mathf.Abs(SnowBallStartOffSet.x)*-1;            
          }
         Anim.SetBool("AttackOn",true);
         Instantiate(Ball,(PlayerFace.position)+SnowBallStartOffSet,Quaternion.identity,this.transform);
        }  
        Vector3 PlayerRotation=PlayerFace.rotation.eulerAngles; //Rotating Player
              if(Horizontal>0){
                   PlayerRotation.y=0;
                   //Debug.Log(PlayerRotation.y.ToString());
               }
               else if(Horizontal<0){
                   PlayerRotation.y=180;
                   //Debug.Log(PlayerRotation.y.ToString());
               }

         PlayerFace.rotation=Quaternion.Euler(PlayerRotation);
         Facing.x=(PlayerFace.rotation.y==0 ? 1 : -1);
        Anim.SetFloat("Walking",Horizontal);  //Player Movement
        float ForceH=Horizontal*10*Time.fixedDeltaTime *MovementSpeed;
        PlayerRB.velocity= new Vector2(ForceH,PlayerRB.velocity.y);
         if(Mathf.Abs(PlayerRB.velocity.y)==0){
            Vertical=Input.GetAxis("Jump");
            Anim.SetFloat("Jumping",Vertical);
            PlayerRB.AddForce(new Vector2(0.0f,Input.GetAxis("Jump")*JumpFactor),ForceMode2D.Impulse);
        }   
    }
    public Vector2 GetFacingDirection(){
        return Facing;
    }
     private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            //Destroy(this.gameObject,1f);
            Physics.IgnoreLayerCollision (9,8, true);
            lives--;
            txtlives.text = "Lives:  " + lives.ToString();
            Anim.SetTrigger("TouchEnemy");
            if(Mathf.Abs(PlayerRB.velocity.y)==0){
                PlayerRB.AddForce(Vector2.up * JumpFactor,ForceMode2D.Impulse);
            }
        }
    }
}
