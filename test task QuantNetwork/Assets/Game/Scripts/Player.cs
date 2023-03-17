using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private TextMeshProUGUI coinsCollectedText;
    [SerializeField] private ParticleSystem explode;

    private Animator animator;
    private bool UpButtonIsPressed;
    private bool DownButtonIsPressed;
    private float duration = .5f;
    private float forceUp = 7.7f;
    private float forceDown = 3.7f;
    private Vector2 _moveUp = new Vector2(0,1);
    private Vector2 _moveDown = new Vector2(0,-1);
    public int coinsCollected;


    
    private void Start() 
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        InputManager();
    }

    void InputManager()
    {
        if (UpButtonIsPressed == false && DownButtonIsPressed == false && _rb.velocity.y < 4.5f)
        {
            FreeFall();
        }
        
        if (DownButtonIsPressed && UpButtonIsPressed == true)
        {
            MoveForward();
        }
        else
        {
            if (UpButtonIsPressed)
            {
                MoveUp();
            }
            if (DownButtonIsPressed == true)
            {
                MoveDown();
            }
        }
    }

    void FreeFall()
    {
        float currentValue = GetComponent<Animator>().GetFloat("RotationValue");
        float t = Mathf.Clamp01(Time.deltaTime / (duration * 1.5f));
        GetComponent<Animator>().SetFloat("RotationValue", Mathf.Lerp(currentValue, 1f, t));
        _rb.AddForce(1.2f * _moveDown, ForceMode2D.Force);
        Debug.Log("FALL");
    }
    
    void MoveForward()
    {
        _rb.AddForce(7.1f * _moveUp, ForceMode2D.Force);
        _rb.AddForce(3.7f * _moveDown, ForceMode2D.Force);
    }
    public void MoveUp()
    {
        _rb.AddForce(forceUp * _moveUp, ForceMode2D.Force);
        float currentValue = GetComponent<Animator>().GetFloat("RotationValue");
        float t = Mathf.Clamp01(Time.deltaTime / duration);
        GetComponent<Animator>().SetFloat("RotationValue", Mathf.Lerp(currentValue, 0f, t));
        Debug.Log("UP");
    }

    public void MoveDown()
    {
        _rb.AddForce(forceDown * _moveDown, ForceMode2D.Force);
        float currentValue = GetComponent<Animator>().GetFloat("RotationValue");
        float t = Mathf.Clamp01(Time.deltaTime / duration);
        GetComponent<Animator>().SetFloat("RotationValue", Mathf.Lerp(currentValue, 1f, t));
        Debug.Log("DOWN");
    }

    public void UpButtonOnPointerDown()
    {
        UpButtonIsPressed = true;
    }
    public void UpButtonOnPointerUp()
    {
        UpButtonIsPressed = false;
    } 

    public void DownButtonOnPointerDown()
    {
        DownButtonIsPressed = true;
    }
    public void DownButtonOnPointerUp()
    {
        DownButtonIsPressed = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Instantiate(explode, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            explode.Play();
            Destroy(gameObject);
            GameManager.instance.OnLose();
        }
        else if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coinsCollected++;
            coinsCollectedText.text = "Coins: " + coinsCollected.ToString();
        }
    }
}

