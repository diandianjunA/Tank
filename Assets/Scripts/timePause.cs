using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timePause : MonoBehaviour
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
            medioAudio.Play();
            sr.sprite = null;
            GameObject[] go = PlayerManager.Instance.curScene.GetRootGameObjects();
            for (int i = 0; i < go.Length; i++)
            {

                if (go[i].tag == "Enemy")
                {
                    go[i].SendMessage("KeepStatic", go[i]);
                }
            }
            Destroy(gameObject, 0.5f);
        }
    }

}
