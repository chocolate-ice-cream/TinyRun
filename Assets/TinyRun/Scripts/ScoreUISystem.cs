using Unity.Entities;
using Unity.Tiny.Core;
using Unity.Tiny.Text;
using Unity.Mathematics;

namespace TinyRun {
    public class ScoreUISystem : ComponentSystem {
        protected override void OnUpdate() {
            var currentScore = 0;
            var hiScore = 0;
            var gameBegun = false;

            Entities.ForEach((ref GameState game) => {
                gameBegun = game.Playing;
                currentScore = game.CurrentScore;
                hiScore = game.HiScore;
            });

            if (gameBegun) {
                Entities.WithAll<CurrentScoreLabel>().ForEach((Entity entity) => {
                    EntityManager.SetBufferFromString<TextString>(entity, ToStringFiveDigits(currentScore));
                });

                Entities.WithAll<HiScoreLabel>().ForEach((Entity entity) => {
                    EntityManager.SetBufferFromString<TextString>(entity, ToStringFiveDigits(hiScore));
                });
            }
        }

        private string ToStringFiveDigits(int num) {
            var digit = 1;
            for (int i = num; i >= 10; i /= 10) {
                ++digit;
            }

            string fiveDigitsStr = null;
            for (int i = 0; i < 5 - digit; i++) {
                fiveDigitsStr += "0";
            }
            fiveDigitsStr += num.ToString();

            return fiveDigitsStr;
        }
    }
}
