using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    public float speed = 5f;
    public int score =0;
    public GameObject Pistola;
    public int estrellasParaPistola= 5;
    public bool hasPistola= false;
    public bool isGameOver = false;
    public bool puedeAtacar = false;
    public bool hasWon = false;
    public int enemigosDerrotados = 0;
    public int cristales=0;
    public int estrellas =0;


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

        if(estrellas >= estrellasParaPistola && hasPistola)
        {
            puedeAtacar = true;
        }

        if(puedeAtacar && enemigosDerrotados >= 7
         && ! hasWon)
        {
            hasWon = true;
            Debug.Log("You Won!");
        }

        if(isGameOver) return;
    }


    private void OnTriggerEnter2D( Collider2D other)
    {
       if(other.CompareTag("Star"))
        {
            estrellas++;// suma 1 a la variable de estrella
            score = score + 1;

            Destroy(other.gameObject);
            Debug.Log("Star collected!");
            Debug.Log("Score:"+ score); 

         if(estrellas>= estrellasParaPistola)
            {
                Pistola.SetActive(true);
                Debug.Log("Pistola desbloqueada");
            }
        } 
        if(other.CompareTag("cristal"))
        {
          cristales++;
          score+=5;
          Destroy(other.gameObject);
          Debug.Log("Cristal recogido. Total:"+ cristales);
  
        }


        if(other.CompareTag("Pistola"))
        {
            hasPistola = true;
            Destroy(other.gameObject);
            Debug.Log("Pistola recogida");

        }
        if(other.CompareTag("Enemy") && !isGameOver)
        {   if(puedeAtacar && cristales >0 )
            {   
                cristales--;// gasta un cristal
                enemigosDerrotados = enemigosDerrotados + 1; // sumar 1 al contador de enemigos derrotados
                Destroy(other.gameObject);
                Debug.Log("Enemigo destruido");
            }
            else
            {
                isGameOver =true;

                Debug.Log("Te golpeó un enemigo, Game Over");

                StartCoroutine(RestartLevel());

            }
        
           


        }
        
       
        }

     IEnumerator RestartLevel()// permite esperar un tiempo para reiniciar
        {
            yield return new WaitForSeconds(2f);// espera 2 segundos

            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
            );// vuelve a cargar la escena osea reinicia el juego    
    }
}
