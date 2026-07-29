using System;
using Microsoft.Xna.Framework;
using TorraFramework.Core;

namespace trails.Script;

public static class Collision
{

    public static Vector4 CheckForTileCollisions(float x, float y, int sizeX, int sizeY)
    {
        Vector4 collided = Vector4.Zero;


        int TileSizeX = (int)Math.Ceiling(sizeX * 0.125f);
        int TileSizeY = (int)Math.Ceiling(sizeY * 0.125f);
        
        float _sizeX = sizeX * 0.125f * 0.5f;
        float _sizeY = sizeY * 0.125f * 0.5f;

        float tx = (x * 0.125f)  + 0.5f;
        float ty = (y * 0.125f) + 0.5f;

        if (CheckTile(tx, -_sizeX , ty, 0))
            collided.X = 1;
        if (CheckTile(tx, _sizeX, ty, 0))
            collided.Y = 1;
       if (CheckTile(tx , 0, ty ,-_sizeY ))
            collided.W = 1;
        if (CheckTile(tx , 0, ty ,_sizeY))
            collided.Z = 1;
      //AssetManager.QuickDraw(x, y, AssetManager.LoadTexture("Hitbox", AssetManager.GenerateTexture(sizeX * 12, sizeY * 12, Color.Purple)));

      return collided;
    }
    private static bool CheckTile(float x, float x2, float y, float y2)
    {   
        int nx = (int)Math.Round((decimal)(x + x2));
        int ny = (int)Math.Round((decimal)(y + y2));

        return Global.current_world.GetTile(nx, ny).Collidable ;
    }
}