using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SnowBallScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D SnowBallRB;
    [SerializeField] float ShootForce;
    [SerializeField] Vector2 ShootingDirection;
    private Animator Anim; 
    

    // Start is called before the first frame update
    void Start()
    {
        SnowBallRB=GetComponent<Rigidbody2D>();
        Anim=GetComponent<Animator>();
        ShootingDirection.x=transform.parent.GetComponent<PlayerMovement>().GetFacingDirection().x;
        SnowBallRB.AddForce((ShootingDirection.normalized)*ShootForce,ForceMode2D.Impulse);
        Anim.SetBool("Contact",false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void OnCollisionEnter2D(Collision2D other)
    {   
        if(this.gameObject.name != other.gameObject.name){
            Anim.SetBool("Contact",true);
            SnowBallRB.constraints = RigidbodyConstraints2D.FreezePosition;
            Destroy(this.gameObject,0.3f);
        }
    }
}
