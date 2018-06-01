using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MusicLibraryTool
{
    public class Track
    {
        public bool IsValid { get { return ValidationMessages.Count==0; } }
        public List<string> ValidationMessages = new List<string>();
        
        public Track(string path){
            FolderPath = Directory.GetParent(path).FullName;
            FolderNameFull = FolderPath.Split('\\')[FolderPath.Split('\\').Length-1];
            FolderTrackTypes = Library.StringToTrackTypes(FolderNameFull);

            TrackPath = path;
            TrackNameFull = path.Split('\\')[path.Split('\\').Length-1];
            TrackType = Library.StringToTrackType(TrackNameFull);

            Artist = Regex.Match(FolderNameFull, "\\[[^\\]]*?\\] ([^\\[]*) - ([^\\[]*) \\[").Groups[1].Value;
            Album = Regex.Match(FolderNameFull, "\\[[^\\]]*?\\] ([^\\[]*) - ([^\\[]*) \\[").Groups[2].Value;
            TrackName = Regex.Match(TrackNameFull, ".+(?=\\.)").Value;

            CoverArtJpg = Validator.HasCoverJpg(FolderPath);

            Validate();
        }
        //FolderInfo
        public string FolderPath { get; }
        public string FolderNameFull { get; }
        public List<TrackType> FolderTrackTypes { get; }
        //TrackFileInfo
        public string TrackPath { get; }
        public string TrackNameFull { get; }
        public TrackType TrackType { get; }
        public bool CoverArtJpg { get; }
        //TrackInfo
        public string Artist { get; }
        public string Album { get; }
        public string TrackName { get; } //Filename without extention and trimmed

        private void Validate() { 
            Validator.FolderName(this.FolderNameFull, ref ValidationMessages);
            Validator.NoSubFolders(this.FolderPath, ref ValidationMessages);
            Validator.TrackTypeInFolderTrackTypes(TrackType, FolderTrackTypes, ref ValidationMessages);
        }
    }

    public class Playlist
    {
        public bool IsValid { get { return ValidationMessages.Count==0; } }
        public List<string> ValidationMessages = new List<string>();

        public Playlist(string playlistPath, string rootPath, string playlistRootPath){
            PlaylistDefinitionPath = playlistPath;
            PlaylistDefinitionNameFull = playlistPath.Split('\\')[playlistPath.Split('\\').Length-1];
            PlaylistImplementationPath = PlaylistDefinitionPath.Replace(playlistRootPath, rootPath) + ".m3u8";
            
            PlaylistType = Library.StringToPlaylistType(PlaylistDefinitionNameFull);
            PlaylistContent = File.ReadAllText(playlistPath).Split(new[]{ Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            PlaylistName = Regex.Match(PlaylistDefinitionNameFull, " .+$").Value.Trim();

            PlaylistImplementationPath = PlaylistImplementationPath.Replace(PlaylistDefinitionNameFull, PlaylistName);
        }

        public string PlaylistDefinitionPath;
	    public string PlaylistDefinitionNameFull; //Validate

        public string PlaylistImplementationPath;
        public string PlaylistName;
        public PlaylistType PlaylistType;
        public List<string> PlaylistContent;  

        private void Validate() { 
            //Validator.FolderName(this.FolderNameFull, ref ValidationMessages);
        } 
    }
}