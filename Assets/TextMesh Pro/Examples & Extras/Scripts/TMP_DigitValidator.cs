using UnityEngine;
using System;

namespace TMPro
{
    /// <summary> Custom Character Input Validator: Only allows digits from 0 to 9 </summary>
    [Serializable]
    //[CreateAssetMenu(fileName = "InputValidator - Digits.asset", menuName = "TextMeshPro/Input Validators/Digits", order = 100)]
    public class TMP_DigitValidator : TMP_InputValidator
    {
        public override char Validate(ref string text, ref int pos, char ch)
        {
            if (ch >= '0' && ch <= '9')
            {
                text += ch;
                pos += 1;
                return ch;
            }

            return (char)0;
        }
    }
}