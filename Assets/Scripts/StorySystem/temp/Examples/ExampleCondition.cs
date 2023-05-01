using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;



public class ExampleCondition : ConditionDataBase {
    [SerializeField]
    private bool _isValid;

    public override bool OnGetIsValid (INode parent) {
        Debug.Log($"Example Condition: Returned {_isValid} for node {parent.UniqueId}");
        return _isValid;
    }
}

