using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues;
using UnityEngine;
using CleverCrow.Fluid.Databases;


[CreateMenu("Action/UpgradeFuel")]
public class UpgradeFuelAction : ActionDataBase {
    [SerializeField]
    private KeyValueDefinitionInt variable;
    [SerializeField]
    private int value;

    public override ActionStatus OnUpdate() {
        Debug.LogError(value);
        GlobalDatabaseManager.Instance.Database.Ints.Set(variable.Key, value);
        UpgradeManager.SetFuelCapacity(value);
        return base.OnUpdate();
    }
}