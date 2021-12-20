using UnityEngine;
using UnityEngine.UI;

public class MoneyComponent : MonoBehaviour
{
    public Text moneyLabel;
    private int _money;

    private void Start()
    {
        Money = 300;
    }

    public int Money
    {
        get { return _money; }
        set
        {
            _money = value;
            moneyLabel.GetComponent<Text>().text = "Money: " + _money;
        }
    }

    public int AddMoney(int count)
    {
        Money += count;
        return Money;
    }
}