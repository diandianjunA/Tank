using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerManager : MonoBehaviour
{
    //ÊôÐÔÖµ
    public int lifeVal = 3;
    public int playerScore = 0;
    private static PlayerManager instance;
    public bool isDeath;
    public GameObject born;
    public bool isDefeated;
    public GameObject gameOver;
    public Scene curScene;

    public static PlayerManager Instance { get => instance; set => instance = value; }
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        curScene=SceneManager.GetActiveScene();
        if (isDeath == true)
        {
            Recover();
        }
        if(isDefeated == true)
        {
            gameOver.SetActive(true);
            GameObject[] go = curScene.GetRootGameObjects();
            for (int i = 0; i < go.Length; i++)
            {

                if (go[i].tag == "Enemy")
                {
                    go[i].SendMessage("KeepStaticForever", go[i]);
                }
            }
            if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
            {
                ReturnToTheMainMenu();
            }
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ReturnToTheMainMenu();
        }
    }
    private void Recover()
    {
        if (lifeVal <= 0)
        {
            isDefeated = true;
            
        }
        else
        {
            lifeVal--;
            GameObject go = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer=true;
            isDeath = false;
        }
    }
    private void ReturnToTheMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
