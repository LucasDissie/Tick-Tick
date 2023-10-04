using Microsoft.Xna.Framework;
using System;
partial class Level : GameObjectList
{

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (quitButton.Pressed)
        {
            Reset();
            GameEnvironment.GameStateManager.SwitchTo("levelMenu");
        }      
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        TimerGameObject timer = Find("timer") as TimerGameObject;
        Player player = Find("player") as Player;

        // check if we died
        if (!player.IsAlive)
        {
            timer.Running = false;
        }
        if (player.GlobalPosition.X + GameEnvironment.Screen.X / 2 <= levelwidth * GameEnvironment.Screen.X / 20 && player.GlobalPosition.X  - GameEnvironment.Screen.X / 2 >= 0)
        {
            Vector2 OldCamPos = GameEnvironment.Camera.CameraPos;
            GameEnvironment.Camera.CameraPos = new Vector2(player.GlobalPosition.X - GameEnvironment.Screen.X / 2, GameEnvironment.Camera.CameraPos.Y);
            if (GameEnvironment.Camera.CameraPos.X > OldCamPos.X)
            {
                for (int i = 0; i < levelwidth / 5 + 1; i++)
                {
                    SpriteGameObject mountain = Find("mountain" + i) as SpriteGameObject;
                    mountain.Velocity = new Vector2(Math.Abs(mountain.Layer - 4) * 30, 0);
                 
                }
            }
            else if (GameEnvironment.Camera.CameraPos.X < OldCamPos.X)
            {
                for (int i = 0; i < levelwidth / 5 + 1; i++)
                {
                    SpriteGameObject mountain = Find("mountain" + i) as SpriteGameObject;
                    mountain.Velocity = new Vector2(Math.Abs(mountain.Layer - 4) * -30, 0);
                }
            }
            else
            {
                for (int i = 0; i < levelwidth / 5 + 1; i++)
                {
                    SpriteGameObject mountain = Find("mountain" + i) as SpriteGameObject;
                    mountain.Velocity = new Vector2(0, 0);
                }
            }
        }
        else
        {
            for (int i = 0; i < levelwidth / 5 + 1; i++)
            {
                SpriteGameObject mountain = Find("mountain" + i) as SpriteGameObject;
                mountain.Velocity = new Vector2(0, 0);
            }
        }
        // check if we ran out of time
        if (timer.GameOver)
        {
            player.Explode();
        }
                       
        // check if we won
        if (Completed && timer.Running)
        {
            player.LevelFinished();
            timer.Running = false;
        }
    }

    public override void Reset()
    {
        base.Reset();
        GameEnvironment.Camera.CameraPos = new Vector2(0, 0);
        VisibilityTimer hintTimer = Find("hintTimer") as VisibilityTimer;
        hintTimer.StartVisible();
    }
}
