using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Mechadroids.UI {
    public class UIElementReference : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI nameField;
        [SerializeField] private TMP_InputField [] valueField;

        public void SetName(string name) {
            nameField.text = name;
        }

        public void SetValue<T>(T fieldValue) {
            this.valueField[0].text = valueField.ToString();
        }

        public void SetValue<T>(T [] fieldValue) {
            if (fieldValue.Length != 3) {
                Debug.LogError("Field value must be of length 3");
                return;
            }

            for (int i = 0; i < 3; i++) {
                this.valueField[i].text = fieldValue[i].ToString();
            }
        }

        public void GetValue<T>(out T fieldValue) {
            fieldValue = (T)System.Convert.ChangeType(valueField[0].text, typeof(T));
        }

        public void GetValue<T>(out T [] fieldValue) {
            fieldValue = new T[3];
            for (int i = 0; i < 3; i++) {
                fieldValue[i] = (T)System.Convert.ChangeType(valueField[i].text, typeof(T));
            }
        }
    }
}
