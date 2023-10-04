using Microsoft.Xna.Framework;

class Turtle : AnimatedGameObject
{
    protected float sneezeTime;
    protected float idleTime;
    private bool isAlive;

    public Turtle()
    {
        LoadAnimation("Sprites/Turtle/spr_sneeze@9", "sneeze", false);
        LoadAnimation("Sprites/Turtle/spr_idle", "idle", true);
        LoadAnimation("Sprites/Player/spr_explode@5x5", "explode", false, 0.04f);
        PlayAnimation("idle");
        Reset();
    }

    public override void Reset()
    {
        sneezeTime = 0.0f;
        idleTime = 5.0f;
        isAlive = true;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (isAlive)
        {
            if (sneezeTime > 0)
            {
                PlayAnimation("sneeze");
                sneezeTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (sneezeTime <= 0.0f)
                {
                    idleTime = 5.0f;
                    sneezeTime = 0.0f;
                }
            }
            else if (idleTime > 0)
            {
                PlayAnimation("idle");
                idleTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (idleTime <= 0.0f)
                {
                    idleTime = 0.0f;
                    sneezeTime = 5.0f;
                }
            }
            CheckPlayerInteractions();
        }
    }

 

    public void CheckPlayerInteractions()
    {
        BombProjectile bomb = GameWorld.Find("bomb") as BombProjectile;
        if (CollidesWith(bomb) && bomb.BombThrown)
        {
            Die();
            bomb.Reset();
        }
        Player player = GameWorld.Find("player") as Player;
        if (!CollidesWith(player))
        {
            return;
        }
        if (sneezeTime > 0)
        {
            player.Die(false);
        }
        else if (idleTime > 0 && player.Velocity.Y > 0)
        { 
            player.Jump(1500);
        }
    }
    public void Die()
    {
        isAlive = false;
        PlayAnimation("explode");
    }
}
