using System;
using System.Net.Sockets;
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
       // Console.WriteLine($"size = {TileSizeX}, {TileSizeY}");
        float _sizeX = (sizeX * 0.125f) * 0.5f;
        float _sizeY = (sizeY * 0.125f) * 0.5f;

        float tx = (x * 0.125f);//+ (Global.current_world._sizeX * 0.5f) ;
        float ty = (y * 0.125f);//+ (Global.current_world._sizeX * 0.5f) ;

        
      /*  // top corners
        if (CheckTile(tx, +_sizeX - 0.1f , ty,  - 0.8f))
        {
             collided.X = GetTilePosX(tx+ 0.1f, -_sizeX) - tx;//;
             collided.Z= 1;
        }
        if (CheckTile(tx, _sizeX- 0.1f, ty, - 0.8f))
        {
            collided.Y = GetTilePosX(tx- 0.1f, -_sizeX) - tx;//tx -_sizeX;
             collided.Z = 1;
        }
        AssetManager.QuickDraw((tx+ 0.1f -_sizeX)* 8 , (ty - 0.8f)  * 8);    
        AssetManager.QuickDraw((tx- 0.1f + _sizeX)* 8 , (ty- 0.8f) * 8);    */
       AssetManager.QuickDraw((tx )* 8 , (ty)  * 8);    

        for (int _y = 0; _y < TileSizeY; ++_y)
        {
            
            if (CheckTile(tx, -_sizeX , ty, -_y)) // tile to the LEFT
                collided.X = -(tx - GetTilePosX(tx, 1));// (tx - _sizeX)-(GetTilePosX(tx, -_sizeX -(Global.current_world._sizeX * 0.5f)))  ;//Main.DistanceFrom(tx - _sizeX, (GetTilePosX(tx, -_sizeX -(Global.current_world._sizeX * 0.5f))) + _sizeX) ) ;
                // Console.WriteLine($"die: { tx }, {GetTilePosX(tx, -(Global.current_world._sizeX * 0.5f)) }");
                //Console.WriteLine($"die: { Main.DistanceFrom(tx , GetTilePosX(tx, -(Global.current_world._sizeX * 0.5f)))}");
            if (CheckTile(tx, _sizeX, ty, -_y)) // tile to the RIGHT
                collided.Y  = (tx - GetTilePosX(tx, 1));//(tx + _sizeX)-(GetTilePosX(tx, _sizeX -(Global.current_world._sizeX * 0.5f)));//(Main.DistanceFrom(tx + _sizeX, (GetTilePosX(tx, _sizeX -(Global.current_world._sizeX * 0.5f))) - _sizeX)  );
            AssetManager.QuickDraw((tx -_sizeX)* 8 , (ty -_y+ 0.5f)  * 8);    
            AssetManager.QuickDraw((tx + _sizeX)* 8 , (ty -_y+ 0.5f) * 8);    
        }
        for (int _x = 0; _x < TileSizeX; ++_x)
        {
            if (CheckTile(tx , _x, ty ,_sizeY )) // down
                collided.W = 1;
            if (CheckTile(tx , _x, ty ,-_sizeY)) // up
                collided.Z = 1;
            AssetManager.QuickDraw((tx +_x)* 8 , (ty -_sizeY ) * 8);    
            AssetManager.QuickDraw((tx + _x)* 8 , (ty + _sizeY) * 8);    
        }
       /* // bottom corners
        if (CheckTile(tx, -_sizeX + 0.1f , ty,  + 0.8f))
        {
             collided.X = GetTilePosX(tx, -_sizeX+ 0.1f) - tx; //tx -_sizeX;
             collided.W= 1;
        }
        if (CheckTile(tx, _sizeX- 0.1f, ty, + 0.8f))
        {
            collided.Y = GetTilePosX(tx, -_sizeX- 0.1f) - tx; //tx - _sizeX;
             collided.W = 1;
        }
        AssetManager.QuickDraw((tx -_sizeX + 0.1f)* 8 , (ty + 0.8f)  * 8);    
        AssetManager.QuickDraw((tx + _sizeX- 0.1f)* 8 , (ty+ 0.8f) * 8);    */

      //AssetManager.QuickDraw(x, y, AssetManager.LoadTexture("Hitbox", AssetManager.GenerateTexture(sizeX * 12, sizeY * 12, Color.Purple)));

      return collided;
    }
    private static bool CheckTile(float x, float x2, float y, float y2)
    {   
        int nx = (int)Math.Ceiling((float)(x + x2));
        int ny = (int)Math.Ceiling((float)(y + y2));

       // Global.current_world.SetTile(nx, ny, TileID.Fractal);
        return Global.current_world.GetTile(nx, ny).Collidable ;
    }
    private static int GetTilePosX(float x, float x2)
    {   
        int nx = (int)Math.Ceiling((float)(x + x2));
        Global.current_world.SetTile(nx + (int)(Global.current_world._sizeY * 0.5f), (int)(Global.current_world._sizeY * 0.5f)-5, TileID.Fractal);
        return nx  ;
    }
    private static int GetTilePosY(float y, float y2)
    {   
        int ny = (int)Math.Round((float)(y + y2+ (Global.current_world._sizeY * 0.5f)));
       // Global.current_world.SetTile(nx, ny, TileID.Fractal);
        return ny;
    }
}