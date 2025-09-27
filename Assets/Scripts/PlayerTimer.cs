using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerTimer : MonoBehaviour
{
    public GameObject text;
    private GameObject newText;
    private InputAction leftMouseClick;
    public int startingTime;
    public int timeTransferAmt = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftMouseClick =  InputSystem.actions.FindAction("Click");
        newText = Instantiate(text, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation) as GameObject;
        newText.SetActive(true);
        
        newText.GetComponent<TMP_Text>().text = startingTime + "";
        StartCoroutine(secondCounter());
    }
    //testing
    // Update is called once per frame
    void Update()
    {
        newText.transform.position = new Vector3(transform.position.x + 0.75f, transform.position.y + 1, transform.position.z);
        
        newText.GetComponent<TMP_Text>().text = startingTime + "";
        if(startingTime <= 0) {
            Destroy(newText);
            Destroy(this.gameObject);
        }
        
        // if (Mouse.current.leftButton.wasPressedThisFrame)
        // {
	    //     addTime();
        // }

        // if (Mouse.current.rightButton.wasPressedThisFrame)
        // {
	    //     removeTime();
        // }

    }
    IEnumerator secondCounter()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); 
            startingTime--;
            
        }
    }
    public void addTime() {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
	    RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);
		Vector2 hitPosition = hit.point;
        float distance = Vector3.Distance(transform.position, hitPosition);
            
	    if(hit.collider != null && distance <= 5.0f)
        {
                //Debug.Log(hit.collider.gameObject);
            if(hit.collider.gameObject.GetComponent<TimerScript>() != null) {
                hit.collider.gameObject.GetComponent<TimerScript>().startingTime += timeTransferAmt;
                startingTime -= timeTransferAmt;
            }
        }
    }
    public void removeTime() {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
	    RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);
		Vector2 hitPosition = hit.point;
        float distance = Vector3.Distance(transform.position, hitPosition);

	    if(hit.collider != null && distance <= 5.0f)
        {
                //Debug.Log(hit.collider.gameObject);
            if(hit.collider.gameObject.GetComponent<TimerScript>() != null) {
                hit.collider.gameObject.GetComponent<TimerScript>().startingTime -= timeTransferAmt;
                startingTime += timeTransferAmt;
                    //hit.collider.gameObject.GetComponent<TimerScript>().changePaused();
            }
        }
    }
    
}
