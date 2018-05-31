using System;
using System.Collections.Generic;
using System.IO;

namespace MusicLibraryTool
{
    public static class Library
    {
        public static List<Track> GetAllTracks(string LibraryPath) {
            var TrackList = new List<Track>();
            foreach(var AlbumFolderPath in Directory.GetDirectories(LibraryPath)){ 
                foreach(var TrackFilePath in Directory.GetFiles(AlbumFolderPath)){
                    if(!TrackFilePath.Contains("cover.jpg")){
                        var Track = new Track(TrackFilePath);
                        TrackList.Add(Track);
                    }
                }
            }
            return TrackList;
        }

        public static List<Playlist> GetAllPlaylists(string PlaylistPath, string RootPath, string playlistRootPath) {
            var PlaylistList = new List<Playlist>();
            foreach(var PlaylistFile in Directory.GetFiles(PlaylistPath,"*",SearchOption.AllDirectories)) {
                PlaylistList.Add(new Playlist(PlaylistFile, RootPath, playlistRootPath));
            }
            return PlaylistList;
        }


        public static TrackType StringToTrackType(string fullName){ 
            if(fullName.ToLower().Contains("m4a")){
                return TrackType.M4A;
            } else if(fullName.ToLower().Contains("mp3")){
                return TrackType.MP3;
            } else if(fullName.ToLower().Contains("ogg")){
                return TrackType.OGG;
            } else if(fullName.ToLower().Contains("flac")){
                return TrackType.FLAC;
            } else if(fullName.ToLower().Contains("wav")){
                return TrackType.WAV;
            } else {
                return TrackType.Other;
            }
        }
        public static List<TrackType> StringToTrackTypes(string fullName){
            var typesList = new List<TrackType>();
            if(fullName.ToLower().Contains("m4a")){
                typesList.Add(TrackType.M4A);
            }
            if(fullName.ToLower().Contains("mp3")){
                typesList.Add(TrackType.MP3);
            }
            if(fullName.ToLower().Contains("ogg")){
                typesList.Add(TrackType.OGG);
            }
            if(fullName.ToLower().Contains("flac")){
                typesList.Add(TrackType.FLAC);
            }
            if(fullName.ToLower().Contains("wav")){
                typesList.Add(TrackType.WAV);
            } 
            return typesList;
        }
        public static PlaylistType StringToPlaylistType(string fullName){ 
            if(fullName.ToLower().Contains("[album]")){
                return PlaylistType.Album;
            } else if(fullName.ToLower().Contains("[artist]")){
                return PlaylistType.Artist;
            } else if(fullName.ToLower().Contains("[collection]")){
                return PlaylistType.Collection;
            } else {
                return PlaylistType.Other;
            }
        }
        public static AlbumType StringToAlbumType(string fullName){ 
            if(fullName.ToLower().Contains("[artist]")){
                return AlbumType.Artist;
            } else if(fullName.ToLower().Contains("[soundtrack]")){
                return AlbumType.Soundtrack;
            } else if(fullName.ToLower().Contains("[variousartists]")){
                return AlbumType.VariousArtists;
            } else if(fullName.ToLower().Contains("[seperatefiles]")){
                return AlbumType.SeperateFiles;
            } else {
                return AlbumType.Other;
            }
        }
    }
}