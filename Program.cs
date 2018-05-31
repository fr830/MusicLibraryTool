using System;
using System.IO;
using System.Linq;

namespace MusicLibraryTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("");
            Console.WriteLine("Starting MusicLibraryTool");
            string RootPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName; 
            Console.WriteLine("RootPath: " + RootPath);
            string LibraryPath = RootPath + "\\ZLibrary";
            Console.WriteLine("ZLibraryPath: " + LibraryPath);
            string PlaylistPath = RootPath + "\\ZPlaylists";
            Console.WriteLine("ZPlaylistPath: " + PlaylistPath);
            Console.WriteLine("");

            if(args.Contains("validate")) {
                Console.WriteLine("Starting validation...");
                new ValidateLibrary(LibraryPath).Validate();
            } else if(args.Contains("generate")) {
                Console.WriteLine("Starting generation...");
                new GeneratePlaylists(RootPath, LibraryPath, PlaylistPath).Generate();
            } else if(args.Contains("withoutcover")) {
                Console.WriteLine("Starting withoutcover...");
                new CoverArtCheck(LibraryPath).Check();
            } else {
                Console.WriteLine("Available Commands:");
                Console.WriteLine("  validate      - Validates library albums on naming and included file types.");
                Console.WriteLine("  withoutcover  - List folders without cover.jpg");
                Console.WriteLine("  generate      - Regenerates playlists from ZPlaylist files.");
            }

            Console.WriteLine("");
        }
    }
}
