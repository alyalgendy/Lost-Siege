using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgain : MonoBehaviour
{
    // Start is called before the first frame update
    public void Replay()
    {
        SceneManager.LoadSceneAsync(2);
    }

}
