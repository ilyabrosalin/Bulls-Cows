using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Main : MonoBehaviour
{
    public Main()
    {
        hiddenNumber = GetRandomNumber();
    }

    public GameObject inputField;
    public GameObject button;
    public Text output;

    public static string hiddenNumber;

    public string GetRandomNumber()
    {
        System.Random rnd = new System.Random();
        string result;
        do
        {
            result = (rnd.Next(1000, 9999)).ToString();
        }
        while (!OnlyUniqueDigits(result));
        return result;
    }

    public Tuple<int, int> SearchBulls(string hiddenNumber, string userNumber)
    {
        Debug.Log(hiddenNumber);
        int bulls = 0;
        int cows = 0;
        string valuesOfBulls = "";
        for (int i = 0, n = 4; i < n; i++)
        {
            if (hiddenNumber[i] == userNumber[i])
            {
                bulls++;
                valuesOfBulls += hiddenNumber[i];
            }
        }
        Debug.Log("valuesOfBulls: " + valuesOfBulls);
        for (int i = 0, n = valuesOfBulls.Length; i < n; i++)
        {
            hiddenNumber = hiddenNumber.Trim(new Char[] { valuesOfBulls[i] });
            userNumber = userNumber.Trim(new Char[] { valuesOfBulls[i] });
        }
        for (int i = 0, n = hiddenNumber.Length; i < n; i++)
        {
            if (userNumber.Contains(hiddenNumber[i]))
            {
                cows++;
                Debug.Log(cows);
            }
        }
        return Tuple.Create(bulls,cows);
    }

    static bool OnlyUniqueDigits(string number)
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
    public void OnClick()
    {
        string userNumber = inputField.GetComponent<InputField>().text;
        var isNumeric = int.TryParse(userNumber, out _);
        var isUnique = OnlyUniqueDigits(userNumber);
        var tuple = (bulls: SearchBulls(hiddenNumber, userNumber).Item1, cows: SearchBulls(hiddenNumber, userNumber).Item2);

        if (isNumeric && isUnique && userNumber.Length==4)
        {
            output.text += inputField.GetComponent<InputField>().text + ": " + tuple.bulls + " быков " + tuple.cows + " коров " + "\n";
            if (tuple.bulls == 4)
            {
                output.text += " Мууу! Победа!";
                button.SetActive(false);
                inputField.SetActive(false);
            }
        }
        else
        {
            if (!isUnique) Debug.Log("Цифры не должны повторяться");
            if (!isNumeric || userNumber.Length != 4) Debug.Log("Код - четырехзначное число");
        }
    }
}
