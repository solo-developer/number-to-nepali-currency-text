using System;
using System.Collections.Generic;
using System.Text;

namespace NumberToNepaliCurrencyText
{
    public class NumberToNepaliCurrencyText
    {
        public static string NumberToCurrencyText(decimal number, MidpointRounding midpointRounding)
        {
            number = decimal.Round(number, 2, midpointRounding);

            string[] arrNumber = number.ToString().Split('.');

            long wholePart = long.Parse(arrNumber[0]);


            string[] numbersUptoHundred = new string[] { "शुन्य", "एक", "दुई", "तीन", "चार", "पाँच", "छ", "सात", "आठ", "नौ", "दश", "एघार", "बाह्र", "तेह्र", "चौध", "पन्ध्र", "सोह्र", "सत्र", "अठार", "उन्नाइस", "विस", "एक्काइस", "बाइस", "तेईस", "चौविस", "पच्चिस", "छब्बिस", "सत्ताइस", "अठ्ठाईस", "उनन्तिस", "तिस", "एकत्तिस", "बत्तिस", "तेत्तिस", "चौँतिस", "पैँतिस", "छत्तिस", "सैँतीस", "अठतीस", "उनन्चालीस", "चालीस", "एकचालीस", "बयालीस", "त्रियालीस", "चवालीस", "पैँतालीस", "छयालीस", "सच्चालीस", "अठचालीस", "उनन्चास", "पचास", "एकाउन्न", "बाउन्न", "त्रिपन्न", "चउन्न", "पचपन्न", "छपन्न", "सन्ताउन्न", "अन्ठाउन्न", "उनन्साठी", "साठी", "एकसट्ठी", "बयसट्ठी", "त्रिसट्ठी", "चौंसट्ठी", "पैंसट्ठी", "छयसट्ठी", "सतसट्ठी", "अठसट्ठी", "उनन्सत्तरी", "सत्तरी", "एकहत्तर", "बहत्तर", "त्रिहत्तर", "चौहत्तर", "पचहत्तर", "छयहत्तर", "सतहत्तर", "अठहत्तर", "उनासी", "असी", "एकासी", "बयासी", "त्रियासी", "चौरासी", "पचासी", "छयासी", "सतासी", "अठासी", "उनान्नब्बे", "नब्बे", "एकान्नब्बे", "बयानब्बे", "त्रियान्नब्बे", "चौरान्नब्बे", "पन्चानब्बे", "छयान्नब्बे", "सन्तान्नब्बे", "अन्ठान्नब्बे", "उनान्सय", "सय" };

            string[] powers = new string[] { "हजार ", "लाख", "करोड", "अर्ब", "खर्ब" };

            string wordNumber = "";

            while (wholePart > 99)
            {
                char[] wholeNumberIntoCharacterArray = wholePart.ToString().ToCharArray();

                if (wholeNumberIntoCharacterArray.Length < 4)
                {
                    int hundredNumber = Convert.ToInt32(Math.Floor(Convert.ToDecimal(wholePart / 100)));
                    wordNumber += $"{numbersUptoHundred[hundredNumber]} {numbersUptoHundred[100]} ";
                    wholePart = Convert.ToInt32(wholePart % 100);
                }
                else
                {
                    //get index of power
                    int powerIndex = 0;
                    int lengthOfNumber = wholeNumberIntoCharacterArray.Length;
                    bool isLengthOfNumberEven = lengthOfNumber % 2 == 0;

                    string divisibleNumberInStringFormat = "1";

                    int numberOfZerosInDivisibleNumber = 0;

                    if (isLengthOfNumberEven)
                    {
                        powerIndex = (lengthOfNumber - 4) / 2;
                        numberOfZerosInDivisibleNumber = lengthOfNumber - 1;
                    }
                    else
                    {
                        numberOfZerosInDivisibleNumber = lengthOfNumber - 2;
                        powerIndex = ((lengthOfNumber - 1) - 4) / 2;
                    }
                    for (int i = 0; i < numberOfZerosInDivisibleNumber; i++)
                    {
                        divisibleNumberInStringFormat += "0";
                    }

                    int divisibleNumber = Convert.ToInt32(divisibleNumberInStringFormat);

                    int tensNumber = Convert.ToInt32(Math.Floor(Convert.ToDecimal(wholePart / divisibleNumber)));

                    wordNumber += $"{numbersUptoHundred[tensNumber]} {powers[powerIndex]} ";

                    wholePart = Convert.ToInt32(wholePart % divisibleNumber);
                }
            }

            if (wholePart > 0)
            {
                wordNumber += $" {numbersUptoHundred[wholePart]}";
            }
            wordNumber += " रूपैया";

            wordNumber = appendPaisaIfPaisaPresent(arrNumber, numbersUptoHundred, wordNumber);

            return wordNumber;
        }

        private static string appendPaisaIfPaisaPresent(string[] arrNumber, string[] numbersUptoHundred, string wordNumber)
        {
            bool isNumberDecimal = arrNumber.Length == 2;
            if (isNumberDecimal)
            {
                int paisa = Convert.ToInt32(arrNumber[1]);
                if (paisa > 0)
                {
                    wordNumber += $" {numbersUptoHundred[Convert.ToInt32(arrNumber[1])]} पैसा";
                }
            }

            return wordNumber;
        }
    }
}
