using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class DoorScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameObject camera;
    GameObject fadeObject;
    GameObject player;
    public Vector3 newPos;
    public TimerScript[] unpauseObjects;
    public int newTime;

    void Start()
    {
        camera = GameObject.Find("Main Camera");
        player = GameObject.Find("Player");
        fadeObject = camera.transform.GetChild(0).gameObject;
        fadeObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 255);
        fadeObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator fadeIn() {
        fadeObject.SetActive(true);
        
        fadeObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);
        while (true) {
            yield return new WaitForSeconds(0.1f); 
            fadeObject.GetComponent<Renderer>().material.color = new Color(fadeObject.GetComponent<Renderer>().material.color.r, fadeObject.GetComponent<Renderer>().material.color.g, fadeObject.GetComponent<Renderer>().material.color.b, fadeObject.GetComponent<Renderer>().material.color.a + 0.1f);
        
            if(fadeObject.GetComponent<Renderer>().material.color.a >= 1) {
                yield return new WaitForSeconds(1f); 
                yield return StartCoroutine(fadeOut());
                yield break;
            }
        }
    
    }
    IEnumerator fadeOut() {
        player.transform.position = newPos;
        player.GetComponent<PlayerTimer>().startingTime = newTime;
        for(int i = 0; i < unpauseObjects.Length; i++) {
            unpauseObjects[i].changePaused();
        }
        
        while (true) {
            yield return new WaitForSeconds(0.1f); 
            fadeObject.GetComponent<Renderer>().material.color = new Color(fadeObject.GetComponent<Renderer>().material.color.r, fadeObject.GetComponent<Renderer>().material.color.g, fadeObject.GetComponent<Renderer>().material.color.b, fadeObject.GetComponent<Renderer>().material.color.a - 0.1f);
        
            if(fadeObject.GetComponent<Renderer>().material.color.a <= 0) {
                fadeObject.SetActive(false);
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
