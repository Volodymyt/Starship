using UnityEditor;
using UnityEngine;

public class SaveHandler : MonoBehaviour
{
    [SerializeField] private UIOptions _UIOptions;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Heals _heals;
    [SerializeField] private PlayerShooting _playerShooting1, _playerShooting2, _starshipGun;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private PlayerFollovingBullet _playerFollovingBullet;
    [SerializeField] private Laser _laser;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private ImprovementPanel[] _improvementPanels;

    private void Start()
    {
        SaveSystem.Init();

        if (PlayerPrefs.GetInt("Load") == 1)
        {
            Load();
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Save();
        }
    }

    private void OnApplicationQuit()
    {
        _playerFollovingBullet.LoadDamage(3);
        _bullet.LoadDamage(1);
    }

    private void OnDisable()
    {
        _playerFollovingBullet.LoadDamage(3);
        _bullet.LoadDamage(1);
    }

    public void Save()
    {
        int[] levels = new int[_improvementPanels.Length];

        for (int i = 0; i < _improvementPanels.Length; i++)
        {
            levels[i] = _improvementPanels[i].ReturnProgress();
        }

        SaveClass saveClass = new SaveClass()
        {
            Money = _UIOptions.ReturnMoney(),
            Speed = _playerMovement.ReturnSpeed(),
            MaxEnergy = _playerMovement.ReturnMaxEnergy(),
            EnergyRechargeSpeed = _playerMovement.ReturnEnergyRechargeSpeed(),
            MaxHealth = _heals.ReturnMaxHealth(),
            HealthRechargeSpeed = _heals.ReturnRechargeSpeed(),
            BulletRechargeTime = _playerShooting1.ReturnRechargeTime(),
            BulletDamage = _bullet.ReturnDamega(),
            FollowBulletRechargeTime = _starshipGun.ReturnFollowBulletRechargeTime(),
            FollowBulletDamage = _playerFollovingBullet.ReturnDamage(),
            LaserDamage = _laser.ReturnDamage(),
            LaserDamageTime = _laser.ReturnDamageTime(),
            LaserRechargeTime = _starshipGun.ReturnLaserRechargeTime(),
            Difficulty = _enemySpawner.ReturnDifficulty(),
            DeadsEnemiesCount = _enemySpawner.ReturnDeadsEnemies(),

            ImprovementProgress = levels,
        };

        string json = JsonUtility.ToJson(saveClass);

        SaveSystem.Save(json);

        Debug.Log("Saved!");
    }

    public void Load()
    {
        string saveString = SaveSystem.Load();

        if (saveString != null)
        {
            SaveClass saveClass = JsonUtility.FromJson<SaveClass>(saveString);

            _UIOptions.LoadRestOfMoney(saveClass.Money);
            _playerMovement.LoadSpeed(saveClass.Speed);
            _playerMovement.LoadMaxEnergy(saveClass.MaxEnergy);
            _playerMovement.LoadEnergyRechargeSpeed(saveClass.EnergyRechargeSpeed);
            _heals.LoadHealthRechargeSpeed(saveClass.HealthRechargeSpeed);
            _heals.LoadMaxHealth(saveClass.MaxHealth);
            _playerShooting1.LoadRechargeTime(saveClass.BulletRechargeTime);
            _playerShooting2.LoadRechargeTime(saveClass.BulletRechargeTime);
            _bullet.LoadDamage(saveClass.BulletDamage);
            _starshipGun.LoadFollowBulletRechargeTime(saveClass.FollowBulletRechargeTime);
            _playerFollovingBullet.LoadDamage(saveClass.FollowBulletDamage);
            _laser.LoadDamage(saveClass.LaserDamage);
            _laser.LoadDamageTime(saveClass.LaserDamageTime);
            _starshipGun.LoadLaserRrechargeTime(saveClass.LaserRechargeTime);
            _enemySpawner.LoadDeadsEnemies(saveClass.DeadsEnemiesCount);
            _enemySpawner.LoadDifficulty(saveClass.Difficulty);

            for (int i = 0; i < _improvementPanels.Length; i++)
            {
                _improvementPanels[i].LoadLevel(saveClass.ImprovementProgress[i] - 1);
            }
        }
    }

    private class SaveClass
    {
        public int Money, Difficulty, DeadsEnemiesCount;
        public float Speed, MaxEnergy, EnergyRechargeSpeed, MaxHealth, HealthRechargeSpeed;
        public float BulletRechargeTime, BulletDamage, FollowBulletRechargeTime, FollowBulletDamage;
        public float LaserDamage, LaserDamageTime, LaserRechargeTime;

        public int[] ImprovementProgress;
    }
}
