using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MusicLibraryTool
{
    public class GeneratePlaylists
    {
        private string RootPath;
        private string LibraryPath;
        private string PlaylistPath;
        private List<Playlist> Playlists;
        private List<Track> Tracks;
        public GeneratePlaylists(string rootPath, string libraryPath, string playlistPath){
            RootPath = rootPath;
            LibraryPath = libraryPath;
            PlaylistPath = playlistPath;
        }

        public void Generate() {
            //Convert m3u8 playlists to collection playlists
            //[^\\]*\[.*\] (.*?) \- (.*?) \[.*\]\\([^\\]*)\..*$


            Playlists = Library.GetAllPlaylists(PlaylistPath, RootPath, PlaylistPath);
            Tracks = Library.GetAllTracks(LibraryPath);

            RecreatePlaylistDirectories(Playlists);

            var albumPlaylists = Playlists.Where(x => x.PlaylistType == PlaylistType.Album).ToList();
            GenerateAlbumPlaylists(albumPlaylists);

            var artistPlaylists = Playlists.Where(x => x.PlaylistType == PlaylistType.Artist).ToList();
            //GenerateArtistPlaylists(artistPlaylists);
            
            var collectionPlaylists = Playlists.Where(x => x.PlaylistType == PlaylistType.Collection).ToList();
            //GenerateCollectionPlaylists(collectionPlaylists);
        }

        private void GenerateArtistPlaylists(List<Playlist> albumPlaylists){
            foreach(var albumPlaylist in albumPlaylists){
                Console.WriteLine("Generating playlist {0}", albumPlaylist.PlaylistDefinitionNameFull);
                var tracks = Tracks.Where(x => x.FolderNameFull.Contains(albumPlaylist.PlaylistName)).ToList();
                CreatePlaylistFile(albumPlaylist, tracks);
            }
        }

        private void GenerateAlbumPlaylists(List<Playlist> albumPlaylists){
            foreach(var albumPlaylist in albumPlaylists){
                Console.WriteLine("Generating playlist {0}", albumPlaylist.PlaylistDefinitionNameFull);
                var tracks = Tracks.Where(x => x.FolderNameFull.Contains(albumPlaylist.PlaylistName)).ToList();
                CreatePlaylistFile(albumPlaylist, tracks);
            }
        }

        private void GenerateCollectionPlaylists(List<Playlist> albumPlaylists){
            foreach(var albumPlaylist in albumPlaylists){
                Console.WriteLine("Generating playlist {0}", albumPlaylist.PlaylistDefinitionNameFull);
                var tracks = Tracks.Where(x => x.FolderNameFull.Contains(albumPlaylist.PlaylistName)).ToList();
                CreatePlaylistFile(albumPlaylist, tracks);
            }
        }

        private void CreatePlaylistFile(Playlist playlist, List<Track> tracks){ 
            var file = File.Create(playlist.PlaylistImplementationPath);
            using(var Writer = new StreamWriter(file)){
                foreach(var track in tracks){
                    Writer.WriteLine(track.TrackPath.Replace(RootPath,".."));
                }
            }
        }

        private void RecreatePlaylistDirectories(List<Playlist> playlists){
            var directories = playlists.Select(x => new { Directory.GetParent(x.PlaylistDefinitionPath).FullName }).GroupBy(x => x.FullName).ToList();

            //Delete Folder if existing
            Console.WriteLine("Deleting Playlist folders");
            foreach(var dir in directories){
                //PlaylistPhysicalPaths
                var PlaylistImplementationPath = dir.FirstOrDefault().FullName.Replace(PlaylistPath,RootPath);
                Console.Write("Deleting: {0}... ", PlaylistImplementationPath);
                try {
                    DeleteDirectory(PlaylistImplementationPath);
                }
                catch{
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Could not delete: {0}  - New playlist?", PlaylistImplementationPath);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.WriteLine("Creating: {0}...", PlaylistImplementationPath);
                try {
                    Directory.CreateDirectory(PlaylistImplementationPath);
                }
                catch {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Could not create: {0}", PlaylistImplementationPath);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
        }

        private static void DeleteDirectory(string target_dir){
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }
    }
}