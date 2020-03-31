using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour, IInteractable {

    public GameObject textBox;
    public TextMesh textMesh;

    public float textSpeed = 0.1f;
    public string[] textArray;

    public UnityEvent OnDialogueEnter;
    public UnityEvent OnDialogueExit;

    private Player player;
    private PlayerInput input;

    private bool isTalking;
    private int currTextIndex;
    private int currCharIndex;
    private float nextCharTime;

    void Awake() {
        if (textBox == null) {
            textBox = transform.Find("TextBox").gameObject;
        }
        if (textMesh == null) {
            textMesh = GetComponentInChildren<TextMesh>();
        }
        player = FindObjectOfType<Player>();
        input = FindObjectOfType<PlayerInput>();
    }

	// Use this for initialization
	void Start () {
		if (OnDialogueEnter == null) {
            OnDialogueEnter = new UnityEvent();
        }
        if (OnDialogueExit == null) {
            OnDialogueExit = new UnityEvent();
        }

        textBox.SetActive(false);
        textMesh.gameObject.SetActive(false);
        isTalking = false;

        input.OnInteractKeyPressed += OnInteractKeyPressed;

        for (int i = 0; i < textArray.Length; i++) {
            textArray[i] = textArray[i].Replace("\\n", "\n");
        }
    }

    // Starts the dialogue
    public void DoAction() {
        if (textArray.Length == 0) {
            return;
        }
        OnDialogueEnter.Invoke();
        player.DisableInput();
        textMesh.text = "";
        currTextIndex = 0;
        nextCharTime = Time.time + textSpeed;
        textBox.SetActive(true);
        textMesh.gameObject.SetActive(true);
        isTalking = true;
    }
	
	// Update is called once per frame
	private void Update () {
		if (isTalking) {
            DisplayText();
            AnimateText();
            CheckIfDialogueReachedEnd();
        }
    }

    private void CheckIfDialogueReachedEnd() {
        if (currTextIndex >= textArray.Length) {
            isTalking = false;
            textBox.SetActive(false);
            textMesh.gameObject.SetActive(false);
            OnDialogueExit.Invoke();
            player.EnableInput();
        }
    }

    private void AnimateText() {
        if (Time.time >= nextCharTime && currCharIndex < textArray[currTextIndex].Length) {
            currCharIndex++;
            nextCharTime = Time.time + textSpeed;
        }
    }

    private void DisplayText() {
        if (currTextIndex < textArray.Length) {
            string displayText = textArray[currTextIndex].Substring(0, currCharIndex);
            textMesh.text = displayText;
        }
    }

    void OnInteractKeyPressed() {
        if (!isTalking) {
            return;
        }

        if (currCharIndex < textArray[currTextIndex].Length) {
            currCharIndex = textArray[currTextIndex].Length;
        }
        else {
            currTextIndex++;
            currCharIndex = 0;
            nextCharTime = Time.time + textSpeed;
        }
    }
}
