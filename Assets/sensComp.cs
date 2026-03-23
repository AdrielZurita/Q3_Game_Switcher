using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class sensComp : MonoBehaviour
{
    public GameObject sensslider;
    public ObjectPlsHelp objectPlsHelp;
    public TextMeshProUGUI sensText;
    public TMP_InputField sensInputField;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        objectPlsHelp.sens = sensslider.GetComponent<UnityEngine.UI.Slider>().value;
        sensText.text = "Currently: " + objectPlsHelp.sens.ToString("F1");
    }

    public void GetInputValue()
    {
        string UserInput = sensInputField.text;
        if (float.TryParse(UserInput, out float result))
        {
            objectPlsHelp.sens = result;
            sensslider.GetComponent<UnityEngine.UI.Slider>().value = result;
        }
        else
        {
            Debug.LogWarning("Invalid input for sensitivity. Please enter a valid number.");
        }
    }
}
