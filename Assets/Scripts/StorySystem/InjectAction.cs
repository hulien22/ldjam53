using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CleverCrow.Fluid.Dialogues.Nodes;
using Unity.VisualScripting;

namespace Assets.Scripts.StorySystem {
    [CreateMenu("Action/GetRandomJob")]
    public class InjectAction : ActionDataBase {
        [SerializeField]
        private string _text = null;
        [SerializeField]
        private NodeDialogueData node = null;

        public override void OnStart() {
            node.dialogue = _text;
            node.SetDirty();
            node.Serialize();
            Debug.Log(_text);
        }
    }
}
