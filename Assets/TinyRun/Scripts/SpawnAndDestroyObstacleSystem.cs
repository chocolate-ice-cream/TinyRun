using Unity.Entities;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
using Unity.Mathematics;
using Unity.Collections;

namespace TinyRun {
    public class SpawnAndDestroyObstacleSystem : ComponentSystem {
        Random _random;

        protected override void OnCreate() {
            _random = new Random();
            _random.InitState();
        }

        protected override void OnUpdate() {
            var begun = false;
            var gameOver = false;

            Entities.ForEach((ref GameState game) => {
                begun = game.Playing;
                gameOver = game.GameOver;
            });

            if (!begun || gameOver) {
                return;
            }

            var lastObstaclePos = new float3(5.0f, 0.0f, 0.0f);
            var existObstacleCount = 0;

            Entities.ForEach((Entity entity, ref Obstacle obstacle, ref Translation translation) => {
                if (!obstacle.IsPrefab) {
                    if (translation.Value.x < -6.5f) {
                        PostUpdateCommands.DestroyEntity(entity);
                    }
                    else {
                        if (translation.Value.x > lastObstaclePos.x) {
                            lastObstaclePos = translation.Value;
                        }

                        ++existObstacleCount;
                    }
                }
            });

            if (existObstacleCount < 4) {
                var prefabs = new NativeArray<Entity>(4, Allocator.Temp);
                var count = 0;

                Entities.ForEach((Entity obstacleEntity, ref Obstacle obstacleComponent) => {
                    if (obstacleComponent.IsPrefab) {
                        prefabs[count] = obstacleEntity;
                        ++count;
                    }
                });

                var spawnObstacle = EntityManager.Instantiate(prefabs[_random.NextInt(prefabs.Length)]);

                var translation = EntityManager.GetComponentData<Translation>(spawnObstacle);
                if (existObstacleCount == 0) {
                    translation.Value = lastObstaclePos;
                }
                else {
                    var speedScale = 1.0f;
                    Entities.ForEach((ref GameState game) => {
                        speedScale = game.SpeedScale;
                    });
                    var offset = 0.0f;
                    if(speedScale < 1.75f) {
                        offset = _random.NextFloat(7.5f, 10.0f);
                    }
                    else {
                        offset = _random.NextFloat(10.0f, 12.5f);
                    }

                    translation.Value = new float3(lastObstaclePos.x + offset, 0.0f, 0.0f);
                }
                EntityManager.SetComponentData(spawnObstacle, translation);

                var scroll = EntityManager.GetComponentData<Scroll>(spawnObstacle);
                var startPos = translation.Value;
                scroll.StartPosition = new float2(startPos.x, startPos.y);
                scroll.Distance = 100.0f + startPos.x;
                scroll.Enable = true;
                lastObstaclePos = startPos;
                EntityManager.SetComponentData(spawnObstacle, scroll);

                var obstacle = EntityManager.GetComponentData<Obstacle>(spawnObstacle);
                obstacle.IsPrefab = false;
                EntityManager.SetComponentData(spawnObstacle, obstacle);

                prefabs.Dispose();
            }
        }
    }
}
