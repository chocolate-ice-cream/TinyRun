using Unity.Entities;

namespace TinyRun {
    public class TitleSystem : ComponentSystem {
        protected override void OnUpdate() {
            Entities.ForEach((ref GameState game) => {
                var begin = false;

                if (!game.Playing) {
                    Entities.ForEach((ref Joystick joystick) => {
                        if (joystick.IsPressed || joystick.IsPressedSpaceKey || joystick.IsPressedUpArrowKey) {
                            begin = true;
                        }
                    });

                    if (begin) {
                        Entities.ForEach((Entity panelEntity, ref TitlePanel titlePanel) => {
                            PostUpdateCommands.AddComponent(titlePanel.StartLabel, new Disabled());
                            PostUpdateCommands.AddComponent(titlePanel.TitleLabel, new Disabled());
                        });

                        Entities.ForEach((Entity panelEntity, ref ScorePanel scorePanel) => {
                            PostUpdateCommands.RemoveComponent<Disabled>(scorePanel.HiLabel);
                            PostUpdateCommands.RemoveComponent<Disabled>(scorePanel.HiScoreLabel);
                            PostUpdateCommands.RemoveComponent<Disabled>(scorePanel.CurrentScoreLabel);
                        });

                        game.Begun = begin;
                        game.GameOverPrev  = false;
                        game.GameOver = false;
                    }
                }
            });
        }
    }
}
