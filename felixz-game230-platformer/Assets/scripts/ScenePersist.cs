using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScenePersist : MonoBehaviour
{
    int startSceneIndex;
    
    // Start is called before the first frame update
    [SerializeField] bool LightSwitch = false;

    private void Awake()
    {   

            if(LightSwitch)
            {
                return;
            }

            int numScenePersists = FindObjectsOfType<ScenePersist>().Length;

            if (numScenePersists > 1)
            {
                Destroy(gameObject);
            }

            else
            {
                DontDestroyOnLoad(gameObject);
            }
        
    }
    void Start()
    {
        
        startSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }


    // Update is called once per frame
    void Update()
    {
        if(LightSwitch)
        {
            return;
        }
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if(currentSceneIndex != startSceneIndex)
        {
            Destroy(gameObject);
        }
    }
}
