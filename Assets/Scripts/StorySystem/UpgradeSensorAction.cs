using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues;
using UnityEngine;
using CleverCrow.Fluid.Databases;


[CreateMenu("Action/UpgradeSensor")]
public class UpgradeSensorAction : ActionDataBase {
    [SerializeField]
    private KeyValueDefinitionBool variable;
    [SerializeField]
    private bool value;

    public override ActionStatus OnUpdate() {
        GlobalDatabaseManager.Instance.Database.Bools.Set(variable.Key, value);
        UpgradeManager.SetSensor();
        return base.OnUpdate();
    }
}