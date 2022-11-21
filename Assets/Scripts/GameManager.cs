using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerprefab;
  
    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.IsConnectedAndReady)
        {
            int randomnumer = Random.Range(-10, 10);
            PhotonNetwork.Instantiate(playerprefab.name, new Vector3(randomnumer,0,randomnumer),Quaternion.identity);
        }
        
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
