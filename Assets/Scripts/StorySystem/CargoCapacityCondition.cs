using CleverCrow.Fluid.Databases;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Actions.Databases;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;

[CreateMenu("Condition/CargoCapacity")]
public class CargoCapacityCondition : ConditionDataBase {
    [SerializeField]
    private KeyValueDefinitionInt cargoA;
    [SerializeField]
    private KeyValueDefinitionInt cargoB;
    [SerializeField]
    private bool hasCapacity;

    public override bool OnGetIsValid(INode parent) {
        int a = GlobalDatabaseManager.Instance.Database.Ints.Get(cargoA.Key, cargoA.DefaultValue);
        int b = GlobalDatabaseManager.Instance.Database.Ints.Get(cargoB.Key, cargoB.DefaultValue);
        Debug.Log($"cur {CargoState.GetPackageCount()}");
        Debug.Log($"cap {a + b + 3}");
        bool res = CargoState.GetPackageCount() < (a + b + 3);
        return (hasCapacity) ? res : !res;
    }
}

