using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [SerializeField] private GameObject _inputField;
    [SerializeField] private GameObject _rulesPanel;
    [SerializeField] private GameObject _makeAMoveBtn;
    [SerializeField] private GameObject _newGameBtn;
    [SerializeField] private Button _showRulesButton;
    [SerializeField] private Button _hideRulesButton;
    [SerializeField] private Text _output;
    [SerializeField] private Text _strong;
    [SerializeField] private Text _alert;

    private string _hiddenNumber;
    
    private void Start()
    {
        _hiddenNumber = GetRandomNumber();
        _showRulesButton.onClick.AddListener(() => _rulesPanel.SetActive(true));
        _hideRulesButton.onClick.AddListener(() => _rulesPanel.SetActive(false));
        _makeAMoveBtn.GetComponent<Button>().onClick.AddListener(OnClickMakeAMoveBtn);
        _newGameBtn.GetComponent<Button>().onClick.AddListener(OnClickNewGameBtn);
    }

    private string GetRandomNumber()
    {
        string result;
        do
        {
            result = UnityEngine.Random.Range(1000, 9999).ToString();
        }
        while (!OnlyUniqueDigits(result));
        return result;
    }

    private BullsAndCows SearchBullsAndCows(string hiddenNumber, string userNumber)
    {
        var bulls = 0;
        var cows = 0;
        for (var i = 0; i < 4; i++)
        {
            if (!hiddenNumber.Contains(userNumber[i]))
            {
                continue;
            }
            if (hiddenNumber[i] == userNumber[i])
            {
                bulls++;
            }
            else
            {
                cows++;
            }
        }
        return new BullsAndCows(bulls,cows);
    }

    private bool OnlyUniqueDigits(string number)
    {
        for (int i = 0, n = number.Length; i < n - 1; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (number[i] == number[j])
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void ChangeAlert(string text, Color color)
    {
        _alert.text = text;
        _alert.color = color;
    }

    private void ClearAlert()
    {
        ChangeAlert("", Color.black);
    }

    private void OnClickNewGameBtn()
    {
        ClearAlert();
        _output.text = "";
        _strong.text = "";
        _makeAMoveBtn.SetActive(true);
        _inputField.SetActive(true);
        _newGameBtn.SetActive(false);
    }

    private void OnClickMakeAMoveBtn()
    {
        Debug.Log(_hiddenNumber);
        ClearAlert();
        var userNumber = _inputField.GetComponent<InputField>().text;
        var isNumeric = int.TryParse(userNumber, out _);
        var isUnique = OnlyUniqueDigits(userNumber);

        if (!isUnique)
        {
            ChangeAlert("Цифры не должны повторяться", Color.red);
            return;
        }

        if (!isNumeric)
        {
            ChangeAlert("Код должен быть числом", Color.red);
            return;
        }

        if (userNumber.Length != 4)
        {
            ChangeAlert("Код - четырехзначное число", Color.red);
            return;
        }

        var bullsAndCows = SearchBullsAndCows(_hiddenNumber, userNumber);
        var bullsDesc = bullsAndCows.Bulls switch
        {
            0 => " быков ",
            1 => " бык ",
            2 or 3 or 4 => " быка ",
            _ => "error"
        };

        var cowsDesc = bullsAndCows.Cows switch
        {
            0 => " коров ",
            1 => " корова ",
            2 or 3 or 4 => " коровы ",
            _ => "error"
        };
        
        var outText = userNumber + ": " + bullsAndCows.Bulls + bullsDesc + bullsAndCows.Cows + cowsDesc + "\n";

        if (bullsAndCows.Bulls + bullsAndCows.Cows >= 3)
        {
            _strong.text = _strong.text.Insert(0, outText);
        }
        else
        {
            _output.text = _output.text.Insert(0, outText);
        }

        _inputField.GetComponent<InputField>().text = "";

        if (bullsAndCows.Bulls == 4)
        {
            Win();   
        }
    }

    private void Win()
    {
        ChangeAlert("Мууу! Победа!", Color.green);
        _makeAMoveBtn.SetActive(false);
        _inputField.SetActive(false);
        _newGameBtn.SetActive(true);
        _hiddenNumber = GetRandomNumber();
    }
}
