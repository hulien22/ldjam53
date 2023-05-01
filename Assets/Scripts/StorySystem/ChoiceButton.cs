using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Assets.Scripts.StorySystem {
    public class ChoiceButton : MonoBehaviour {
        public TMP_Text title;
        public Button button;
        public UnityEvent<int> clickEvent = new ActivateChoiceIndexEvent();

        private class ActivateChoiceIndexEvent : UnityEvent<int> {
        }

        private void Awake() {
            button.onClick.AddListener(() => {
                clickEvent.Invoke(transform.GetSiblingIndex());
            });
        }
    }
}
