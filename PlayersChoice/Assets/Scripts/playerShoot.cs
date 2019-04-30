using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerShoot : MonoBehaviour
{
    public GameObject projectile;
    public float speed = 50.0f;

    public GameObject magicShot;
    public float magicSpeed = 50.0f;

    public Color tempcolor;

    public float alphaValue;

    //UI images
    public Image swordIcon;
    public Image bowIcon;
    public Image magicIcon;
    public Image potionIcon;
    public Image bowCooldown;
    public Image magicCooldown;
    public Image swordCooldown;

    

    //Item Selections
    public GameObject selectedSword;
    public GameObject selectedBow;
    public GameObject selectedMagic;
    public GameObject selectedLowArmor;
    public GameObject selectedHighArmor;
    public GameObject selectedOneSprint;
    public GameObject selectedThreeSprint;
    public GameObject selectedFiveSprint;
    public GameObject splectedPotions;

    public TextMeshProUGUI potionNumber;


    public TextMeshProUGUI potionText;

    //Weapon selected
    public GameObject swordSelected;
    public GameObject bowSelected;
    public GameObject magicSelected;


    //Weapon GameObject
    public GameObject swordObject;
    public GameObject bowObject;
    public GameObject magicObject;
   

    //Weapon available to use?
    public bool swordPurchased;
    public bool bowPurchased;
    public bool magicPurchased;
    public bool potionPurchased;
    public bool lowArmorPurchased;
    public bool highArmorPurchased;

    //Number of potions
    public int potionAmount;
    public int maxPotions;

    //Player damage
    public float playerDamage;
    public float playerDamageModifier = 1.0f;


    //Weapon damage
    public float bowDamage;
    public float swordDamage;
    public float magicDamage;
    public float fistDamage;

    public float damageTook;
    public float attackRange;
    public Transform swordAttack;
    public Transform magicAttack;
    public Transform bowAttack;
    public Transform attackPos;
    public LayerMask whoIsEnemy;

    //Swing timers
    public float maxBowShoot;
    private float bowShoot;

    public float maxMagicShoot;
    private float magicShoot;

    //button toggles
    private bool swordOn;
    private bool bowOn;
    private bool magicOn;
    private bool lowAOn;
    private bool highAOn;
    private bool oneSOn;
    private bool threeSOn;
    private bool fiveSOn;

    private Animator anim;

    public GameObject hitEffect;

    public AudioSource drinkSound;

    public AudioSource gotHitSound;

    public GameObject drinkEffect;

    public bool clothPlayer;


    // Start is called before the first frame update
    void Start()
    {
        bowShoot = maxBowShoot;
        magicShoot = maxMagicShoot;
        Time.timeScale = 0f;
        swordOn = false;
        bowOn = false;
        magicOn = false;
        lowAOn = false;
        highAOn = false;
        oneSOn = false;
        threeSOn = false;
        fiveSOn = false;
        anim = GetComponent<Animator>();
        magicSelected.SetActive(false);
        swordSelected.SetActive(false);
        bowSelected.SetActive(false);


    }
        // Update is called once per frame
        void Update()
    {
       if (highArmorPurchased == false && lowArmorPurchased == false)
        {
            clothPlayer = true;
        }

        bowCooldown.fillAmount = bowShoot / maxBowShoot;
        magicCooldown.fillAmount = magicShoot / maxMagicShoot;

        if (Input.GetKeyDown("1") && swordPurchased == true)
        {
            swordSelected.SetActive(true);
            bowSelected.SetActive(false);
            magicSelected.SetActive(false);
        }

        if (Input.GetKeyDown("2") && bowPurchased == true)
        {
            bowSelected.SetActive(true);
            swordSelected.SetActive(false);
            magicSelected.SetActive(false);
        }

        if (Input.GetKeyDown("3") && magicPurchased == true)
        {
            bowSelected.SetActive(false);
            swordSelected.SetActive(false);
            magicSelected.SetActive(true);
        }

        // Using Bow
        if(bowSelected.activeInHierarchy)
        {

            swordObject.SetActive(false);
            bowObject.SetActive(true);
            magicObject.SetActive(false);
            magicCooldown.fillAmount = 0;
           

            playerDamage = bowDamage * playerDamageModifier;
            attackPos = bowAttack;
            damageTook = playerDamage;

            if (bowShoot <= 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                    Vector2 playerPosition = new Vector2(transform.position.x + 0.2f, transform.position.y + 0.8f);
                    Vector2 direction = target - playerPosition;
                    direction.Normalize();
                    Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                    GameObject projectiles = (GameObject)Instantiate(projectile, playerPosition, rotation);
                    projectiles.GetComponent<Rigidbody2D>().velocity = direction * speed;
                    bowShoot = maxBowShoot;
                }
            }
            else
            {
                bowShoot -= Time.deltaTime;
            }

            
        }

        //Using Sword
        if (swordSelected.activeInHierarchy)
        {
            swordObject.SetActive(true);
            bowObject.SetActive(false);
            magicObject.SetActive(false);
            magicCooldown.fillAmount = 0;
            bowCooldown.fillAmount = 0;

            playerDamage = swordDamage * playerDamageModifier;
            attackPos = swordAttack;
            damageTook = playerDamage;

            weaponMovement weaponTimer = GetComponentInChildren<weaponMovement>();

            if (weaponTimer.swordSwingtime <= 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(swordAttack.position, attackRange, whoIsEnemy);


                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<enemyController>().TakeDamage(damageTook);
                        Instantiate(hitEffect, enemiesToDamage[i].transform.position, Quaternion.identity);
                        Debug.Log("Hit Enemy With Sword");
                    }


                }
            }

           /* if (Input.GetMouseButtonDown(0))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(swordAttack.position, attackRange, whoIsEnemy);


                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<enemyController>().TakeDamage(damageTook);
                    Instantiate(hitEffect, enemiesToDamage[i].transform.position, Quaternion.identity);
                    Debug.Log("Hit Enemy With Sword");
                }


            }*/
            
           
            
        }

        //Using Magic
        if (magicSelected.activeInHierarchy)
        {
            swordObject.SetActive(false);
            bowObject.SetActive(false);
            magicObject.SetActive(true);
            bowCooldown.fillAmount = 0;
            swordCooldown.fillAmount = 0;


            playerDamage = magicDamage * playerDamageModifier;
            attackPos = magicAttack;
            damageTook = playerDamage;

            if (magicShoot <= 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                    Vector2 playerPosition = new Vector2(transform.position.x + 0.2f, transform.position.y + 1.2f);
                    Vector2 direction = target - playerPosition;
                    direction.Normalize();
                    Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                    GameObject magicBeam = (GameObject)Instantiate(magicShot, playerPosition, rotation);
                    magicBeam.GetComponent<Rigidbody2D>().velocity = direction * magicSpeed;
                    magicShoot = maxMagicShoot;
                }
            }
            else
            {
                magicShoot -= Time.deltaTime;
            }
            
        }

        if (Input.GetKeyDown(KeyCode.R) && potionPurchased == true)
        {
            if (potionAmount > 0)
            {
                potionAmount -= 1;
                drinkSound.GetComponent<soundRandomizer>().PlayActive();
                Instantiate(drinkEffect, transform.position, Quaternion.identity);
                potionText.text = potionAmount.ToString();
                GetComponent<healthBar>().health += 3;
            }
        }

        anim.SetBool("gotLight", lowArmorPurchased);
        anim.SetBool("gotHeavy", highArmorPurchased);

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    public void BuySellSword()
    {
        if (!swordOn){
            Debug.Log("Sword Purchased");
            swordPurchased = true;
            GetComponent<healthBar>().numberHearts -= 8;
            GetComponent<healthBar>().health -= 8;
            Debug.Log(GetComponent<healthBar>().numberHearts);
            swordOn = true;
            selectedSword.SetActive(true);
            gotHitSound.GetComponent<soundRandomizer>().PlayActive();
        }
        else
        {
            swordPurchased = false;
            GetComponent<healthBar>().numberHearts += 8;
            GetComponent<healthBar>().health += 8;
            swordOn = false;
            selectedSword.SetActive(false);
        }
    }

    public void BuySellBow()
    {
        if (!bowOn)
        {
            Debug.Log("Bow Purchased");
            bowPurchased = true;
            GetComponent<healthBar>().numberHearts -= 9;
            GetComponent<healthBar>().health -= 9;
            Debug.Log(GetComponent<healthBar>().numberHearts);
            bowOn = true;
            selectedBow.SetActive(true);
            gotHitSound.GetComponent<soundRandomizer>().PlayActive();
        }
        else
        {
            bowPurchased = false;
            GetComponent<healthBar>().numberHearts += 9;
            GetComponent<healthBar>().health += 9;
            bowOn = false;
            selectedBow.SetActive(false);
        }
    }

    public void BuySellMagic()
    {
        if (!magicOn)
        {
            Debug.Log("Magic Purchased");
            magicPurchased = true;
            GetComponent<healthBar>().numberHearts -= 10;
            GetComponent<healthBar>().health -= 10;
            Debug.Log(GetComponent<healthBar>().numberHearts);
            magicOn = true;
            selectedMagic.SetActive(true);
            gotHitSound.GetComponent<soundRandomizer>().PlayActive();
        }
        else
        {
            magicPurchased = false;
            GetComponent<healthBar>().numberHearts += 10;
            GetComponent<healthBar>().health += 10;
            magicOn = false;
            selectedMagic.SetActive(false);
        }
    }

    public void BuySellLowArmor()
    {
        if (!lowAOn)
        {
            Debug.Log("Low armor Purchased");
            lowArmorPurchased = true;
            GetComponent<healthBar>().numberHearts -= 7;
            GetComponent<healthBar>().health -= 7;
            Debug.Log(GetComponent<healthBar>().numberHearts);
            lowAOn = true;
            selectedLowArmor.SetActive(true);
            gotHitSound.GetComponent<soundRandomizer>().PlayActive();
            if (highAOn == true)
            {
                highArmorPurchased = false;
                GetComponent<healthBar>().numberHearts += 8;
                GetComponent<healthBar>().health += 8;
                highAOn = false;
                selectedHighArmor.SetActive(false);
            }
        }
        else
        {
            lowArmorPurchased = false;
            GetComponent<healthBar>().numberHearts += 7;
            GetComponent<healthBar>().health += 7;
            lowAOn = false;
            selectedLowArmor.SetActive(false);
        }
    }

    public void BuySellHighArmor()
    {
        if (!highAOn)
        {
            Debug.Log("High armor Purchased");
            highArmorPurchased = true;
            GetComponent<healthBar>().numberHearts -= 8;
            GetComponent<healthBar>().health -= 8;
            Debug.Log(GetComponent<healthBar>().numberHearts);
            highAOn = true;
            selectedHighArmor.SetActive(true);
            gotHitSound.GetComponent<soundRandomizer>().PlayActive();
            if (lowAOn == true)
            {
                lowArmorPurchased = false;
                GetComponent<healthBar>().numberHearts += 7;
                GetComponent<healthBar>().health += 7;
                lowAOn = false;
                selectedLowArmor.SetActive(false);
            }
        }
        else
        {
            highArmorPurchased = false;
            GetComponent<healthBar>().numberHearts += 8;
            GetComponent<healthBar>().health += 8;
            highAOn = false;
            selectedHighArmor.SetActive(false);
        }
    }

    public void BuyPotion()
    {
        potionPurchased = true;
        potionAmount += 1;
        potionText.text = potionAmount.ToString();
        GetComponent<healthBar>().numberHearts -= 3;
        GetComponent<healthBar>().health -= 3;
        splectedPotions.SetActive(true);
        Debug.Log(potionAmount);
        potionNumber.text = potionAmount.ToString();
        gotHitSound.GetComponent<soundRandomizer>().PlayActive();
    }

    public void SellPotion()
    {
        if (potionAmount > 0)
        {
            potionAmount -= 1;
            potionText.text = potionAmount.ToString();
            GetComponent<healthBar>().numberHearts += 3;
            GetComponent<healthBar>().health += 3;
            potionNumber.text = potionAmount.ToString();
            if (potionAmount == 0)
            {
                potionPurchased = false;
                potionAmount = 0;
                potionText.text = potionAmount.ToString();
                splectedPotions.SetActive(false);
            }
        }
        else
        {
            potionPurchased = false;
            potionAmount = 0;
            potionText.text = potionAmount.ToString();
            splectedPotions.SetActive(false);
        }
        
        
    }

    public void BuySellOneSprint()
    {
        if (!oneSOn)
        {
            GetComponent<playerMovement>().canSprint = true;
            GetComponent<playerMovement>().maxSprint = 1.0f;
            GetComponent<healthBar>().numberHearts -= 1;
            GetComponent<healthBar>().health -= 1;
            oneSOn = true;
            selectedOneSprint.SetActive(true);
            gotHitSound.GetComponent<soundRandomizer>().PlayActive();
            if (threeSOn == true)
            {
                threeSOn = false;
                GetComponent<healthBar>().numberHearts += 3;
                GetComponent<healthBar>().health += 3;
                selectedThreeSprint.SetActive(false);
            }
            if (fiveSOn == true)
            {
                fiveSOn = false;
                GetComponent<healthBar>().numberHearts += 5;
                GetComponent<healthBar>().health += 5;
                selectedFiveSprint.SetActive(false);
            }
            
        }
        else
        {
            GetComponent<playerMovement>().canSprint = false;
            GetComponent<playerMovement>().maxSprint = 0f;
            GetComponent<healthBar>().numberHearts += 1;
            GetComponent<healthBar>().health += 1;
            oneSOn = false;
            selectedOneSprint.SetActive(false);
        }
    }

    public void BuySellThreeSprint()
    {
        if (!threeSOn)
        {
            GetComponent<playerMovement>().canSprint = true;
            GetComponent<playerMovement>().maxSprint = 3.0f;
            GetComponent<healthBar>().numberHearts -= 3;
            GetComponent<healthBar>().health -= 3;
            threeSOn = true;
            selectedThreeSprint.SetActive(true);
            gotHitSound.GetComponent<soundRandomizer>().PlayActive();
            if (oneSOn == true)
            {
                oneSOn = false;
                GetComponent<healthBar>().numberHearts += 1;
                GetComponent<healthBar>().health += 1;
                selectedOneSprint.SetActive(false);
            }
            if (fiveSOn == true)
            {
                fiveSOn = false;
                GetComponent<healthBar>().numberHearts += 5;
                GetComponent<healthBar>().health += 5;
                selectedFiveSprint.SetActive(false);
            }

        }
        else
        {
            GetComponent<playerMovement>().canSprint = false;
            GetComponent<playerMovement>().maxSprint = 0f;
            GetComponent<healthBar>().numberHearts += 3;
            GetComponent<healthBar>().health += 3;
            threeSOn = false;
            selectedThreeSprint.SetActive(false);
        }
    }

    public void BuySellFiveSprint()
    {
        if (!fiveSOn)
        {
            GetComponent<playerMovement>().canSprint = true;
            GetComponent<playerMovement>().maxSprint = 5.0f;
            GetComponent<healthBar>().numberHearts -= 5;
            GetComponent<healthBar>().health -= 5;
            fiveSOn = true;
            selectedFiveSprint.SetActive(true);
            gotHitSound.GetComponent<soundRandomizer>().PlayActive();
            if (oneSOn == true)
            {
                oneSOn = false;
                GetComponent<healthBar>().numberHearts += 1;
                GetComponent<healthBar>().health += 1;
                selectedOneSprint.SetActive(false);
            }
            if (threeSOn == true)
            {
                threeSOn = false;
                GetComponent<healthBar>().numberHearts += 3;
                GetComponent<healthBar>().health += 3;
                selectedThreeSprint.SetActive(false);
            }

        }
        else
        {
            GetComponent<playerMovement>().canSprint = false;
            GetComponent<playerMovement>().maxSprint = 0f;
            GetComponent<healthBar>().numberHearts += 5;
            GetComponent<healthBar>().health += 5;
            fiveSOn = false;
            selectedFiveSprint.SetActive(false);
        }
    }

    public void InitiateGame()
    {
        //---Default to sword if sword puchased---------------------------------------
        if (swordPurchased == true && bowPurchased == false && magicPurchased == false)
        {
            swordSelected.SetActive(true);
            bowSelected.SetActive(false);
            magicSelected.SetActive(false);

            tempcolor = bowIcon.color;
            tempcolor.a = alphaValue;
            bowIcon.color = tempcolor;

            tempcolor = magicIcon.color;
            tempcolor.a = alphaValue;
            magicIcon.color = tempcolor;

        }

        if (swordPurchased == true && bowPurchased == true && magicPurchased == false)
        {
            swordSelected.SetActive(true);
            bowSelected.SetActive(false);
            magicSelected.SetActive(false);

            tempcolor = magicIcon.color;
            tempcolor.a = alphaValue;
            magicIcon.color = tempcolor;
        }

        if (swordPurchased == true && bowPurchased == true && magicPurchased == true)
        {
            swordSelected.SetActive(true);
            bowSelected.SetActive(false);
            magicSelected.SetActive(false);
        }

        if (swordPurchased == true && bowPurchased == false && magicPurchased == true)
        {
            swordSelected.SetActive(true);
            bowSelected.SetActive(false);
            magicSelected.SetActive(false);

            tempcolor = bowIcon.color;
            tempcolor.a = alphaValue;
            bowIcon.color = tempcolor;
        }

        //---Default to bow if no sword-----------------------------------------------
        if (swordPurchased == false && bowPurchased == true && magicPurchased == false)
        {
            swordSelected.SetActive(false);
            bowSelected.SetActive(true);
            magicSelected.SetActive(false);

            tempcolor = swordIcon.color;
            tempcolor.a = alphaValue;
            swordIcon.color = tempcolor;

            tempcolor = magicIcon.color;
            tempcolor.a = alphaValue;
            magicIcon.color = tempcolor;
        }

        if (swordPurchased == false && bowPurchased == true && magicPurchased == true)
        {
            swordSelected.SetActive(false);
            bowSelected.SetActive(true);
            magicSelected.SetActive(false);

            tempcolor = swordIcon.color;
            tempcolor.a = alphaValue;
            swordIcon.color = tempcolor;
        }

        //---Default to magic if nothing else-----------------------------------------
        if (swordPurchased == false && bowPurchased == false && magicPurchased == true)
        {
            swordSelected.SetActive(false);
            bowSelected.SetActive(false);
            magicSelected.SetActive(true);

            tempcolor = swordIcon.color;
            tempcolor.a = alphaValue;
            swordIcon.color = tempcolor;

            tempcolor = bowIcon.color;
            tempcolor.a = alphaValue;
            bowIcon.color = tempcolor;


        }
        
        //---Fists---------------------------------------------------------------------
        if (swordPurchased == false && bowPurchased == false && magicPurchased == false)
        {
            swordSelected.SetActive(false);
            bowSelected.SetActive(false);
            magicSelected.SetActive(false);

            tempcolor = swordIcon.color;
            tempcolor.a = alphaValue;
            swordIcon.color = tempcolor;

            tempcolor = magicIcon.color;
            tempcolor.a = alphaValue;
            magicIcon.color = tempcolor;

            tempcolor = bowIcon.color;
            tempcolor.a = alphaValue;
            bowIcon.color = tempcolor;
        } 

        if (potionPurchased == false)
        {
            tempcolor = potionIcon.color;
            tempcolor.a = alphaValue;
            potionIcon.color = tempcolor;
            potionAmount = 0;
            potionText.text = potionAmount.ToString();
        }
    }
}
