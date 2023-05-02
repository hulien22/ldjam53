using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditsText : MonoBehaviour {
    public static CreditsText instance;
    [SerializeField] private TMP_Text text;

    private void Awake() {
        instance = this;
    }
    void Start() {
        text = GetComponent<TMP_Text>();
    }

    public static void SetText(int credits) {
        instance.text.text = $"{credits}¢";
    }
}
