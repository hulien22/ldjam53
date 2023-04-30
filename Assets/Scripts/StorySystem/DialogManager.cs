using Assets.Scripts.StorySystem;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Choices;
using CleverCrow.Fluid.Dialogues.Graphs;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
    public static DialogManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("Duplicate instance of dialog manager.");
            return;
        }
        Instance = this;
 
        controller = new DialogueController(new DatabaseInstanceExtended());
        // @NOTE If you don't need audio just call _ctrl.Events.Speak((actor, text) => {}) instead
        controller.Events.SpeakWithAudio.AddListener(OnSetDialog);
        controller.Events.Choice.AddListener(OnSetChoices);
        controller.Events.End.AddListener(EndDialog);

        controller.Events.NodeEnter.AddListener((node) => {
            //Debug.Log($"Node Enter: {node.GetType()} - {node.UniqueId}");
        });

        //controller.Play(dialogues[0]);//, gameObjectOverrides.ToArray<IGameObjectOverride>()
    }

    private DialogueController controller;

    public DialogueGraph[] dialogues;

    public GameObject speakerContainer;
    public Image portrait;
    public TMP_Text portaitLabel;
    public TMP_Text textLabel;

    public RectTransform choiceList;
    public ChoiceButton choicePrefab;

    private void ClearChoices() {
        foreach (Transform child in choiceList) {
            Destroy(child.gameObject);
        }
    }

    private IEnumerator NextDialogue() {
        yield return null;

        while (!Input.GetMouseButtonDown(0)) {
            yield return null;
        }

        controller.Next();
    }

    public void StartDialog(int planet) {
        speakerContainer.SetActive(true);
        controller.Play(dialogues[planet]);
    }

    public void EndDialog() {
        speakerContainer.SetActive(false);
    }

    public void OnSetDialog(IActor actor, string text, AudioClip audioClip) {
        Debug.Log("here");
        if (audioClip) Debug.Log($"Audio Clip Detected ${audioClip.name}");
        AudioManager.PlaySound(audioClip);
        ClearChoices();
        SetDialog(actor, text);

        StartCoroutine(NextDialogue());
    }

    public void OnSetChoices(IActor actor, string text, List<IChoice> choices) {
        ClearChoices();
        SetDialog(actor, text);

        choices.ForEach(c => {
            var choice = Instantiate(choicePrefab, choiceList);
            choice.title.text = c.Text;
            choice.clickEvent.AddListener(controller.SelectChoice);
        });
    }

    private void SetDialog(IActor actor, string text) {
        portrait.sprite = actor.Portrait;
        portaitLabel.text = actor.DisplayName;
        textLabel.text = text;
    }

    private void Update() {
        controller.Tick();
    }

    public void SelectOption(int option) {

    }
}
