using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerHealth : MonoBehaviourPunCallbacks
{
    [SerializeField] float startingHealth = 100f;
    [SerializeField] float timesciencelasthit;

    private float timer = 0f;
    [SerializeField]
    private CharacterController characterController;
    private Animator anim;
    [SerializeField]
    private float currentHealth;
    private bool gameover = false;
    [SerializeField]
    private BoxCollider swordColliders;
    [SerializeField]
    private BoxCollider ShieldColliders;
    [SerializeField]
    private Image Healthslider;


    public const byte event_1 = 0;
    public const byte event21 = 1;

    public bool GameOver
    {
        get { return gameover; }
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        currentHealth = startingHealth;
        Healthslider.fillAmount = currentHealth / startingHealth;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon" && !characterController.gameObject.GetComponent<PhotonView>().IsMine)
        {
            float damage = 10f;
            timer = 0;
            characterController.gameObject.GetComponent<PhotonView>().RPC("takehit", RpcTarget.AllBuffered, 10f);
            Debug.Log("hit");
        }
        if (other.tag == "Sheild")
        {

        }
    }
    [PunRPC]
    void takehit(float damage)
    {
        if (currentHealth > 0)
        {
            PlayerHit(currentHealth);
            anim.Play("Get Hit");
            currentHealth -= damage;
            Debug.Log("the hit");
            Healthslider.fillAmount = currentHealth / startingHealth;

        }
        if (currentHealth <= 0)
        {
            killPlayer();
        }

    }

    void killPlayer()
    {
        PlayerHit(currentHealth);
        anim.SetTrigger("death");
        anim.enabled = false;
        gameover = true;
        Debug.Log("death");
        
      //  characterController.gameObject.GetComponent<PhotonView>().RPC("Gameover", RpcTarget.AllBuffered, gameover);
        characterController.enabled = false;

        Gameover(true);


    }

    public void Gameover(bool state)
    {
        UiManager.instance.gameoverpanel.SetActive(state);
        if(currentHealth == 0)
        {
            UiManager.instance.gameovertext.text = "You lost";
        }
        else
        {
            UiManager.instance.gameovertext.text = "You Won";
        }
            



    }






    public void PlayerHit(float currentHp)
    {
        if (currentHp > 0)
        {
            gameover = false;
        }
        else
        {
            gameover = true;
        }
    }

    public void Beginattack()
    {
        swordColliders.enabled = true;
    }

    public void Endattack()
    {
        swordColliders.enabled = false;
    }

    public void Begindefence()
    {
        ShieldColliders.enabled = true;
    }

    public void Enddefence()
    {
        ShieldColliders.enabled = false;
    }

}
