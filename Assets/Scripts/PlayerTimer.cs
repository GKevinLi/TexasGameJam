using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerTimer : MonoBehaviour
{
    public GameObject text;
    private GameObject newText;
    private InputAction leftMouseClick;
    private Transform initialTransform;

    public Animator anim;


    public GameObject fadeObject;
    public GameObject obj2;
    public GameObject obj3;
    bool dead = false;

    public int startingTime;
    public int timeTransferAmt = 0;

    private Coroutine moveText2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftMouseClick =  InputSystem.actions.FindAction("Click");
        newText = Instantiate(text, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation) as GameObject;
        newText.SetActive(true);
        initialTransform = transform;
        initialTransform.rotation = transform.rotation;
        newText.GetComponent<TMP_Text>().text = startingTime + "";
        StartCoroutine(secondCounter());

        anim = gameObject.GetComponent<Animator>();

        fadeObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        fadeObject.SetActive(false);
    }
    //testing
    // Update is called once per frame
    void Update()
    {
        newText.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        
        newText.GetComponent<TMP_Text>().text = startingTime + "";
        if(startingTime <= 0) {
            Destroy(newText);
            anim.SetTrigger("onDeath");
            onDeath();
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
            
	    if(hit.collider != null)
        {
                //Debug.Log(hit.collider.gameObject);
            if(hit.collider.gameObject.GetComponent<TimerScript>() != null) {
                hit.collider.gameObject.GetComponent<TimerScript>().startingTime += timeTransferAmt;
                startingTime -= timeTransferAmt;

                hit.collider.gameObject.GetComponent<TimerScript>().showTimeChange(timeTransferAmt);
                showTimeChange(-timeTransferAmt);
            }
        }
    }
    public void removeTime() {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
	    RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);
		Vector2 hitPosition = hit.point;
        float distance = Vector3.Distance(transform.position, hitPosition);

	    if(hit.collider != null)
        {
                //Debug.Log(hit.collider.gameObject);
            if(hit.collider.gameObject.GetComponent<TimerScript>() != null) {
                hit.collider.gameObject.GetComponent<TimerScript>().startingTime -= timeTransferAmt;
                startingTime += timeTransferAmt;
                    //hit.collider.gameObject.GetComponent<TimerScript>().changePaused();
                hit.collider.gameObject.GetComponent<TimerScript>().showTimeChange(-timeTransferAmt);
                showTimeChange(timeTransferAmt);
            }
        }
    }
    public void showTimeChange(int timeChange) {
        GameObject tempText = Instantiate(text, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), new Quaternion(0, 0, 0, 0)) as GameObject;
        tempText.AddComponent<DestroyAfterTime>();
        tempText.SetActive(true);
        if(timeChange < 0) {
            tempText.GetComponent<TMP_Text>().text = "" + timeChange;
        }
        else {
            tempText.GetComponent<TMP_Text>().text = "+" + timeChange;
        }
        moveText2 = StartCoroutine(moveTimeChange(tempText));
    }
    IEnumerator moveTimeChange(GameObject tempText) {
        while (true) {
            yield return new WaitForSeconds(0.01f); 
            tempText.transform.position = new Vector3(tempText.transform.position.x, tempText.transform.position.y + 0.005f, tempText.transform.position.z);
            tempText.GetComponent<TMP_Text>().color = new Color(tempText.GetComponent<TMP_Text>().color.r, tempText.GetComponent<TMP_Text>().color.g, tempText.GetComponent<TMP_Text>().color.b, tempText.GetComponent<TMP_Text>().color.a - 0.03f);

            if(tempText.GetComponent<TMP_Text>().color.a <= 0.0f) {
                Destroy(tempText);
            }
        }


    }
    public void onDeath() {
        
        StartCoroutine(fadeIn());
    }
    IEnumerator fadeIn() {
        fadeObject.SetActive(true);
        
        fadeObject.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        while (true) {
            yield return new WaitForSeconds(0.1f); 
            fadeObject.GetComponent<Image>().color = new Color(fadeObject.GetComponent<Image>().color.r, fadeObject.GetComponent<Image>().color.g, fadeObject.GetComponent<Image>().color.b, fadeObject.GetComponent<Image>().color.a + 0.1f);
        
            if(fadeObject.GetComponent<Image>().color.a >= 1) {
                yield return new WaitForSeconds(1f); 
                obj2.SetActive(true);
                obj3.SetActive(true);
                Time.timeScale = 0f;
                yield break;
            }
        }
    
    }
    
}
