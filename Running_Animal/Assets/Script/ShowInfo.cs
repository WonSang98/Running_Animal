using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInfo : MonoBehaviour
{
    public Text Text_Dia;
    public Text Text_Gold;
    public Text Text_Forest;
    public Text Text_Desert;
    public Text Text_Arctic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        show();
    }

    void show()
    {
        Text_Dia.text = $"{GameManager.Data.Cash}��";
        Text_Gold.text = $"{GameManager.Data.Gold}��";
        Text_Forest.text = $"{GameManager.Data.Money_Forest}��";
        Text_Desert.text = $"{GameManager.Data.Money_Desert}��";
        Text_Arctic.text = $"{GameManager.Data.Money_Arctic}��";
    }
}
