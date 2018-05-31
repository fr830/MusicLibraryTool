using System;
using System.Linq;

namespace MusicLibraryTool
{
    public class CoverArtCheck
    {
        private string LibraryPath;
        public CoverArtCheck(string libraryPath){
            LibraryPath = libraryPath;
        }
        public void Check(){
            var tracks = Library.GetAllTracks(LibraryPath);

            var albums = tracks.Select(x => new {x.CoverArtJpg, x.FolderNameFull}).GroupBy(x => x.FolderNameFull).ToList();

            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach(var album in albums){
                if(!album.FirstOrDefault().CoverArtJpg){
                    Console.WriteLine("No cover.jpg found for: {0}", album.FirstOrDefault().FolderNameFull);
                }
            }
            Console.WriteLine("");
            Console.WriteLine("Amount of albums without cover.jpg: {0}", albums.Where(x=>!x.FirstOrDefault().CoverArtJpg).ToList().Count);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}