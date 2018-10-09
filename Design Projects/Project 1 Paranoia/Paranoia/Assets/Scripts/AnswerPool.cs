using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerPool : MonoBehaviour {

    public GameObject answerPrefab;

    private Stack<GameObject> inactiveAnswers = new Stack<GameObject>();

    public GameObject GetObject()
    {

        GameObject spawnedgameObject;

        if (inactiveAnswers.Count > 0)
        {
            spawnedgameObject = inactiveAnswers.Pop();
        }
        else
        {
            spawnedgameObject = (GameObject)GameObject.Instantiate(answerPrefab);
            PooledObject pooledObject = spawnedgameObject.AddComponent<PooledObject>();
            pooledObject.pool = this;
        }
        spawnedgameObject.SetActive(true);

        return spawnedgameObject;
    }

    public void ReturnObject(GameObject toReturn)
    {
        PooledObject pooledObject = toReturn.GetComponent<PooledObject>();

        if(pooledObject != null && pooledObject.pool == this)
        {

            toReturn.SetActive(false);

            inactiveAnswers.Push(toReturn);

        }
        else
        {
            Destroy(toReturn);
        }

    }
}
    public class PooledObject : MonoBehaviour
{

    public AnswerPool pool;

}
