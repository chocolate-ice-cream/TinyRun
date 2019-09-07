using Unity.Entities;
using Unity.Tiny.Core2D;
using Unity.Tiny.UIControls;
using Unity.Tiny.HitBox2D;
using Unity.Mathematics;

namespace TinyRun {
    public class GameOverSystem : ComponentSystem {
        protected override void OnUpdate() {
            Entities.ForEach((ref GameState game) => {
                var retry = false;

                if (game.Playing && game.GameOver) {
                    Entities.WithAll<RetryButton>().ForEach((Entity entity, ref PointerInteraction pointerInteraction) =>
                    {
                        retry = pointerInteraction.clicked;
                        pointerInteraction.clicked = false;
                    });

                    Entities.ForEach((ref Joystick joystick) => {
                        if (joystick.IsPressedSpaceKey || joystick.IsPressedUpArrowKey) {
                            retry = true;
                            joystick.IsPressedSpaceKey = false;
                            joystick.IsPressingSpaceKey = false;
                            joystick.IsPressedUpArrowKey = false;
                            joystick.IsPressingUpArrowKey = false;
                        }
                    });

                    if (retry) {
                        Entities.ForEach((Entity panelEntity, ref GameOverPanel panel) => {
                            PostUpdateCommands.AddComponent(panel.GameOverLabel, new Disabled());
                            PostUpdateCommands.AddComponent(panel.RetrySprite, new Disabled());
                        });

                        // キャラクターを初期状態に設定
                        Entities.ForEach((Entity entity, ref Character character, ref Jump jump, ref Translation charaTranslation) => {
                            character.jumping = false;
                            charaTranslation.Value = new float3(-2.5f, 0.63f, 0.0f);

                            jump.t = 0.0f;
                            jump.v = 0.0f;

                            if (EntityManager.HasComponent<HitBoxOverlap>(entity)) {
                                PostUpdateCommands.RemoveComponent<HitBoxOverlap>(entity);
                            }
                        });

                        // 床、BGを初期位置に設定
                        Entities.ForEach((ref Scroll scroll, ref Translation scrollTranslation) => {
                            var startPos = scroll.StartPosition;
                            scrollTranslation.Value = new float3(startPos.x, startPos.y, 0.0f);
                            scroll.Speed = scroll.DefaultSpeed;
                            scroll.CurrentDistance = float2.zero;
                            scroll.Enable = true;
                        });

                        // 障害物を削除
                        Entities.ForEach((Entity entity, ref Obstacle obstacle, ref Translation obstacleTranslation) => {
                            if (!obstacle.IsPrefab) {
                                PostUpdateCommands.DestroyEntity(entity);
                            }
                        });

                        game.CurrentScore = 0;
                        game.Elapse = 0.0f;
                        game.SpeedScale = 1.0f;
                        game.Playing = retry;
                        game.GameOverPrev = false;
                        game.GameOver = false;
                        game.Retry = true;
                    }
                }
            });
        }
    }
}
