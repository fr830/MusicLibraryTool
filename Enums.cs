using System;

namespace MusicLibraryTool
{
    public enum TrackType
    {
        FLAC,
        OGG,
        MP3,
        M4A,
        WAV,
        Other
    }
    public enum AlbumType
    {
        Artist,
        Soundtrack,
        VariousArtists,
        SeperateFiles,
        Other
    }

    public enum PlaylistType
    {
       Album,
       Artist,
       Collection,
       Other
    }
}