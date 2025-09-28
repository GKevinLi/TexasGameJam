using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool pressed = false;
    Vector3 pos;
    public GameObject controlledGameObject;


    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(pressed);
        if(pressed) {
            pressButton();
            controlledGameObject.SetActive(true);
        }
        else {
            depressButton();
            controlledGameObject.SetActive(false);
        }
    }
    void OnTriggerStay2D(Collider2D collision) 
    { 
        Debug.Log("COLLIDED");
        if (collision.gameObject.GetComponent<TimerScript>() != null) {
            if(this.transform.parent.gameObject.GetComponent<TimerScript>().startingTime < collision.gameObject.GetComponent<TimerScript>().startingTime) {
                pressed = true;
            }
            else {
                pressed = false;
            }
            
        } 
        if (collision.gameObject.GetComponent<PlayerTimer>() != null) {
            if(this.transform.parent.gameObject.GetComponent<TimerScript>().startingTime < collision.gameObject.GetComponent<PlayerTimer>().startingTime) {
                pressed = true;
            }
            else {
                pressed = false;
            }
        } 
        
    } 
    void OnTriggerExit2D(Collider2D collision) 
    { 
        //Debug.Log("COLLIDED");
        if (collision.gameObject.GetComponent<TimerScript>() != null) {
           
            pressed = false;
        } 
        if (collision.gameObject.GetComponent<PlayerTimer>() != null) {
            
            pressed = false;
        } 
        
    } 
    public void pressButton() {
        transform.position = new Vector3(pos.x, pos.y - 0.1f, pos.z);
    }
    public void depressButton() {
        transform.position = new Vector3(pos.x, pos.y, pos.z);
    }
}
