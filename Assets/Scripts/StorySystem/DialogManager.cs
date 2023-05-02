using Assets.Scripts.StorySystem;
using CleverCrow.Fluid.Databases;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Choices;
using CleverCrow.Fluid.Dialogues.Graphs;
using CleverCrow.Fluid.Dialogues.Nodes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            //node.
            //Debug.Log($"Node Enter: {node.GetType()} - {node.UniqueId}");
        });


        //controller.Play(dialogues[0]);//, gameObjectOverrides.ToArray<IGameObjectOverride>()
    }

    private void Start() {
        StartTutorial();
    }

    public Rocket rocket;


    private DialogueController controller;

    public DialogueGraph[] dialogues;

    public GameObject speakerContainer;
    public Image portrait;
    public TMP_Text portaitLabel;
    public TMP_Text textLabel;

    public RectTransform choiceList;
    public ChoiceButton choicePrefab;
    public DialogueGraph tutorial;

    [Header("Job Injection")]
    [SerializeField] private NodeDialogueData acquiredItem;
    [SerializeField] private NodeDialogueData jobBoard;
    [SerializeField] private NodeDialogueData alreadyHasJob;
    [SerializeField] private NodeDialogueData easyJob;
    [SerializeField] private NodeDialogueData mediumJob;
    [SerializeField] private NodeDialogueData hardJob;

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

    private void StartTutorial() {
        LocationManager.SetLocation(Location.Terrus);
        speakerContainer.SetActive(true);
        controller.Play(tutorial);
    }

    public void StartDialog(Location planet) {
        rocket.SetControls(false);
        if (!GlobalDatabaseManager.Instance.Database.Bools.Get("completedIntro", false)) return;
        AudioManager.LowerBackground();
        LocationManager.SetLocation(planet);
        if (JobsGenerator.GetPlanet(planet) != null) {
            SetupJobDialog(planet);
        }
        speakerContainer.SetActive(true);
        controller.Play(dialogues[(int)planet]);
    }

    private void SetupJobDialog(Location planet) {
        Location easy = JobsGenerator.GetJob(planet, JobsGenerator.Difficulty.Easy);
        Location medium = JobsGenerator.GetJob(planet, JobsGenerator.Difficulty.Medium);
        Location hard = JobsGenerator.GetJob(planet, JobsGenerator.Difficulty.Hard);
        string request = JobsGenerator.GetTextForRequest(easy, medium, hard);
        ActorDefinition actor = JobsGenerator.GetActor(planet);
        jobBoard.dialogue = request;
        jobBoard.actor = actor;
        //easy
        jobBoard.Choices[0].text = JobsGenerator.GetTextForJob(easy);
        jobBoard.Choices[0].ClearConnectionChildren();
        jobBoard.Choices[0].AddConnectionChild(easyJob);
        easyJob.dialogue = JobsGenerator.GetTextForAcceptance(easy);
        (easyJob.enterActions.First() as AcquireCargoAction).Target = easy;
        //medium
        jobBoard.Choices[1].text = JobsGenerator.GetTextForJob(medium);
        jobBoard.Choices[1].ClearConnectionChildren();
        jobBoard.Choices[1].AddConnectionChild(mediumJob);
        mediumJob.dialogue = JobsGenerator.GetTextForAcceptance(medium);
        (mediumJob.enterActions.First() as AcquireCargoAction).Target = medium;
        //hard
        jobBoard.Choices[2].text = JobsGenerator.GetTextForJob(hard);
        jobBoard.Choices[2].ClearConnectionChildren();
        jobBoard.Choices[2].AddConnectionChild(hardJob);
        hardJob.dialogue = JobsGenerator.GetTextForAcceptance(hard);
        (hardJob.enterActions.First() as AcquireCargoAction).Target = hard;
        //already has job
        alreadyHasJob.dialogue = JobsGenerator.GetTextForAlreadyHaveJob(planet);
        alreadyHasJob.actor = actor;
        //acquired item
        acquiredItem.dialogue = JobsGenerator.GetTextForAcquired(planet);
    }

    public void EndDialog() {
        speakerContainer.SetActive(false);
        rocket.SetControls(true);
        AudioManager.RaiseBackground();
    }

    public void OnSetDialog(IActor actor, string text, AudioClip audioClip) {
        Debug.Log("here");
        if (audioClip) Debug.Log($"Audio Clip Detected ${audioClip.name}");
        AudioManager.PlayVoice(VoiceManager.GetVoice(actor));
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
        AudioManager.PlayVoice(VoiceManager.GetVoice(actor));
    }

    private void Update() {
        controller.Tick();
    }

    public void SelectOption(int option) {

    }
}
