using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btnStart_click()
    {
        Debug.Log("Unload This Sence and load Level One.");
        SceneManager.LoadSceneAsync(0);
    }
}
