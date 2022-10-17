using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.gm.level > 1)
        {
            SceneManager.LoadScene(GameManager.gm.level - 1);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
