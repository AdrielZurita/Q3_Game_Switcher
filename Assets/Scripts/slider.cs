using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slider : MonoBehaviour
{
    public GameObject sensslider;
    public ObjectPlsHelp objectPlsHelp;
    // Start is called before the first frame update
    void Start()
    {
        sensslider = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        objectPlsHelp.sens = sensslider.GetComponent<UnityEngine.UI.Slider>().value;
    }
}
