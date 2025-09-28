using UnityEngine;

public class addToolScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision) {
        Debug.Log("TRIGGERED");
        if(collision.gameObject.GetComponent<ToolManager>() != null) {
            collision.gameObject.GetComponent<ToolManager>().numTools += 1;
            Destroy(gameObject);
        }
    }
}
