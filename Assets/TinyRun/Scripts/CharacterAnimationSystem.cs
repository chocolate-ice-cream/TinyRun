using Unity.Entities;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;

namespace TinyRun {
    public class CharacterAnimationSystem : ComponentSystem {
        protected override void OnUpdate() {
            Entities.ForEach((ref Animations anims) => {
                var sequenceIdleEntity = anims.SequenceIdleEntity;
                var sequenceRunEntity = anims.SequenceRunEntity;
                var sequenceJumpEntity = anims.SequenceJumpEntity;
                var sequenceDeathEntity = anims.SequenceDeathEntity;
                var isGameOver = false;
                var isPlaying = false;
                var begun = false;
                var retry = false;

                Entities.ForEach((ref GameState game) => {
                    isGameOver = (!game.GameOverPrev && game.GameOver);
                    isPlaying = game.Playing;
                    begun = game.Begun;
                    retry = game.Retry;
                });

                Entities.ForEach((ref Character character, ref Sprite2DSequencePlayer sequencePlayer) => {
                    //if (isPlaying) {
                    if (true) {
                        if (isGameOver) {
                            // ゲームオーバー
                            sequencePlayer.time = 0.0f;
                            sequencePlayer.loop = LoopMode.ClampForever;
                            sequencePlayer.sequence = sequenceDeathEntity;
                        }
                        else if (begun) {
                            //sequencePlayer.time = 0.0f;
                            //sequencePlayer.loop = LoopMode.Loop;
                            //sequencePlayer.sequence = sequenceRunEntity;

                            // ジャンプ
                            sequencePlayer.time = 0.0f;
                            sequencePlayer.loop = LoopMode.ClampForever;
                            sequencePlayer.sequence = sequenceJumpEntity;
                        }
                        else if (retry) {
                            // 走り
                            sequencePlayer.time = 0.0f;
                            sequencePlayer.loop = LoopMode.Loop;
                            sequencePlayer.sequence = sequenceRunEntity;
                        }
                        else {
                            if (!character.jumpingPrev && character.jumping) {
                                // ジャンプ
                                sequencePlayer.time = 0.0f;
                                sequencePlayer.loop = LoopMode.ClampForever;
                                sequencePlayer.sequence = sequenceJumpEntity;
                            }
                            else if (character.jumpingPrev && !character.jumping) {
                                // 着地
                                sequencePlayer.time = 0.0f;
                                sequencePlayer.loop = LoopMode.Loop;
                                sequencePlayer.sequence = sequenceRunEntity;
                            }
                        }
                    }
                });
            });
        }
    }
}
