using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class ToolManager : MonoBehaviour
{
    private float scrollDelta;
    private int scrollVal;
    public GameObject[] tools;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 scrollValue = Mouse.current.scroll.ReadValue();

        // The Y component represents vertical scrolling
        scrollDelta = scrollValue.y;
        scrollVal += (int) scrollDelta;
        scrollVal = scrollVal % 2;
        if(scrollVal == -1) {
            scrollVal = 1;
        }
        Debug.Log(scrollVal);
        switchTools(scrollVal);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
	        if(scrollVal == 0) {
                gameObject.GetComponent<PlayerTimer>().addTime();
            }
            if(scrollVal == 1) {
                gameObject.GetComponent<PlayerTimer>().removeTime();
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
