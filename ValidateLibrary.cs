using System;
using System.Linq;

namespace MusicLibraryTool
{
    public class ValidateLibrary
    {
        private string LibraryPath;
        public ValidateLibrary(string LibraryPath){
            this.LibraryPath = LibraryPath;
        }

        public void Validate() {
            var tracks = Library.GetAllTracks(LibraryPath);

            Console.ForegroundColor = ConsoleColor.Red;
            foreach(var track in tracks){
                if(!track.IsValid){
                    Console.WriteLine(track.FolderNameFull+" with track "+track.TrackNameFull+ " is Invalid because:");
                    foreach(var error in track.ValidationMessages){
                        Console.WriteLine("  "+error);
                    }
                }
            }
            Console.WriteLine("");
            Console.WriteLine("Amount of invalid Tracks: {0}", tracks.Where(x => !x.IsValid).ToList().Count);
            Console.ForegroundColor = ConsoleColor.Gray;

            var TracksAmount = tracks.Count;
            var TracksFlacAmount = tracks.Where(x => x.TrackType == TrackType.FLAC).ToList().Count;
            decimal TracksFlacPercentage = Math.Round(( (decimal)TracksFlacAmount / (decimal)TracksAmount) * 100, 2);
            var TracksWithoutCoverJpgAmount = tracks.Where(x => !x.CoverArtJpg).ToList().Count;
            decimal TracksCoverJpgPercentage = Math.Round(( (decimal)TracksWithoutCoverJpgAmount / (decimal)TracksAmount) * 100, 2);

            Console.WriteLine("");
            Console.WriteLine("Amount of Tracks: {0}", TracksAmount);
            Console.WriteLine("Amount of Flac Tracks: {0} ({1}%)", TracksFlacAmount, TracksFlacPercentage);
            Console.WriteLine("Amount of Tracks without a cover.jpg: {0} ({1}%)", TracksWithoutCoverJpgAmount, TracksCoverJpgPercentage);
        }
    }
}