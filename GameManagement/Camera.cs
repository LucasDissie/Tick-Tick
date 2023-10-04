using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class Camera 
{
    private static Vector2 cameraposition;
    public void Draw()
    {
    }
    public Camera()
    {
        cameraposition.X = 0;
        cameraposition.Y = 0;
    }
    public void HandleInput()
    {
    }

    public Vector2 CameraPos
    {
        get { return cameraposition; }
        set { cameraposition = value; }
    }
}
