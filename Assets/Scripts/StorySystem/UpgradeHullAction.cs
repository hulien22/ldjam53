using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues;
using UnityEngine;
using CleverCrow.Fluid.Databases;


[CreateMenu("Action/UpgradeHull")]
public class UpgradeHullAction : ActionDataBase {
    [SerializeField]
    private KeyValueDefinitionInt variable;
    [SerializeField]
    private int value;

    public override ActionStatus OnUpdate() {
        GlobalDatabaseManager.Instance.Database.Ints.Set(variable.Key, value);
        UpgradeManager.SetHull(value);
        return base.OnUpdate();
    }
}