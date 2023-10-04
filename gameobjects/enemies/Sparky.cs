using Microsoft.Xna.Framework;

class Sparky : AnimatedGameObject
{
    protected float idleTime;
    protected float yOffset;
    protected float initialY;
    private bool isAlive;

    public Sparky(float initialY)
    {
        LoadAnimation("Sprites/Sparky/spr_electrocute@6x5", "electrocute", false);
        LoadAnimation("Sprites/Sparky/spr_idle", "idle", true);
        LoadAnimation("Sprites/Player/spr_explode@5x5", "explode", false, 0.04f);
        PlayAnimation("idle");
        this.initialY = initialY;
        Reset();
    }

    public override void Reset()
    {
        idleTime = (float)GameEnvironment.Random.NextDouble() * 5;
        position.Y = initialY;
        yOffset = 120;
        velocity = Vector2.Zero;
        isAlive = true; 
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (isAlive)
        {
            if (idleTime <= 0)
            {
                PlayAnimation("electrocute");
                if (velocity.Y != 0)
                {
                    // falling down
                    yOffset -= velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (yOffset <= 0)
                    {
                        velocity.Y = 0;
                    }
                    else if (yOffset >= 120.0f)
                    {
                        Reset();
                    }
                }
                else if (Current.AnimationEnded)
                {
                    velocity.Y = -60;
                }
            }
            else
            {
                PlayAnimation("idle");
                idleTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (idleTime <= 0.0f)
                {
                    velocity.Y = 300;
                }
            }

            CheckPlayerInteractions();
        }
    }    

    public void CheckPlayerInteractions()
    {
        Player player = GameWorld.Find("player") as Player;
        if (CollidesWith(player) && idleTime <= 0)
        {
            player.Die(false);
        }
        BombProjectile bomb = GameWorld.Find("bomb") as BombProjectile;
        if (CollidesWith(bomb) && bomb.BombThrown)
        {
            Die();
            bomb.Reset();
            velocity.Y = 0;
        }
    }
    public void Die()
    {
        isAlive = false;
        PlayAnimation("explode");
    }
}
