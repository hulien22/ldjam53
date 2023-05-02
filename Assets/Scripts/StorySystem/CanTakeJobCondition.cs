using Assets.Scripts.StorySystem;
using CleverCrow.Fluid.Databases;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Actions.Databases;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;
// using static UnityEditor.FilePathAttribute;

[CreateMenu("Condition/CanTakeJob")]
public class CanTakeJobCondition : ConditionDataBase {
    private IKeyValueData<int> database;
    [SerializeField]
    private bool canTake;
    public override void OnInit(IDialogueController dialogue) {
        database = GlobalDatabaseManager.Instance.Database.Ints;
    }
    public override bool OnGetIsValid(INode parent) {
        KeyValueDefinitionInt locationItem = JobsGenerator.GetPlanet(LocationManager.GetLocation()).SidequestItem;
        if (database.Get(locationItem.key, locationItem.DefaultValue) != 0) {
            return !canTake;
        }
        return canTake;
    }
}
