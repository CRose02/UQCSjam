using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance is null) Debug.Log("NO GAMEMANGER FOUND");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public int maxPlayerHealth;
    public int currentPlayerHealth;
    public bool hasKey;
    private GameObject zone;
    public GenerateLevel generateLevel;

    public Sprite tickSprite;
    public Sprite crossSprite;
    public Image keyCheckbox;

    public PlayerMovement playerMovement;
    public GameObject deathScreen;
    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            onKeyPickup();
        }

        if (currentPlayerHealth != maxPlayerHealth && currentPlayerHealth > 0)
        {
            RechargeHealth();
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            generateLevel.ResetLevelSystem();
        }

        if (dead)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                generateLevel.ResetLevelSystem();
            }
        }
    }

    public float rechargeTimer = 0f;
    public float rechargeTime;
    public AnimationCurve rechargeCurve;
    public RectTransform healthRect;
    public float rect3;
    public float rect2;
    public float rect1;
    public float rect0;

    public bool TakeDamage()
    {
        currentPlayerHealth--;
        rechargeTimer = 0f;
        if (currentPlayerHealth == 0)
        {
            healthRect.sizeDelta = new Vector2(rect0, 22f);
            // DIE
            playerMovement.StopMove();
            deathScreen.SetActive(true);
            dead = true;
            // start listening for r to restart
            return true;
        }
        if (currentPlayerHealth == 1)
        {
            healthRect.sizeDelta = new Vector2(rect1, 22f);
        }
        if (currentPlayerHealth == 2)
        {
            healthRect.sizeDelta = new Vector2(rect2, 22f);
        }


        return false;
    }

    public void ResetFromDeath()
    {
        playerMovement.StartMove();
        deathScreen.SetActive(false);
        dead = false;
    }

    public void ResetHealth()
    {
        currentPlayerHealth = maxPlayerHealth;
        healthRect.sizeDelta = new Vector2(rect3, 22f);
        rechargeTimer = 0f;
    }

    private void RechargeHealth()
    {
        rechargeTimer += Time.deltaTime;
        if (rechargeTimer > rechargeTime)
        {
            rechargeTimer = 0f;
            currentPlayerHealth++;

            if (currentPlayerHealth == 1)
            {
                healthRect.sizeDelta = new Vector2(rect1, 22f);
            }
            if (currentPlayerHealth == 2)
            {
                healthRect.sizeDelta = new Vector2(rect2, 22f);
            }
            if (currentPlayerHealth == 3)
            {
                healthRect.sizeDelta = new Vector2(rect3, 22f);
            }
        }

        if (currentPlayerHealth == 1)
        {
            healthRect.sizeDelta = new Vector2(Mathf.Lerp(rect1, rect2, rechargeCurve.Evaluate(rechargeTimer/rechargeTime)), 22f);
        }
        if (currentPlayerHealth == 2)
        {
            healthRect.sizeDelta = new Vector2(Mathf.Lerp(rect2, rect3, rechargeCurve.Evaluate(rechargeTimer / rechargeTime)), 22f);
        }
    }

    public void AssignZone(GameObject zoneInst)
    {
        zone = zoneInst;
    }

    public void onKeyPickup()
    {
        hasKey = true;
        zone.SetActive(true);
        keyCheckbox.sprite = tickSprite;
    }

    public void ResetKeyUI()
    {
        keyCheckbox.sprite = crossSprite;
    }

    private int EnemyCount()
    {
        return generateLevel.enemyParent.childCount;
    }

    public bool ShouldDropKey()
    {
        if (hasKey)
        {
            return false;
        }

        int enemies = EnemyCount();
        if (enemies > 20)
        {
            return false;
        }

        if (enemies > 10)
        {
            int result = Random.Range(0, 10);
            if (result == 0)
            {
                return true;
            } else
            {
                return false;
            }
        }

        if (enemies > 2)
        {
            int result = Random.Range(0, 3);
            if (result == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        if (enemies >= 0)
        {
            return true;
        }

        return false;
    }
}
