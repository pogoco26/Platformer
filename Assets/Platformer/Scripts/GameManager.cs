using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI coinText;
    int coins = 0;
    // Start is called before the first frame update
    Camera m_Camera;
    void Awake()
    {
        m_Camera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        int intTime = 400 - (int)Time.realtimeSinceStartup;
        string timeString = $"Time \n {intTime}";
        string coinString;
        timerText.text = timeString;
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePosition = mouse.position.ReadValue();
            Ray ray = m_Camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Use the hit variable to determine what was clicked on.
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
    }
}
