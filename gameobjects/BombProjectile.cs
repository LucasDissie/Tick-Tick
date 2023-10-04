using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;



class BombProjectile : SpriteGameObject
{
    private bool bombThrown;
    private int resetTimer;
    public BombProjectile(int layer = 2, string id = "bomb") : base("Sprites/Projectiles/spr_bombProjectile", layer, id)
    {
        bombThrown = false;
        velocity = Vector2.Zero;
        position = Vector2.Zero;
    }
    public override void Update(GameTime gameTime)
    {
        Player player = GameWorld.Find("player") as Player;
        base.Update(gameTime);
        if (bombThrown)
        {
            ThrowBomb();
            if (resetTimer <= 0) Reset();
            resetTimer--;
        }
        else if (player.Mirror)
        {
            position.X = player.GlobalPosition.X - 69;
            position.Y = player.GlobalPosition.Y - 46;
        }
        else
        { 
            position.X = player.GlobalPosition.X + 35;
            position.Y = player.GlobalPosition.Y - 46;
        }
        //Debug.WriteLine(BoundingBox.Center);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (inputHelper.KeyPressed(Keys.F))
        {
            bombThrown = true;
            resetTimer = 120;
        }
    }
    public override void Reset()
    {
        base.Reset();
        velocity = new Vector2(0, 0);
        position = Vector2.Zero;
        bombThrown = false;
    }
    public void ThrowBomb()
    {
        Player player = GameWorld.Find("player") as Player;
        if (!player.Mirror) velocity = new Vector2(999, 0);
        else velocity = new Vector2(-999, 0);
    }
    public bool BombThrown
    {
        get { return bombThrown; }
    }
}
