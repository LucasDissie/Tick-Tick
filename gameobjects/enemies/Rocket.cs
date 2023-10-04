using Microsoft.Xna.Framework;

class Rocket : AnimatedGameObject
{
    protected double spawnTime;
    protected Vector2 startPosition;
    protected bool isAlive;


    public Rocket(bool moveToLeft, Vector2 startPosition)
    {
        LoadAnimation("Sprites/Rocket/spr_rocket@3", "default", true, 0.2f);
        LoadAnimation("Sprites/Player/spr_explode@5x5", "explode", false, 0.04f); 
        PlayAnimation("default");
        Mirror = moveToLeft;
        this.startPosition = startPosition;
        Reset();
    }

    public override void Reset()
    {
        visible = false;
        isAlive = true;
        position = startPosition;
        velocity = Vector2.Zero;
        PlayAnimation("default");
        spawnTime = GameEnvironment.Random.NextDouble() * 5;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (isAlive)
        {
            if (spawnTime > 0)
            {
                spawnTime -= gameTime.ElapsedGameTime.TotalSeconds;
                return;
            }
            visible = true;
            velocity.X = 600;
            if (Mirror)
            {
                this.velocity.X *= -1;
            }
            CheckPlayerInteractions();
            // check if we are outside the screen
            Rectangle screenBox = new Rectangle(0, 0, GameEnvironment.Screen.X, GameEnvironment.Screen.Y);
            if (!screenBox.Intersects(this.BoundingBox))
            {
                Reset();
            }
        }
    }

    public void CheckPlayerInteractions()
    {
        Player player = GameWorld.Find("player") as Player;
        if (CollidesWith(player) && player.Falling && visible && player.LastApex + 20 <= GlobalPosition.Y)
        {
            Die();
            player.Jump(player.Velocity.Y * 0.7f + 400);
        }
        else if (CollidesWith(player) && visible && isAlive)
        {
            player.Die(false);
        }
        BombProjectile bomb = GameWorld.Find("bomb") as BombProjectile;
        if (CollidesWith(bomb) && visible && bomb.BombThrown)
        {
            Die();
            bomb.Reset();
        }
    }
    public void Die()
    {
        velocity.X = 0;
        PlayAnimation("explode");
        isAlive = false;
    }
}
