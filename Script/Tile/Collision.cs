using System;
using Microsoft.Xna.Framework;

namespace trails.Script;

public static class Collision
{

    public static Vector4 CheckForTileCollisions(float x, float y, int sizeX, int sizeY)
    {

        Vector4 collided = Vector4.Zero;

        int TileSizeX = (int)Math.Ceiling(sizeX * 0.125f);
        int TileSizeY = (int)Math.Ceiling(sizeY * 0.125f);
        
        float _sizeX = (sizeX * 0.125f) * 0.5f ;
        float _sizeY = (sizeY * 0.125f) * 0.5f;

        float tx = (x * 0.125f) + (Global.current_world._sizeX * 0.5f) + 0.5f;
        float ty = (y * 0.125f)+ (Global.current_world._sizeY * 0.5f) + 0.5f;

        if (CheckTile(tx - _sizeX , ty))
            collided.X = 1;
        if (CheckTile(tx + _sizeX, ty))
            collided.Y = 1;
       if (CheckTile(tx , ty- _sizeY ))
            collided.W = 1;
        if (CheckTile(tx , ty+ _sizeY))
            collided.Z = 1;
       
      return collided;
    }
    private static bool CheckTile(float x, float y)
    {
        return Global.current_world.GetTile((int)Math.Round((decimal)x), (int)Math.Round((decimal)y)).Collidable ;
    }
}