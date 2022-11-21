using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;


public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    
    [SerializeField]
    private Button Attack1;
    [SerializeField]
    private Button Attack2;
    [SerializeField]
    private Button Shield;
    [SerializeField]
    public GameObject gameoverpanel;
    [SerializeField]
    public Button Exit;
    public Text gameovertext;
    // Start is called before the first frame update
    void Start()
    {
        Attack1.onClick.AddListener(PlayerMoevenet.instance.Attacks1);
        Attack2.onClick.AddListener(PlayerMoevenet.instance.Attacks2);
        Shield.onClick.AddListener(PlayerMoevenet.instance.Defence);
        gameoverpanel.SetActive(false);
        Exit.onClick.AddListener(exitgame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void exitgame()
    {
        SceneManager.LoadScene(0);
        PhotonNetwork.LeaveRoom();
    }


}
