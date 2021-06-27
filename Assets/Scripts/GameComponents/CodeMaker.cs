using Mastermind.Boards;
using Mastermind.Enums;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Mastermind
{
    /// <summary>
    /// Basic behaviour to accept input code from the code maker.
    /// </summary>
    public class CodeMaker : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField codeInputField = null;
        
        public ProcessInputCodeMakerCodeError GetCode(out Combination secretCode)
        {
            secretCode = null;
            string input = codeInputField.text.Trim();
            if (string.IsNullOrWhiteSpace(input))
                return  ProcessInputCodeMakerCodeError.EmptyInput;
            if (input.Length != 4)
                return  ProcessInputCodeMakerCodeError.InvalidInputLength;

            List<byte> tokens = new List<byte>();

            for (int i = 0; i < input.Length; i++)
            {
                string tokenString = input[i].ToString();
                if (byte.TryParse(tokenString, out byte token) == false)
                    return  ProcessInputCodeMakerCodeError.InvalidInputCharacter;
                if (tokens.Contains(token) && !GameManager.Instance.AllowDuplication)
                    return  ProcessInputCodeMakerCodeError.DuplicationNotAllowedByGameSettings;
                tokens.Add(token);
            }
            secretCode = new Combination(tokens);
            return  ProcessInputCodeMakerCodeError.None;
        }
    }
   
}