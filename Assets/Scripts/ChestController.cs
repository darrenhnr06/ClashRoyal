using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class ChestController : MonoBehaviour
{
    public AudioSource buttonSound;
    public AudioSource cashSound;

    public Button addChestButton;
    public Image[] image;
    public bool[] status; 
    public Button[] chestButtons;
    public GameObject[] chestUI;
    
    public Sprite openBox;
    public Canvas canvas;

    private int coins;
    private int gems;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI gemText;
    public TextMeshProUGUI timer;

    public Button[] waitButton;
    public Button[] spendButton;

    public GameObject timerGameObject;

    private bool startTimer = false;
    private float numOfSecs;

    private string buttonType;
    private GameObject openedButton;

    private void Start()
    {
        coins = 1000;
        gems = 50;
        addChestButton.onClick.AddListener(addChest);
        for(int i=0;i<4;i++)
        {
            waitButton[i].onClick.AddListener(Timer);
            spendButton[i].onClick.AddListener(Spend);
        }
    }


    private void Update()
    {
        coinText.text = coins.ToString();
        gemText.text = gems.ToString();

        if(startTimer == true)
        {
            CountDown();
        }
    }

    void Spend()
    {
        buttonSound.Play();
        cashSound.Play();

        if (buttonType == "Button1")
        {
            gems -= 2;
            coins += UnityEngine.Random.Range(100, 200);
            gems += UnityEngine.Random.Range(10, 20);
            chestUI[0].gameObject.SetActive(false);
        }
        else if (buttonType == "Button2")
        {
            gems -= 3;
            coins += UnityEngine.Random.Range(300, 500);
            gems += UnityEngine.Random.Range(20, 40);
            chestUI[1].gameObject.SetActive(false);
        }
        else if (buttonType == "Button3")
        {
            gems -= 6;
            coins += UnityEngine.Random.Range(600, 800);
            gems += UnityEngine.Random.Range(45, 60);
            chestUI[2].gameObject.SetActive(false);
        }
        else if (buttonType == "Button4")
        {
            gems -= 18;
            coins += UnityEngine.Random.Range(100, 200);
            gems += UnityEngine.Random.Range(80, 100);
            chestUI[3].gameObject.SetActive(false);
        }

        if (openedButton.name == "Button0")
        {
            status[0] = false;
        }
        else if (openedButton.name == "Button1")
        {
            status[1] = false;
        }
        else if (openedButton.name == "Button2")
        {
            status[2] = false;
        }
        else if (openedButton.name == "Button3")
        {
            status[3] = false;
        }
        Debug.Log(openedButton.name);
        openedButton.name = "GarbageButton";
        openedButton.SetActive(false);
    }


    void addChest()
    {
        buttonSound.Play();

        for (int i=0;i<4;i++)
        {
            if(status[i]==false)
            {
                Vector3 position = image[i].gameObject.transform.position;
                int k = UnityEngine.Random.Range(0, 3);
                Button button = GameObject.Instantiate(chestButtons[k]).GetComponent<Button>();
                button.name = "Button" + i;
                PlayerPrefs.SetString(button.name, chestButtons[k].name);
                button.transform.position = position;
                button.gameObject.SetActive(true);
                button.gameObject.transform.SetParent(image[i].gameObject.transform);
                status[i] = true; 
                button.onClick.AddListener(openChest);
                
                return;
            }
        }
    }

    void openChest()
    {
        buttonSound.Play();

        string name = EventSystem.current.currentSelectedGameObject.name;
        GameObject button = GameObject.Find(name);
        button.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        button.gameObject.GetComponent<Button>().image.sprite = openBox;

        buttonType = PlayerPrefs.GetString(button.name);
        openedButton = button;

        if (buttonType == "Button1")
        {
            chestUI[0].gameObject.SetActive(true);
        }
        else if(buttonType == "Button2")
        {
            chestUI[1].gameObject.SetActive(true);
        }
        else if(buttonType == "Button3")
        {
            chestUI[2].gameObject.SetActive(true);
        }
        else if(buttonType == "Button4")
        {
            chestUI[3].gameObject.SetActive(true);
        }
    }

    void Timer()
    {
        timerGameObject.gameObject.SetActive(true);

        if (buttonType == "Button1")
        {
            chestUI[0].gameObject.SetActive(false);
        }
        else if (buttonType == "Button2")
        {
            chestUI[1].gameObject.SetActive(false);
        }
        else if (buttonType == "Button3")
        {
            chestUI[2].gameObject.SetActive(false);
        }
        else if (buttonType == "Button4")
        {
            chestUI[3].gameObject.SetActive(false);
        }

        string name = EventSystem.current.currentSelectedGameObject.name;
        
        if (name == waitButton[0].name)
        {
            //numOfSecs = 900;
            numOfSecs = 5;
            startTimer = true;
        }
        else if (name == waitButton[1].name)
        {
            //numOfSecs = 1800;
            numOfSecs = 6;
            startTimer = true;
        }
        else if (name == waitButton[2].name)
        {
            //numOfSecs = 3600;
            numOfSecs = 7;
            startTimer = true;
        }
        else if (name == waitButton[3].name)
        {
            //numOfSecs = 10800;
            numOfSecs = 8;
            startTimer = true;
        }
        
    }
    void CountDown()
    {
        float sec, min;
        min = numOfSecs / 60;
        sec = numOfSecs % 60;
            
        numOfSecs -= Time.deltaTime;


        timer.text = ((int)(min)).ToString() + "Mins " + ((int)(sec)).ToString() + "Sec";
            
        if(numOfSecs < 0)
        {
            cashSound.Play();

            startTimer = false;
            timerGameObject.gameObject.SetActive(false);
            if (buttonType == "Button1")
            {
                coins += UnityEngine.Random.Range(100, 200);
                gems += UnityEngine.Random.Range(10, 20);
            }
            else if (buttonType == "Button2")
            {
                coins += UnityEngine.Random.Range(300, 500);
                gems += UnityEngine.Random.Range(20, 40);
            }
            else if (buttonType == "Button3")
            {
                coins += UnityEngine.Random.Range(600, 800);
                gems += UnityEngine.Random.Range(45, 60);
            }
            else if (buttonType == "Button4")
            {
                coins += UnityEngine.Random.Range(100, 200);
                gems += UnityEngine.Random.Range(80, 100);
            }
        }
       

        if (openedButton.name == "Button0")
        {
            status[0] = false;
        }
        else if (openedButton.name == "Button1")
        {
            status[1] = false;
        }
        else if (openedButton.name == "Button2")
        {
            status[2] = false;
        }
        else if (openedButton.name == "Button3")
        {
            status[3] = false;
        }
        Debug.Log(openedButton.name);
        openedButton.name = "GarbageButton";
        openedButton.SetActive(false);
    }
}
