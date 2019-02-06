using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resources : MonoBehaviour
{
    public int maxStoneResource;
    public int maxWaterResource;
    public int maxHerbResource;
    public int maxWoodResource;

    public int currentWood;
    public int currentWater;
    public int currentHerb;
    public int currentStone;

    [Header("Current Resources")]
    public Text stone;
    public Text wood;
    public Text Herb;
    public Text water;

    // Start is called before the first frame update
    void Start()
    {
        currentHerb = 0;
        currentStone = 0;
        currentWater = 0;
        currentWood = 0;
    }

    // Update is called once per frame
    void Update()
    {
        stone.text = ("Stone: " + currentStone);
        wood.text = ("Wood: " + currentWood);
        Herb.text = ("Herb: " + currentHerb);
        water.text = ("Water: " + currentWater);
    }


}
