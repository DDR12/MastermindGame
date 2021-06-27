using TMPro;
using UnityEngine;

namespace Mastermind.ViewModels
{
    /// <summary>
    /// A UI row for each turn result.
    /// </summary>
    public class TurnResultUIViewModel : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI guessText = null, evaulationText = null;
        [SerializeField]
        private uint iconsSize = 30;
        [SerializeField]
        private string bullsIconName = "bull", cowsIconName = "cow"; // Names of the icons in the text sprite sheet.


        public void Repaint(GameTurnResult result)
        {
            guessText.text = string.Join(" ", result.Guess.Tokens);

            // Just formatting the icons sizes inside the text.
            string bullsIcon = Utils.WrapInSizeTag($"<sprite=\"{bullsIconName}\" index=0>", iconsSize);
            string cowsIcon = Utils.WrapInSizeTag($"<sprite=\"{cowsIconName}\" index=0>", iconsSize);

            evaulationText.text = $"{result.GuessResult.Bulls}x{bullsIcon}\t{result.GuessResult.Cows}x{cowsIcon}";
        }
    }
}