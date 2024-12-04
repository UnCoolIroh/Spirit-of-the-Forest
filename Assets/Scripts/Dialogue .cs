using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

/*
Basic dialogue logic used from Diving Squid video on creating NPC dialogue: https://www.youtube.com/watch?v=1nFNOyCalzo&ab_channel=diving_squid, which I then built on top of.
*/
public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public Text npcNameText;
    public string npcName;
    public string[] dialogue;
    public GameObject contButton;
    private int index;
    public float wordSpeed;
    public bool inRange;
    public bool talkedTo;
    //public GameLogic logic;
    AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

           HandleDialogue();
        
    }

    private void HandleDialogue() {
        if (Input.GetKeyDown(KeyCode.Space) && inRange){
            if (dialoguePanel.activeInHierarchy && contButton.activeInHierarchy) {
                NextLine();
            }
            else if(dialoguePanel.activeInHierarchy && !contButton.activeInHierarchy) {

            }
            else {
                if (!talkedTo) {
                    npcNameText.text = npcName;
                    audioManager.playSFX(audioManager.buttonPress);
                    dialoguePanel.SetActive(true);
                    StartCoroutine(Typing());
                }
            }
        }
        if (dialogueText.text == dialogue[index]) {
            contButton.SetActive(true);
        }
    }

    public void NextLine() {
        if (inRange) {
            audioManager.playSFX(audioManager.buttonPress);
            contButton.SetActive(false);
            if (index < dialogue.Length - 1) {
                index++;
                dialogueText.text = "";
                StartCoroutine(Typing());
            }
            else {
                ZeroText();
                talkedTo = true;
            }
        }
    }

    public void ZeroText() {
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")){
            inRange = true;
        }
    }

    IEnumerator Typing(){
        foreach(char letter in dialogue[index].ToCharArray()) {
            if (!inRange) {
                break;
            }
            audioManager.playSFX(audioManager.typeWriter);
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")){
            inRange = false;
            ZeroText();
        }
    }
}
