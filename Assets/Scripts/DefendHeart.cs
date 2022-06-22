using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendHeart : MonoBehaviour
{
    public GameObject[] elements=new GameObject[2];
    public AudioClip clip;
    public AudioSource medioAudio;
    public SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        medioAudio.clip = clip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tank")
        {
            Destroy(Instantiate(elements[0], new Vector3(1, -8f, 0), Quaternion.identity), 5);
            Destroy(Instantiate(elements[0], new Vector3(-1, -8f, 0), Quaternion.identity), 5);
            Destroy(Instantiate(elements[0], new Vector3(-1, -7f, 0), Quaternion.identity), 5);
            Destroy(Instantiate(elements[0], new Vector3(0, -7f, 0), Quaternion.identity), 5);
            Destroy(Instantiate(elements[0], new Vector3(1, -7f, 0), Quaternion.identity), 5);
            medioAudio.Play();
            sr.sprite = null;
            Invoke("wait", 5f);
        }
    }
    private void createItem(GameObject element)
    {
        Instantiate(element, new Vector3(1, -8, 0), Quaternion.identity);
        Instantiate(element, new Vector3(-1, -8, 0), Quaternion.identity);
        for (int i = -1; i <= 1; i++)
        {
            Instantiate(element, new Vector3(i, -7, 0), Quaternion.identity);
        }
    }
    public void wait()
    {
        createItem(elements[1]);
        Destroy(gameObject);
    }
}
