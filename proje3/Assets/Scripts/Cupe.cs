using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;

public class Cupe : MonoBehaviour
{


    public TextMeshProUGUI TextMeshPro;
    public static TextMeshProUGUI scoreText;
    public CardColor color;
    private bool isTurning = false;
    private int counter = 0;

    public static int score = 0;

    void Update()
    {
        
        if (isTurning)
        {
            if (counter < 180)
            {
                transform.Rotate(eulers: new Vector3(x: 1, y: 0, z: 0));
                counter++;
            }

            else
            {
                CheckEndOfTurning();

            }
        }

     
    }
    private void CheckEndOfTurning()
    {
        isTurning = false;
        if (GameManager.turnedCard == null)
        {
            GameManager.turnedCard = this;
        }
        else
        {
            if (GameManager.turnedCard.color == color)
            {
                GameManager.turnedCard.gameObject.SetActive(false);
                gameObject.SetActive(false);
                UpdateScore();
            }
            else
            {
                GameManager.turnedCard.transform.Rotate(eulers: new Vector3(x: 180, y: 0, z: 0));
                transform.Rotate(eulers: new Vector3(x: 180, y: 0, z: 0));
                RestartGame();
            }
           
            GameManager.upsideCardCount = 0;
            GameManager.turnedCard = null;
        }
    }

    private void OnMouseDown()
    {
        if (isTurning) return;
        if (GameManager.upsideCardCount > 1) return;
        isTurning = true;
        GameManager.upsideCardCount++;
        counter = 0;
      //  print(message: "T�kland� fghfn " + name);
       // GameManager.RestartGame();

    }

    public void UpdateScore()
    {
    
        score += 10;
        if (TextMeshPro != null)
        {
         
        TextMeshPro.text = "Score: " + score;
           
        }
    }

    public void RestartGame()
    {
        print(message: "Yenile butonuna t�kland� ");
       
        score = 0;
        GameManager.upsideCardCount = 0;

        // T�m kartlar� bul
        Cupe[] allCupes = GameObject.FindObjectsOfType<Cupe>();

        // Her kart� etkinle�tir
        foreach (Cupe cupe in allCupes)
        {
            cupe.gameObject.SetActive(true);
        }

        // Kartlar� tekrar kar��t�r
        ShuffleCards(allCupes);
    }

    private static void ShuffleCards(Cupe[] cards)
    {
        // Kartlar�n pozisyonlar�n� rastgele de�i�tir
        for (int i = 0; i < cards.Length; i++)
        {
            int randomIndex = Random.Range(i, cards.Length);
            Vector3 tempPosition = cards[i].transform.position;
            cards[i].transform.position = cards[randomIndex].transform.position;
            cards[randomIndex].transform.position = tempPosition;
        }
    }


    public enum CardColor
    {
        Yellow, Red
    }
}
