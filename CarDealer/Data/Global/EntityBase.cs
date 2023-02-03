using System;

namespace Global.Repository
{
   public abstract class EntityBase
   {
       public int Id { get; set; }
       public bool IsLoadedFromDB { get; set; }

       public string reaction { get; set; }
    }
}
