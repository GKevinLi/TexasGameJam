using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextScene(string sceneName)
    {
        Debug.Log("AUIGSDUABIUd");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(sceneName.Equals("next")) {
            
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else if(sceneName.Equals("this")) {
            SceneManager.LoadScene(currentSceneIndex);
        }
        else {
            SceneManager.LoadScene(sceneName);
        }
        
    }
}
