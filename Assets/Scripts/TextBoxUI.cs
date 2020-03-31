using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TextBoxUI : MonoBehaviour {

    public Transform textBoxInner;
    public Transform textBoxOuter;
    public float outerWidth = 0.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        textBoxOuter.localScale = new Vector2(textBoxInner.localScale.x + outerWidth, textBoxInner.localScale.y + outerWidth);
	}
}
