using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LevelCompleteScript : MonoBehaviour
{
    GameObject camera;
    public GameObject fadeObject;
    public GameObject endButton;
    GameObject player;
    public Vector3 newPos;
    public TimerScript[] unpauseObjects;
    public int newTime;

    void Start()
    {
        camera = GameObject.Find("Main Camera");
        player = GameObject.Find("Player");
        
        fadeObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        fadeObject.SetActive(false);
        endButton.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator fadeIn() {
        fadeObject.SetActive(true);
        
        fadeObject.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        while (true) {
            yield return new WaitForSeconds(0.1f); 
            fadeObject.GetComponent<Image>().color = new Color(fadeObject.GetComponent<Image>().color.r, fadeObject.GetComponent<Image>().color.g, fadeObject.GetComponent<Image>().color.b, fadeObject.GetComponent<Image>().color.a + 0.1f);
        
            if(fadeObject.GetComponent<Image>().color.a >= 1) {
                endButton.SetActive(true);
                //yield return StartCoroutine(fadeOut());
                yield break;
            }
        }
    
    }
    void OnTriggerEnter2D(Collider2D collision) {

        if(collision.gameObject.GetComponent<ToolManager>() != null) {
            StartCoroutine(fadeIn());
        }
    }
}
