using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Material[] possibleMaterials;
    private Material playerMaterial;
    public GameObject ballPrefab;

    public Text scoreText;
    private int score = 0;


    public GameObject winScreen;
    public GameObject loseScreen;

    private void Start()
    {
        playerMaterial = possibleMaterials[Random.Range(0, possibleMaterials.Length)];
        GetComponent<Renderer>().material = playerMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Ball_Scripts ball = other.GetComponent<Ball_Scripts>();
            
            if(ball.ballColor == playerMaterial)
            {
                Destroy(other.gameObject);
                ChangeMaterialPlayer();
                StartCoroutine(SpawnBallAfterDelay(ball.ballColor));

                score++;
                scoreText.text = "Score: " + score;

                if(score == 10)
                {
                    winScreen.SetActive(true);
                }
            }
            else
            {
                loseScreen.SetActive(true);
                
            }
        }
    }

    void ChangeMaterialPlayer()
    {
        playerMaterial = possibleMaterials[Random.Range(0, possibleMaterials.Length)];
        GetComponent<Renderer>().material = playerMaterial;
    }

    IEnumerator SpawnBallAfterDelay(Material ballColor)
    {
        yield return new WaitForSeconds(1f); 

        Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 0.5f, Random.Range(-10f, 10f));
        GameObject newBall = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);

        Ball_Scripts newBallScript = newBall.GetComponent<Ball_Scripts>();
        newBallScript.ballColor = ballColor;
    }
}
