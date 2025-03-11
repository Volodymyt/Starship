using TMPro;
using UnityEngine;

public class ImprovementPanel : MonoBehaviour
{
    [SerializeField] private GameObject _pointsBar, _objectToUpgrade;
    [SerializeField] private GameObject[] NextImprovements;
    [SerializeField] private Improvement _improvement;
    [SerializeField] private UIOptions _UIOptions;
    [SerializeField] private TMP_Text _costText, _info;
    [SerializeField] private float _upgradeValue;
    [SerializeField] private int _level = 0;

    private void Start()
    {
        _improvement.LoadData(_pointsBar, _costText, NextImprovements,  _level);
    }

    public void LoadLevel(int Level)
    {
        _level = Level;
    }

    public int ReturnProgress()
    {
       return _improvement.ReturnProgress();
    }

    public void Upgrade()
    {
        if (_improvement.CheckMoney(_UIOptions.ReturnMoney()))
        {
            _improvement.ApplyImprovement(_objectToUpgrade, _upgradeValue);
        }

        _UIOptions.LoadRestOfMoney(_improvement.ImprovementOperations(_UIOptions.ReturnMoney()));
    }
}
