using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nhbernet.Models;

namespace nhbernet.Query
{
   public interface userInterface
    {
       List<user> list();
       void save(user u);
       user getuser(int id);

       void update(user u);
       void delete(user u);

       List<city> citylist();
       List<state> statelist();

       void savecity(city city);
       void savestate(state state);
    }
}
