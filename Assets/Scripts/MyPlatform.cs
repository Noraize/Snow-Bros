using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlatform : MonoBehaviour
{
    [SerializeField] Transform attachedTo;
    public Transform standingOnPlatform;
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("I am being used");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // Debug.Log("other: " + other.gameObject.name);
        if(other.gameObject.CompareTag("Platform")){
            standingOnPlatform = other.transform;
        }
    }
}
