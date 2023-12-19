using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    [SerializeField]
    private CloudMover cloudPrefab;

    [SerializeField]
    private int cloud_count;

    private List<CloudMover> cloudPoolActive;
    private List<CloudMover> cloudPoolPassive;
    private void Awake()
    {
        cloudPoolActive = new();
        cloudPoolPassive = new();
    }

    private void Start()
    {

        List<int> startLocations = new();
                

        for (int i = 0; i < cloud_count; i++)
        {
            startLocations.Add(i * 2);
        }

        foreach (var location_X in startLocations)
        {
            CloudMover cloud=Instantiate(cloudPrefab,new Vector3(location_X,Random.Range(22f,27f),0),Quaternion.Euler(0,0,90));
            cloud.transform.localScale = Random.Range(0, 2) == 1 ? Vector3.one : new Vector3(-1, 1, 1);
            cloudPoolActive.Add(cloud);
            cloud.EndOfLifeEvent.AddListener(MoveCloudToPassivePool);            
        }
    }


    private void MoveCloudToPassivePool(CloudMover cloud)
    {
        cloudPoolActive.Remove(cloud);
        cloudPoolPassive.Add(cloud);
    }

    private void Update()
    {
        if(cloudPoolActive.Count < cloud_count - 5)
        {
            foreach (var passiveCloud in cloudPoolPassive)
            {
                Vector2 randomPositionOnScreen=Camera.main.ViewportToWorldPoint(new Vector2(1+Random.Range(0,0.1f), 0));
                passiveCloud.transform.position = new Vector3(randomPositionOnScreen.x, Random.Range(22f, 27f), 0);
                passiveCloud.gameObject.SetActive(true);
                cloudPoolPassive.Remove(passiveCloud);
                cloudPoolActive.Add(passiveCloud);

                if(cloudPoolActive.Count>cloud_count-5)   break;
            }
        }
    }
}
