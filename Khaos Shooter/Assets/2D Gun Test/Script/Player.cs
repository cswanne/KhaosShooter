using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    
    [System.Serializable]
    public class PlayerStats        //define a new class to hold the player's stats
    {
        public int maxHealth = 100;

        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
                     
        }

        public void Init()
        {
            curHealth = maxHealth;
        }

    }
        
    public PlayerStats stats = new PlayerStats();     //crate an instance of the PlayerStats class to use and refer to

    
    public int fallBoundary = 10;

    [SerializeField]
    private StatusIndicator statusIndicator;

    void Start()
    {
        stats.Init();

        if (statusIndicator == null)
        {
            Debug.LogError("No status indicator referenced");
        }
        else
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }
    
    void Update ()
    {
        if (transform.position.y <= fallBoundary)
            DamagePlayer(999999);
    }

    public void DamagePlayer (int damage )
    {
        stats.curHealth -= damage;
        if (stats.curHealth <= 0)
        {
            //GameMaster.KillPlayer(this);
        }

        //statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
    }
}
