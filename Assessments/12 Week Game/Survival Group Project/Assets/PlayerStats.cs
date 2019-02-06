using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    public float maxOxygen;
    public float maxHeat;

    public float currentOxygen;
    public float currentHeat;

    public float oxygenLoss = 2.5f;
    public float heatLoss = 5f;

    public CircleCollider2D col;

    public bool isSafe;

    public float maxStoneResource;
    public float maxWaterResource;
    public float maxHerbResource;
    public float maxWoodResource;

    public float currentWood;
    public float currentWater;
    public float currentHerb;
    public float currentStone;

    [Header("Current Resources")]
    public Text stone;
    public Text wood;
    public Text Herb;
    public Text water;

    [Header("Resource Gain")]
    public float woodGain = 2;
    public float stoneGain = 2;
    public float herbGain = 5;
    public float waterGain = 5;

    public bool isCollectingWood;
    // Start is called before the first frame update
    void Start()
    {
        maxOxygen = 100;
        maxHeat = 100;

        isCollectingWood = false;

        currentOxygen = maxOxygen;
        currentHeat = maxHeat;

        isSafe = true;

        currentHerb = 0;
        currentStone = 0;
        currentWater = 0;
        currentWood = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isSafe)
        {
            SafeZone();
        }

        if (isCollectingWood)
        {
            CollectingWood();
        }

        stone.text = "Stone: " + currentStone.ToString();
        wood.text = "Wood: " + currentWood.ToString();
        Herb.text = "Herb: " + currentHerb.ToString();
        water.text = "Water: " + currentWater.ToString();
    }

    public void SafeZone()
    {
        currentOxygen -= oxygenLoss * Time.deltaTime;
        currentHeat -= heatLoss * Time.deltaTime;
    }

    public void CollectingWood()
    {
        currentWood += woodGain * Time.deltaTime;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isSafe = false;

        if (collision.gameObject.tag == "Tree")
        {
            isCollectingWood = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SafeZone")
        {
            isSafe = true;
            currentOxygen = maxOxygen;
            currentHeat = maxHeat;
        }
        else if(collision.gameObject.tag == "Tree")
        {

            isCollectingWood = true;

        }

    }
}
