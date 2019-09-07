using Unity.Entities;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
using Unity.Mathematics;

namespace TinyRun {
    public class CharacterJumpSystem : ComponentSystem {
        protected override void OnUpdate() {
            var deltaTime = World.TinyEnvironment().frameDeltaTime;
            var pressedJumpKey = false;
            var pressedDownArrow = false;
            var pressingDownArrow = false;
            var isGameOver = false;

            Entities.ForEach((ref GameState game) => {
                isGameOver = game.GameOver;
            });

            if (isGameOver) {
                return;
            }

            Entities.ForEach((Entity entity, ref Joystick joystick) => {
                if (joystick.IsPressedDownArrowKey) {
                    pressedDownArrow = true;
                }
                else if (joystick.IsPressingDownArrowKey) {
                    pressingDownArrow = true;
                }
                else if (joystick.IsPressed || joystick.IsPressingSpaceKey || joystick.IsPressedSpaceKey || joystick.IsPressingUpArrowKey || joystick.IsPressedUpArrowKey) {
                    pressedJumpKey = true;
                }
            });

            Entities.ForEach((Entity entity, ref Character character, ref Jump jump, ref Translation transformPosition) => {
                var pos = transformPosition.Value;
                var gravityScale = 1.0f;

                character.jumpingPrev = character.jumping;

                if (pressedDownArrow) {
                    if (character.jumping) {
                        if(jump.v > 0.0f) {
                            jump.v = 0.0f;
                        }
                        gravityScale = 4.0f;
                    }
                }
                else if (pressingDownArrow) {
                    if (character.jumping) {
                        gravityScale = 4.0f;
                    }
                }
                else {
                    if (!character.jumping && pressedJumpKey) {
                        character.jumping = true;
                        jump.v = jump.v0;
                    }

                    if (!character.jumping) {
                        return;
                    }
                }

                if (character.jumping) {
                    pos.y += jump.v * deltaTime;

                    if (!pressedJumpKey || jump.t > jump.durationIgnoreGravity || jump.v <= 0.0f) {
                        jump.v = jump.v + jump.g * gravityScale * deltaTime;
                    }

                    jump.t += deltaTime;

                    if (pos.y < jump.floorY) {
                        pos.y = jump.floorY;
                        jump.v = 0.0f;
                        jump.t = 0.0f;
                        character.jumping = false;
                    }

                    transformPosition.Value = pos;
                }
            });
        }
    }
}
