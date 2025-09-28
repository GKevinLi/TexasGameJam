using UnityEngine;
using TMPro;
using System.Collections;

public class TimerScript : MonoBehaviour
{

    public GameObject text;
    private GameObject newText;
    private Transform initialTransform;

    

    public int startingTime;
    public bool paused;
    private Coroutine timer;

    private Coroutine moveText2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        newText = Instantiate(text, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), new Quaternion(0, 0, 0, 0)) as GameObject;
        newText.SetActive(true);
        initialTransform = transform;
        newText.GetComponent<TMP_Text>().text = startingTime + "";
        if(!paused) {
            timer = StartCoroutine(secondCounter());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        newText.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        newText.GetComponent<TMP_Text>().text = startingTime + "";
        if(startingTime <= 0) {
            Destroy(newText);
            Destroy(this.gameObject);
        }
    }
    IEnumerator secondCounter()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); 
            startingTime--;
            
        }
    }
    public void changePaused() {
        if(paused) {
            paused = !paused;
            timer = StartCoroutine(secondCounter());
        }
        else {
            paused = !paused;
            StopCoroutine(timer);
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
                if(tempText != null) {
                    Destroy(tempText);
                }
                
            }
            
        }


    }
    
}
