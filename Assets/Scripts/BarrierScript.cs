using UnityEngine;
using TMPro;

public class BarrierScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public BoxCollider2D box1;
    public BoxCollider2D box2;
    public int timeThreshold;
    private GameObject text;
    public bool reverseBarrier = false;

    void Start()
    {
       text = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        text.GetComponent<TMP_Text>().text = timeThreshold + "";
    }

    void OnTriggerStay2D(Collider2D collision) 
    { 
        //Debug.Log("COLLIDED");
        if(!reverseBarrier) {
            if (collision.gameObject.GetComponent<TimerScript>() != null) {
                if(timeThreshold <= collision.gameObject.GetComponent<TimerScript>().startingTime) {
                    box1.enabled = false;
                }
                else {
                    box1.enabled = true;
                }
            
            } 
            if (collision.gameObject.GetComponent<PlayerTimer>() != null) {
                if(timeThreshold <= collision.gameObject.GetComponent<PlayerTimer>().startingTime) {
                    box1.enabled = false;
                }
                else {
                    box1.enabled = true;
                }
            } 
        }
        else {
            if (collision.gameObject.GetComponent<TimerScript>() != null) {
                if(timeThreshold >= collision.gameObject.GetComponent<TimerScript>().startingTime) {
                    box1.enabled = false;
                }
                else {
                    box1.enabled = true;
                }
            
            } 
            if (collision.gameObject.GetComponent<PlayerTimer>() != null) {
                if(timeThreshold >= collision.gameObject.GetComponent<PlayerTimer>().startingTime) {
                    box1.enabled = false;
                }
                else {
                    box1.enabled = true;
                }
            }
        }
    } 
}
