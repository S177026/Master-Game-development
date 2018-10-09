using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour {

    public RoundData[] allData;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public RoundData GetRoundData()
    {
        return allData[0];
    }
}
