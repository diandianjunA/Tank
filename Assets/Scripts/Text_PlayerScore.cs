using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Text_PlayerScore : MonoBehaviour
{
    public TextMeshProUGUI Text;
    // Start is called before the first frame update
    void Start()
    {
        Text=transform.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Text.text=PlayerManager.Instance.playerScore.ToString();
    }
}
