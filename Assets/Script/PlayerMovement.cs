using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public int score =0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3 (moveHorizontal, moveVertical, 0);
        transform.Translate(direction * speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D( Collider2D other)
    {
       if(other.CompareTag("Star"))
        {
            score = score + 1;

            Destroy(other.gameObject);
            Debug.Log("Star collected!");
            Debug.Log("Score:"+ score); 
          
        } 
    }
}
