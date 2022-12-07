using TMPro;
using UnityEngine;

namespace Script.Managers
{
    public class MoneyManager : MonoBehaviour
    {
        public static MoneyManager Instance { get; private set; }
        public TextMeshProUGUI textMoney;
        private int _multipleIndex;
        private string _moneyDigit;

        [SerializeField] private string[] multipleMoney = new string[]
        {
            "", "k", "M", "B", "T", 
            "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an", "ao", "ap", "aq", "ar",
            "as", "at", "au", "av", "aw", "ax", "ay", "az", 
            "ba", "bb", "bc", "bd", "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br", 
            "bs", "bt", "bu", "bv", "bw", "bx", "by", "bz",
        };

        public int money;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }


        public int Money
        {
            get => money;
            set
            {
                money += value;
                TextMoney();
                textMoney.text = _moneyDigit+multipleMoney[_multipleIndex];
            }
        }

        //TODO: !!--High Alert--!!Check for floor the money. if you have 1.399 money it is ceiling to 1.4k!!
        private void TextMoney()
        {
            var numberOfZeros = Mathf.FloorToInt(Mathf.Log10(money));
            Debug.Log("Number Of Zeros: " +numberOfZeros);
            
            if (numberOfZeros < 3)
            {
                _multipleIndex = 0;
                _moneyDigit = money.ToString();
            }
            else
            {
                var tempIndex = numberOfZeros / 3;
                _multipleIndex = Mathf.FloorToInt(tempIndex);
                
                var tempDigit = numberOfZeros % 3;
                
                var tempMoney = Mathf.Pow(10,numberOfZeros);
                Debug.Log("Temp Money: "+tempMoney);
                Debug.Log("Temp Digit: "+tempDigit);
                
                switch (tempDigit)
                {
                    case 0:
                        _moneyDigit = (money/tempMoney).ToString("F1");
                        break;
                    case 1:
                        _moneyDigit = (money/tempMoney).ToString("F1");
                        break;
                    case 2:
                        _moneyDigit = (money/tempMoney).ToString("F1");
                        break;
                }
            }
        }
        
    }
}
