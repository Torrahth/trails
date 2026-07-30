using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace TorraFramework.Core;

public static class EntityManager
{

    static List<Entity> entities = new List<Entity>();
 
    public static int CreateEntity(Entity entity)
    {
        if (entity == null)
            return -1;


        entities.Add(entity);
        return 0; 
    }
       public static int CreateEntity(Entity entity, Microsoft.Xna.Framework.Vector2 Position)
        {
        if (entity == null)
            return -1;

        entity.Position = Position;
        entities.Add(entity);
        return 0; 
    }
    public static void Draw(SpriteBatch spritebatch)
    {
        foreach (Entity entity in entities)
        {
            entity.Draw(spritebatch);
        }
    }
}