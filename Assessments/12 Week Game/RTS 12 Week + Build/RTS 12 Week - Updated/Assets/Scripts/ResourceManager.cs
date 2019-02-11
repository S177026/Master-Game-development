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

    public float maxHerb = 1000000f;
    public float maxWood = 1000000f;
    public float maxGold = 1000000f;
    public float maxCables = 1000000f;

    public Text herbDis;
    public Text woodDis;
    public Text goldDis;
    public Text cableDis;

    public Civilian Civi; 

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
