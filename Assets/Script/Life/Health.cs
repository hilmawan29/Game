using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    private float timer = 30f;
    private bool setTimer = false;
    private bool setriger = false;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            //Player Hurt
            anim.SetTrigger("hurt");
        } else
        {
            if(!dead)
            {
                setTimer = true;
                isDead();
            }
           
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private void Update()
    {
        Debug.Log(timer);
        isDead();
        if(Input.GetKeyDown(KeyCode.E))
            TakeDamage(1);
    }


    private void isDead(){

        if (setTimer == true)
        {
            anim.SetTrigger("die");
            timer -= Time.deltaTime * 15;
            if(timer < 1f)
            {
                setriger = true;
                
            }
        }

        if (setriger == true)
        {
                //Player Dead
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
                SceneManager.LoadScene(0);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Saw")
        {
            //setTimer = true;
        }
    }
}
