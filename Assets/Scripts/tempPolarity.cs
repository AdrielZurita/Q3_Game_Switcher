using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class tempPolarity : MonoBehaviour
{
    public ObjectPlsHelp objectPlsHelp;
    public TextMeshProUGUI polarityText;
    public TextMeshProUGUI chargeText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (objectPlsHelp.isPositive == true)
        {
            polarityText.text = "+";
            polarityText.color = Color.red;
        }
        else
        {
            polarityText.text = "-";
            polarityText.color = Color.blue;
        }
        chargeText.text = "Charge: " + objectPlsHelp.chargeAmount.ToString("F2");
    }
}
