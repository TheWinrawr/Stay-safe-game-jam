using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCutscene : MonoBehaviour {

    public TextMesh[] textArray;
    public float textFadeInTime = 0.5f;

    private int textIndex;
    private float timeUntilFadeIn;

    private void Start() {
        textIndex = 0;

        for (int i = 0; i < textArray.Length; i++) {
            Color clear = textArray[i].color;
            clear.a = 0;
            textArray[i].color = clear;
        }

        timeUntilFadeIn = Time.time + textFadeInTime + 1f;
    }

    private void Update() {
        if (Input.anyKeyDown && Time.time > timeUntilFadeIn) {
            textIndex++;
            if (textIndex >= textArray.Length) {
                SceneManager.LoadScene("test scene", LoadSceneMode.Single);
            }

            timeUntilFadeIn = Time.time + textFadeInTime;
        }

        if (textIndex < textArray.Length) {
            FadeInText();
        }
    }

    private void FadeInText() {
        Color textColor = textArray[textIndex].color;
        float t = textFadeInTime > 0 ? (timeUntilFadeIn - Time.time) / textFadeInTime : 0;
        textColor.a = Mathf.Lerp(1, 0, t);
        textArray[textIndex].color = textColor;
    }
}
