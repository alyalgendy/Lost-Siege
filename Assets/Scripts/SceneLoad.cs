using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public float delayTime;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadScene", delayTime);
    }

    // Update is called once per frame
    void LoadScene()
    {
        SceneManager.LoadSceneAsync(2);
    }
}