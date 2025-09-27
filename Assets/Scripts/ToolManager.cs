using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class ToolManager : MonoBehaviour
{
    private float scrollDelta;
    private int scrollVal;
    public GameObject[] tools;
    public GameObject boxPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tools[0] = transform.Find("Add Tool").gameObject;
        tools[1] = transform.Find("Remove Tool").gameObject;
        tools[2] = transform.Find("Create Tool").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 scrollValue = Mouse.current.scroll.ReadValue();

        // The Y component represents vertical scrolling
        scrollDelta = scrollValue.y;
        scrollVal += (int) scrollDelta;
        scrollVal = scrollVal % 3;
        if(scrollVal == -1) {
            scrollVal = 2;
        }
        
        switchTools(scrollVal);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
	        if(scrollVal == 0) {
                gameObject.GetComponent<PlayerTimer>().addTime();
            }
            if(scrollVal == 1) {
                gameObject.GetComponent<PlayerTimer>().removeTime();
            }
            if(scrollVal == 2) {
                Vector3 vec = (Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
	            Instantiate(boxPrefab, new Vector3(vec.x, vec.y, 10), transform.rotation);
                gameObject.GetComponent<PlayerTimer>().startingTime -= 5;
            }
        }

    }

    public void switchTools(int val) {
        for(int i = 0; i < tools.Length; i++) {
            if(i == val) {
                tools[i].SetActive(true);
            }
            else {
                tools[i].SetActive(false);
            }
        }

    }
}
