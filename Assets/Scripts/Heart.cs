using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite brokenSprite;
    public GameObject explosionPrefab;
    public AudioClip dieAudio;
    // Start is called before the first frame update
    void Start()
    {
        sr= GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Die()
    {
        sr.sprite = brokenSprite;
        Instantiate(explosionPrefab,transform.position,transform.rotation);
        PlayerManager.Instance.isDefeated = true;
        PlayerManager.Instance.isDeath = true;
        AudioSource.PlayClipAtPoint(dieAudio,transform.position);
    }
}
