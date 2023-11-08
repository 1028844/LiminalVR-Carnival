using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReStartGame : MonoBehaviour

{
// This to reload the scene and go back to the begining.
    public void ResetScene()
    {
        SceneManager.LoadScene(0);
    }
}

