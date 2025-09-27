using UnityEngine;
using TMPro;
using System.Collections;

public class TimerScript : MonoBehaviour
{

    public GameObject text;
    private GameObject newText;
    public int startingTime;
    public bool paused = false;
    private Coroutine timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        newText = Instantiate(text, new Vector3(transform.position.x + (transform.position.x * 0.25f), transform.position.y + 1, transform.position.z), transform.rotation) as GameObject;
        newText.SetActive(true);
       
        newText.GetComponent<TMP_Text>().text = startingTime + "";
        timer = StartCoroutine(secondCounter());
    }

    // Update is called once per frame
    void Update()
    {
        newText.transform.position = new Vector3(transform.position.x + 0.75f, transform.position.y + 1, transform.position.z);
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
}
