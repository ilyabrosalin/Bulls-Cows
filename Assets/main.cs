using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Main : MonoBehaviour
{
    public Main()
    {
        hiddenNumber = GetRandomNumber();
    }

    public GameObject inputField;
    public GameObject makeAMoveBtn;
    public GameObject newGameBtn;
    public Text output;
    public Text strong;
    public Text alert;

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

    public Tuple<int, int> SearchBullsAndCows(string hiddenNumber, string userNumber)
    {
        int bulls = 0;
        int cows = 0;
        for (int i = 0; i < 4; i++)
        {
            if (hiddenNumber.Contains(userNumber[i]))
            {
                if (hiddenNumber[i] == userNumber[i])
                    bulls++;
                else
                    cows++;
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

    public void OnClickNewGameBtn()
    {
        output.text = "";
        strong.text = "";
        alert.text = "";
        alert.color = Color.black;
        makeAMoveBtn.SetActive(true);
        inputField.SetActive(true);
        newGameBtn.SetActive(false);
    }

    public void OnClickMakeAMoveBtn()
    {
        Debug.Log(hiddenNumber);
        alert.text = "";
        alert.color = Color.black;
        string userNumber = inputField.GetComponent<InputField>().text;
        var isNumeric = int.TryParse(userNumber, out _);
        var isUnique = OnlyUniqueDigits(userNumber);
        
        if (isNumeric && isUnique && userNumber.Length==4)
        {
            (int bulls, int cows) = (SearchBullsAndCows(hiddenNumber, userNumber).Item1, SearchBullsAndCows(hiddenNumber, userNumber).Item2);
            string bullsDesc = "";
            string cowsDesc = "";
            switch (bulls)
            {
                case 0:
                    bullsDesc = " быков ";
                    break;
                case 1:
                    bullsDesc = " бык ";
                    break;
                case 2 or 3 or 4:
                    bullsDesc = " быка ";
                    break;
            }
            switch (cows)
            {
                case 0:
                    cowsDesc = " коров ";
                    break;
                case 1:
                    cowsDesc = " корова ";
                    break;
                case 2 or 3 or 4:
                    cowsDesc = " коровы ";
                    break;
            }
            string outText = inputField.GetComponent<InputField>().text + ": " + bulls + bullsDesc + cows + cowsDesc + "\n";
            if (bulls + cows >= 3) 
                strong.text = strong.text.Insert(0, outText);
            else
                output.text = output.text.Insert(0, outText);

            inputField.GetComponent<InputField>().text = "";
            if (bulls == 4)
            {
                alert.text = "Мууу! Победа!";
                alert.color = Color.green;
                makeAMoveBtn.SetActive(false);
                inputField.SetActive(false);
                newGameBtn.SetActive(true);
                hiddenNumber = GetRandomNumber();
            }
        }
        else
        {
            alert.color = Color.red;
            if (!isUnique) alert.text = "Цифры не должны повторяться";
            if (!isNumeric || userNumber.Length != 4) alert.text = "Код - четырехзначное число";
        }
    }

    public void GoToRules()
    {
        SceneManager.LoadScene("Rules");
    }
}
