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
        
        float player_tile_x = x * 0.125f;
        float player_tile_y = y * 0.125f;

        float _sizeX = (sizeX * 0.125f) * 0.5f;
        float _sizeY = (sizeY * 0.125f) * 0.5f;

        int wx = (int)(Global.current_world._sizeX * 0.5f);
        int wy = (int)(Global.current_world._sizeY * 0.5f);
        
        int player_to_worldX =(int)Math.Ceiling((x + 0.5f) * 0.125f);
        int player_to_worldY =  (int)Math.Ceiling((y + 0.5f) * 0.125f);

      /*  int PLAYER_TILEX = player_to_worldX + wx;
        int PLAYER_TILEY = player_to_worldY + wy;

        int PLAYER_DRAW_TILEX =(player_to_worldX*8 )-4;
        int PLAYER_DRAW_TILEY = (player_to_worldY *8)-4;

      //  AssetManager.QuickDraw(PLAYER_DRAW_TILEX, PLAYER_DRAW_TILEY, 8, 8, Color.AliceBlue);
        AssetManager.QuickDraw(((x + 0.5f) ), ((y + 0.5f) ), 1, 1, Color.IndianRed);*/
        //Global.current_world.SetTile(PLAYER_TILEX, PLAYER_TILEY, TileID.Sunchain);

        if (GetTile((x*0.125f)+ _sizeX, (y*0.125f) ).Collidable)
            collided.X = -Main.DistanceFrom((x*0.125f)+ _sizeX, GetTilePosX(((x + 0.5f) * 0.125f) - _sizeX)) * 8; 
            //-Main.DistanceFrom(GetTilePosX(((x + 0.5f) * 0.125f) - _sizeX), (x*0.125f)+ _sizeX) * 8; 
        if (GetTile((x*0.125f)- _sizeX, (y*0.125f) ).Collidable)
            collided.Y = Main.DistanceFrom(GetTilePosX(((x + 0.5f) * 0.125f) + _sizeX), (x*0.125f)+ _sizeX) * 4; 
            //Main.DistanceFrom(GetTilePosX(((x + 0.5f) * 0.125f) + _sizeX), (x*0.125f)+ _sizeX) * 4; 

        if (GetTile((x*0.125f), (y*0.125f) + _sizeY).Collidable)
            collided.W = -Main.DistanceFrom((y*0.125f)+ _sizeY, GetTilePosY(((y + 0.5f) * 0.125f) - _sizeY)) * 8; 
        if (GetTile((x*0.125f), (y*0.125f) - _sizeY).Collidable)
            collided.Z = Main.DistanceFrom(GetTilePosY(((y + 0.5f) * 0.125f) + _sizeY), (y*0.125f)+ _sizeY) * 4; 


        return collided;
    }
    private static Tile GetTile(float x, float y)
    {   
        int nx = (int)Math.Ceiling((float)(x ));
        int ny = (int)Math.Ceiling((float)(y  ));
      //  AssetManager.QuickDraw((int)Math.Ceiling((float)(nx* 8))-4, (int)Math.Ceiling((float)(ny* 8))-4 , 8, 8, new Color(100, 100, 100, 100));

       // Global.current_world.SetTile(nx, ny, TileID.Fractal);
        return Global.current_world.GetTile(nx, ny) ;
    }
    private static int GetTilePosX(float x)
    {   
        int nx = (int)Math.Ceiling((float)(x));
        //Global.current_world.SetTile(nx + (int)(Global.current_world._sizeX * 0.5f), (int)(Global.current_world._sizeY * 0.5f)-5, TileID.Fractal);
        return nx  ;
    }
  private static int GetTilePosY(float y)
    {   
        int ny = (int)Math.Ceiling((float)(y));
        //Global.current_world.SetTile(nx + (int)(Global.current_world._sizeX * 0.5f), (int)(Global.current_world._sizeY * 0.5f)-5, TileID.Fractal);
        return ny;
    }
}