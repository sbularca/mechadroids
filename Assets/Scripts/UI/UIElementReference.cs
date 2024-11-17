using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Mechadroids.UI {
    public class UIElementReference : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI nameField;
        [SerializeField] private TMP_InputField [] valueField;

        public TMP_InputField [] InputFields => valueField;

        public void SetName(string name) {
            nameField.text = name;
        }

        public void SetValue<T>(T fieldValue) {
            valueField[0].text = fieldValue.ToString();
        }

        public void SetValue<T>(T [] fieldValue) {
            if (fieldValue.Length != 3) {
                Debug.LogError("Field value must be of length 3");
                return;
            }

            for (int i = 0; i < 3; i++) {
                valueField[i].text = fieldValue[i].ToString();
            }
        }
    }
}
