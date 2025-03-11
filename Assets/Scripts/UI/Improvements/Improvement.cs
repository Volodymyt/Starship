using JetBrains.Annotations;
using System.Linq;
using TMPro;
using UnityEngine;

public abstract class Improvement : ScriptableObject
{
    private GameObject PointsBar;
    private GameObject[] Points, NextImprovements;
    private TMP_Text CostText;

    public int StartCost, Cost, AddToCost;

    public abstract void ApplyImprovement(GameObject objectToUpgrade, float UpgradeValue);

    public int ImprovementOperations(int Money)
    {
        for (int i = 0; i < Points.Length; i++)
        {
            if (!Points[i].activeSelf)
            {
                if (Money >= Cost)
                {
                    Money -= Cost;
                    Cost += AddToCost;
                    Points[i].SetActive(true);
                    CostText.text = Cost.ToString();

                    if (i >= 5)
                    {
                        for (int j = 0; j < NextImprovements.Length; j++)
                        {
                            NextImprovements[j].SetActive(true);
                        }
                    }

                    break;
                }
            }
        }

        return Money;
    }

    public void LoadData(GameObject pointsBar, TMP_Text cost, GameObject[] nextImprovement, int Level)
    {
        CostText = cost;
        Cost = StartCost;
        CostText.text = Cost.ToString();

        NextImprovements = nextImprovement;
        PointsBar = pointsBar;

        Points = PointsBar.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();

        if (Level > 0)
        {
            for (int i = 0; i < Level; i++)
            {
                if (!Points[i].activeSelf)
                {
                    Cost += AddToCost;
                    Points[i].SetActive(true);
                    CostText.text = Cost.ToString();

                    if (i >= 5)
                    {
                        for (int j = 0; j < NextImprovements.Length; j++)
                        {
                            NextImprovements[j].SetActive(true);
                        }
                    }
                }
            }
        }
    }

    public int ReturnProgress()
    {
        int Progress = Cost / StartCost;

        return Progress;
    }

    public bool CheckMoney(float Money)
    {
        bool MoneyMoreThanCost = false;

        for (int i = 0; i < Points.Length; i++)
        {
            if (!Points[i].activeSelf)
            {
                if (Money >= Cost)
                {
                    MoneyMoreThanCost = true;
                }
            }
        }

        return MoneyMoreThanCost;
    }
}
