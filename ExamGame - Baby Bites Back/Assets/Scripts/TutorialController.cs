using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialController : MonoBehaviour
{
    public GameObject WalkingSpeechBubble;
    
    [Header("Attacking Zone")]
    public GameObject AttackingSpeechBubble;
    public GameObject InitialBrock;
    [Header("Spawn Zone")]
    public GameObject SpawnSpeechBubble;
    public GameObject TutorialSpawn;
    [Header("Ultimate Zone")]
    public GameObject UltimateSpeechBubble;
    public GameObject UltimateBrocks;
    public GameObject UltUI;
    [Header("Healing Zone")]
    public GameObject HealingSpeechBubble;
    public GameObject HealingBlocks;
    [Header("EndTutorial")]
    public GameObject WakeUpSpeechBubble;
    public GameObject WakeUpButton;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D Col)
    {
        if(gameObject.tag == "WalkingZone" && Col.CompareTag("Player"))
        {
            Debug.Log("Walked");
            WalkingSpeechBubble.SetActive(false);
            AttackingSpeechBubble.SetActive(true);
            InitialBrock.SetActive(true);
            Destroy(gameObject);
        }

        if(gameObject.tag == "AttackingZone" && Col.CompareTag("Player"))
        {
            Debug.Log("Attack");
            AttackingSpeechBubble.SetActive(false);
            SpawnSpeechBubble.SetActive(true);
            TutorialSpawn.SetActive(true);
            Destroy(gameObject);
        }

        if(gameObject.tag == "SpawnZone" && Col.CompareTag("Player"))
        {
            Debug.Log("Spawned");
            SpawnSpeechBubble.SetActive(false);
            UltUI.SetActive(true);
            UltimateSpeechBubble.SetActive(true);
            UltimateBrocks.SetActive(true);
            Destroy(gameObject);
        }

        if(gameObject.tag == "UltimateZone" && Col.CompareTag("Player"))
        {
            Debug.Log("Ultimate");
            UltimateSpeechBubble.SetActive(false);
            HealingSpeechBubble.SetActive(true);
            HealingBlocks.SetActive(true);
            Destroy(gameObject);
        }

        if(gameObject.tag == "HealingZone" && Col.CompareTag("Player"))
        {
            Debug.Log("Healed");
            HealingSpeechBubble.SetActive(false);
            WakeUpSpeechBubble.SetActive(true);
            WakeUpButton.SetActive(true);
        }
    }
}
