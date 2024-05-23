using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chanhgescene : MonoBehaviour
{
    public int scene;
   
    void Start()
    {
        Invoke("ChangeScene", 15f);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(scene);
    }
}
