
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Personaje : MonoBehaviour
{

    public float velocidad;
    public DamageHitboxController damageHBController;
    Vector2 movement;
    public Rigidbody2D rig;
    public Animator anim;
    private SpriteRenderer spritePersonaje;
    public GameObject inventarioCanvas;
    public GameObject menuPausa;
    public AudioSource ataque;

    bool enEsquiva;
    bool puedeDash = true;
    public TrailRenderer dashTrail;

    public Vector3 entranceDirection;
    public Vector3 entrancePosition;
    public bool clearEntrance;
    private void Awake()
    {

        spritePersonaje = GetComponentInChildren<SpriteRenderer>();

    }

    private void Start()
    {
        /*if (clearEntrance)
        {
            PlayerPrefs.DeleteKey(SceneManager.GetActiveScene().name);
        }*/

        if(SaveManager.Instancia.loadingData && SceneManager.GetActiveScene().buildIndex == SaveManager.Instancia.datosGuardados.sceneIndex)
        {
            if (SaveManager.Instancia.datosGuardados.spawnPosition != Vector3.zero )
            {
                transform.position = SaveManager.Instancia.datosGuardados.spawnPosition;
            }

        }
        else
        {
            if(PlayerPrefs.HasKey(SceneManager.GetActiveScene().name))
            {
                Vector3 pos = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString(SceneManager.GetActiveScene().name));

                if (pos != Vector3.zero)
                {
                    transform.position = pos;
                    entrancePosition = pos;
                }
            }
        }
        SaveManager.Instancia.loadingData = false;


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        anim.SetFloat("ultimoHorizontal", 1);
        anim.SetFloat("ultimoVertical", 0);

        Time.timeScale = 1;

    }


    void Update()
    {
        if (HeroeDamageable.isAlive)
        {

            if (!Dialogue.didDialogueStart)
            {


                if (Input.GetMouseButtonDown(0))
                {
                    anim.SetTrigger("Attack");
                    ataque.Play();
                }

                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");

                anim.SetFloat("Horizontal", movement.x);
                anim.SetFloat("Vertical", movement.y);
                anim.SetFloat("speed", movement.sqrMagnitude);

                if (movement.x != 0 || movement.y != 0)
                {
                    anim.SetFloat("ultimoHorizontal", Input.GetAxisRaw("Horizontal"));
                    anim.SetFloat("ultimoVertical", Input.GetAxisRaw("Vertical"));

                }
                if (Input.GetKeyDown(KeyCode.I))
                {
                    if (inventarioCanvas.activeSelf)
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                        Time.timeScale = 1;

                    }
                    else
                    {
                        Time.timeScale = 0;
                        Cursor.lockState = CursorLockMode.Confined;
                        Cursor.visible = true;
                    }
                    inventarioCanvas.SetActive(!inventarioCanvas.activeSelf);
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (menuPausa.activeSelf)
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                        Time.timeScale = 1;

                    }
                    else
                    {
                        Time.timeScale = 0;
                        Cursor.lockState = CursorLockMode.Confined;
                        Cursor.visible = true;
                    }
                    menuPausa.SetActive(!menuPausa.activeSelf);
                }

                if (Input.GetMouseButtonDown(1) && puedeDash && !HeroeDamageable.isDamaged)
                {
                    if (!enEsquiva)
                    {
                        StartCoroutine(Esquivar());
                        puedeDash = false;
                    }
                }

            }else
            {
                movement.x = 0;
                movement.y = 0;
            }

        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            movement.x = 0;
            movement.y = 0;

        }


    }

    private void FixedUpdate()
    {
        if (!HeroeDamageable.isDamaged && !enEsquiva)
            rig.MovePosition(rig.position + movement * velocidad * Time.deltaTime);
    }

    public void AttackStart()
    {
        damageHBController.ActivateHitBox(anim.GetFloat("ultimoHorizontal"), anim.GetFloat("ultimoVertical"));
    }

    public void AttackStop()
    {
        damageHBController.DesactivateHitBox();
    }


    IEnumerator Esquivar()
    {
        enEsquiva = true;

        dashTrail.enabled = true;

        float duracionEsquiva = 0.1f;

        float lastInputX = anim.GetFloat("ultimoHorizontal");
        float lastInputY = anim.GetFloat("ultimoVertical");
        Vector2 finalVel = Vector2.zero;
        if (lastInputX > 0)
        {
            finalVel = new Vector2(-1, 0);
        }
        else if (lastInputX < 0)
        {
            finalVel = new Vector2(1, 0);
        }
        else if (lastInputY < 0)
        {
            finalVel = new Vector2(0, 1);
        }
        else if (lastInputY > 0)
        {
            finalVel = new Vector2(0, -1);
        }
        while (duracionEsquiva > 0)
        {

            rig.velocity = finalVel * 20;

            yield return new WaitForSeconds(Time.deltaTime);

            duracionEsquiva -= Time.deltaTime;
        }

        Invoke("ResetDash", 1f);

        dashTrail.enabled = false;

        enEsquiva = false;

    }

    public void ResetDash()
    {
        puedeDash = true;
    }

    private void OnDestroy()
    {
      
        PlayerPrefs.SetString(SceneManager.GetActiveScene().name, JsonUtility.ToJson(entrancePosition));
    }
}

