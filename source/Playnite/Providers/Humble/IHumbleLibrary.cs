using Playnite.Database;
using Playnite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playnite.Providers.Humble
{
    public interface IHumbleLibrary
    {

       

        List<IGame> GetInstalledGames();

        List<IGame> GetLibraryGames();

    }
}
