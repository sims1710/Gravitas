using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GravityControl : MonoBehaviour
{
    public Slider gravitySlider;
    public Toggle gravityToggle;
    public Rigidbody2D astronaut;
    public List<Rigidbody2D> spikes;
    public List<Rigidbody2D> cacti;
    public List<Rigidbody2D> flowers;
    public List<Rigidbody2D> randomObjects;

    public float moveSpeed = 5f;
    private float maxLiftHeight = 15f;

    private float astronautLiftForce = 20f;
    private float spikeLiftForce = 9f;
    private float cactusLiftForce = 30f;
    private float flowerLiftForce = 45f;
    private float randomObjectLiftForce = 45f;

    private Dictionary<Rigidbody2D, Vector2> initialPositions = new Dictionary<Rigidbody2D, Vector2>();

    private float moveInput;
    private Rigidbody2D rb;

    private int toggleCount = 0;
    private bool uiHidden = false; 
    private bool isGravityUp = false;

    private AudioSource audioSource;
    public AudioClip toggleOnSound;

    void Start()
    {
        rb = astronaut.GetComponent<Rigidbody2D>();
        StoreInitialPosition(astronaut);
        StoreInitialPositions(spikes);
        StoreInitialPositions(cacti);
        StoreInitialPositions(flowers);
        StoreInitialPositions(randomObjects);

        gravitySlider.interactable = gravityToggle.isOn;
        gravityToggle.onValueChanged.AddListener(ToggleGravitySlider);

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        if (isGravityUp)
        {
            Physics2D.gravity = new Vector2(0, 1000f); 
        }
        else
        {
            Physics2D.gravity = new Vector2(0, -10f); 
        }

        if (!gravityToggle.isOn)
        {
            rb.gravityScale = 1;
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            ApplyGravityControl(astronaut, astronautLiftForce, moveInput);
            ApplyGravityControl(spikes, spikeLiftForce);
            ApplyGravityControl(cacti, cactusLiftForce);
            ApplyGravityControl(flowers, flowerLiftForce);
            ApplyGravityControl(randomObjects, randomObjectLiftForce);
        }
    }

    void ApplyGravityControl(Rigidbody2D obj, float liftForce, float moveInput = 0)
    {
        if (obj == null) return;
        obj.gravityScale = 0;
        float targetY = initialPositions[obj].y + (gravitySlider.value * liftForce);
        targetY = Mathf.Min(targetY, initialPositions[obj].y + maxLiftHeight);
        Vector2 newPosition = new Vector2(obj.position.x + moveInput * moveSpeed * Time.fixedDeltaTime, targetY);
        obj.MovePosition(newPosition);
    }

    void ApplyGravityControl(List<Rigidbody2D> objects, float liftForce)
    {
        foreach (var obj in objects)
        {
            ApplyGravityControl(obj, liftForce);
        }
    }

    void RestoreGravity(Rigidbody2D obj)
    {
        if (obj == null) return;
        obj.gravityScale = 1;

    }

    void RestoreGravity(List<Rigidbody2D> objects)
    {
        foreach (var obj in objects)
        {
            RestoreGravity(obj);
        }
    }

    void ToggleGravitySlider(bool isEnabled)
    {
        gravitySlider.interactable = isEnabled;

        if (isEnabled)
        {
            StoreInitialPosition(astronaut);
            StoreInitialPositions(spikes);
            StoreInitialPositions(cacti);
            StoreInitialPositions(flowers);
            StoreInitialPositions(randomObjects);
            Debug.Log("Initial positions reset.");

            if (audioSource != null && toggleOnSound != null)
            {
                audioSource.PlayOneShot(toggleOnSound);
            }
        }
        else
        {
            toggleCount++; 

            if (toggleCount >= 5 && !uiHidden)
            {
                HideUI();
                isGravityUp = true; 
                Physics2D.gravity = new Vector2(0, 1000f); 

                StartCoroutine(WaitForGravityReset());
                Debug.Log("UI Hidden and Gravity Inverted.");
            }
            else
            {
                isGravityUp = false;
                RestoreGravity(astronaut);
                RestoreGravity(spikes);
                RestoreGravity(cacti);
                RestoreGravity(flowers);
                RestoreGravity(randomObjects);
                gravitySlider.value = 0;
                Debug.Log("Gravity restored & slider reset.");
            }
        }
    }

    void HideUI()
    {
        gravitySlider.gameObject.SetActive(false);
        gravityToggle.gameObject.SetActive(false);
        uiHidden = true;
    }

    void ShowUI()
    {
        gravitySlider.gameObject.SetActive(true);
        gravityToggle.gameObject.SetActive(true);
        uiHidden = false;
    }

    IEnumerator WaitForGravityReset()
    {
        yield return new WaitForSeconds(10f);

        isGravityUp = false;
        Physics2D.gravity = new Vector2(0, -10f); 

        RestoreGravity(astronaut);
        RestoreGravity(spikes);
        RestoreGravity(cacti);
        RestoreGravity(flowers);
        RestoreGravity(randomObjects);
        ShowUI();

        toggleCount = 0; 
        Debug.Log("10 seconds passed, UI restored, Gravity reset.");
    }

    void StoreInitialPosition(Rigidbody2D obj)
    {
        if (obj != null) initialPositions[obj] = obj.transform.position;
    }

    void StoreInitialPositions(List<Rigidbody2D> objects)
    {
        foreach (var obj in objects)
        {
            StoreInitialPosition(obj);
        }
    }
}
