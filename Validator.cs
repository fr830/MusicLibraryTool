using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MusicLibraryTool
{
    public static class Validator 
    {
        public static void FolderName(string folderName, ref List<string> errorMessages) {
            if(!Regex.Match(folderName, "\\[.+\\] .+ - .+\\[.+]").Success) {
                errorMessages.Add("Foldername is invalid");
            }
        }
        public static void NoSubFolders(string path, ref List<string> errorMessages){
            if(Directory.GetDirectories(path).Length != 0){
                errorMessages.Add("Albumfolder contains subdirectories");
            }
        }

        public static void TrackTypeInFolderTrackTypes(TrackType TrackType, List<TrackType> FolderTypes, ref List<string> errorMessages){
            if(!FolderTypes.Contains(TrackType)){
                errorMessages.Add("Tracktype doesn't match foldertype");
            }
        }

        public static bool HasCoverJpg(string path){
            var files = Directory.GetFiles(path);
            foreach(var file in files) {
                if(file.Contains("cover.jpg")){
                    return true;
                } 
            }
            return false;
        }
    }
}