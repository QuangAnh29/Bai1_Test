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
    public GameObject plane;

    private void Start()
    {
        playerMaterial = possibleMaterials[Random.Range(0, possibleMaterials.Length)];
        GetComponent<Renderer>().material = playerMaterial;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Ball_Scripts ball = collision.gameObject.GetComponent<Ball_Scripts>();

            if (ball.ballColor == playerMaterial)
            {
                Destroy(collision.gameObject);
                ChangeMaterialPlayer();
                StartCoroutine(SpawnBallAfterDelay(ball.ballColor));

                score++;
                scoreText.text = "Score: " + score;

                if (score == 10)
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

        Vector3 planeSize = plane.transform.localScale;
        float xPosition = Random.Range(-planeSize.x * 5f, planeSize.x * 5f);
        float zPosition = Random.Range(-planeSize.z * 5f, planeSize.z * 5f);
        Vector3 spawnPosition = new Vector3(xPosition, 0.05f, zPosition);

        GameObject newBall = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);

        Ball_Scripts newBallScript = newBall.GetComponent<Ball_Scripts>();
        newBallScript.ballColor = ballColor;
    }

}
