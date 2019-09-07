using Unity.Entities;
using Unity.Tiny.Core;
using Unity.Tiny.HitBox2D;
using Unity.Tiny.Text;

namespace TinyRun {
    public class GameMainSystem : ComponentSystem {
        protected override void OnUpdate() {
            Entities.ForEach((ref GameState game) => {
                var isGameOver = false;

                game.GameOverPrev = game.GameOver;

                if (!game.Playing) {
                    var scrollBegin = false;

                    Entities.ForEach((ref Character character) => {
                        scrollBegin = (character.jumpingPrev && !character.jumping);
                    });

                    if (scrollBegin) {
                        Entities.ForEach((Entity entity, ref Scroll scroll) => {
                            scroll.Enable = true;
                        });

                        game.Playing = scrollBegin;
                    }
                }
                else if (game.Playing && !game.GameOver) {
                    var deltaTime = World.TinyEnvironment().frameDeltaTime;
                    game.Elapse += deltaTime * 8.0f;

                    var currentScore = (int)game.Elapse;
                    game.CurrentScore = currentScore;

                    if (currentScore > game.HiScore) {
                        game.HiScore = currentScore;
                    }

                    var scale = game.SpeedScale;
                    switch (currentScore) {
                    case 100:
                        scale = 1.125f;
                        break;
                    case 200:
                        scale = 1.25f;
                        break;
                    case 300:
                        scale = 1.375f;
                        break;
                    case 400:
                        scale = 1.5f;
                        break;
                    case 500:
                        scale = 1.625f;
                        break;
                    case 600:
                        scale = 1.75f;
                        break;
                    case 700:
                        scale = 1.875f;
                        break;
                    case 800:
                        scale = 2.0f;
                        break;
                    case 900:
                        scale = 2.125f;
                        break;
                    case 1000:
                        scale = 2.25f;
                        break;
                    }

                    game.SpeedScale = scale;

                    Entities.ForEach((ref Scroll scroll) => {
                        scroll.Speed = scroll.DefaultSpeed * scale;
                    });

                    Entities.ForEach((Entity entity, ref Character character, ref RectHitBox2D rectHitBox2d) => {
                        if (EntityManager.HasComponent<HitBoxOverlap>(entity)) {
                            isGameOver = true;
                        }
                    });

                    if (isGameOver) {
                        Entities.ForEach((Entity entity, ref Scroll scroll) => {
                            scroll.Enable = false;
                        });

                        Entities.ForEach((Entity entity, ref GameOverPanel panel) => {
                            PostUpdateCommands.RemoveComponent<Disabled>(panel.GameOverLabel);
                            PostUpdateCommands.RemoveComponent<Disabled>(panel.RetrySprite);
                        });

                        game.GameOver = isGameOver;
                    }
                }

                game.Retry = false;
                game.Begun = false;
            });
        }
    }
}
