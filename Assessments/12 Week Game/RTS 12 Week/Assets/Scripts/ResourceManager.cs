using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public float herb;
    public float wood;
    public float gold;
    public float cables;

    public float maxHerb;
    public float maxWood;
    public float maxGold;
    public float maxCables;

    public Text herbDis;
    public Text woodDis;
    public Text goldDis;
    public Text cableDis;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        herbDis.text = "" + herb + "/" + maxHerb;
        woodDis.text = "" + wood + "/" + maxWood;
        goldDis.text = "" + gold + "/" + maxGold;
        cableDis.text = "" + cables + "/" + maxCables;
    }
}
