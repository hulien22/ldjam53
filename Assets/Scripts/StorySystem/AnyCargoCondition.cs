using CleverCrow.Fluid.Databases;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Actions.Databases;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;
using System.Linq;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

[CreateMenu("Condition/AnyValidCargo")]
public class AnyCargoCondition : ConditionDataBase {
    [SerializeField]
    private KeyValueDefinitionInt[] variables;
    private IKeyValueData<int> database;
    public override void OnInit(IDialogueController dialogue) {
        database = GlobalDatabaseManager.Instance.Database.Ints;
    }
    public override bool OnGetIsValid(INode parent) {
        int l = (int)LocationManager.GetLocation();
        foreach (KeyValueDefinitionInt variable in variables) {
            if (database.Get(variable.key, variable.defaultValue) == l)
                return true;
        }
        return false;
    }
}

