using CleverCrow.Fluid.Databases;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Actions.Databases;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

[CreateMenu("Condition/ValidCargo")]
public class CargoCondition : ConditionDataBase {
    [SerializeField]
    private KeyValueDefinitionInt variable;
    private IKeyValueData<int> database;
    public override void OnInit(IDialogueController dialogue) {
        database = GlobalDatabaseManager.Instance.Database.Ints;
    }
    public override bool OnGetIsValid(INode parent) {
        Debug.Log(variable);
        int l = (int)LocationManager.GetLocation();
        Debug.Log(l);
        int i = database.Get(variable.key, variable.DefaultValue);
        Debug.Log(i);
        return l == i;
    }
}

