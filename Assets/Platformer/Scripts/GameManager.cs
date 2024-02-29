using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI pointText;
    int coins = 0;
    int points = 0;
    string coinString;
    string pointString;
    // Start is called before the first frame update
    Camera m_Camera;
    void Awake()
    {
        m_Camera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        int intTime = 100 - (int)Time.realtimeSinceStartup;
        string timeString = $"Time \n {intTime}";
        timerText.text = timeString;
        if(intTime <= 0){
            Debug.Log("GAME OVER!!!!");
            this.gameObject.tag = "Dead";
        }
        //Mouse mouse = Mouse.current;
        /*
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePosition = mouse.position.ReadValue();
            Ray ray = m_Camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                
                if(hit.transform.gameObject.tag == "Brick"){
                    Destroy(hit.transform.gameObject);
                }
                if(hit.transform.gameObject.tag == "Gold"){
                    coins++;
                    coinString = $"Coins \n {coins}";
                    coinText.text = coinString;
                    Debug.Log(coins);
                }
            }
        }
        */
    }
    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Brick"){
            ContactPoint contact = collision.GetContact(0);
            if (contact.point.y > transform.position.y){
                Destroy(collision.gameObject);
                points += 100;
                pointString = $"Mario \n {points}";
                pointText.text = pointString;
            }
        }
        if(collision.gameObject.tag == "Gold"){
            ContactPoint contact = collision.GetContact(0);
            if(contact.point.y > transform.position.y){
                collision.gameObject.tag = "Untagged";
                points += 100;
                coins++; 
                pointString = $"Mario \n {points}";
                pointText.text = pointString;
                coinString = $"Coins \n {coins}";
                coinText.text = coinString;
            }
        }
        if(collision.gameObject.tag == "Lava"){
            Debug.Log("GAME OVER!!!!");
            this.gameObject.tag = "Dead";
        }
        if(collision.gameObject.tag == "Goal"){
            Debug.Log("YOU WIN!!!!");
            this.gameObject.tag = "Dead";
        }
    }
}
