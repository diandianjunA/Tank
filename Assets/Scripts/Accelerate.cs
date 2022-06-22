using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerate : MonoBehaviour
{
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
            collision.gameObject.SendMessage("Accelerate");
            medioAudio.Play();
            sr.sprite = null;
            Destroy(gameObject, 0.5f);
        }
    }
}
