namespace WebAppMusic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Notknow : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MusicPlaylistApplicationUsers", newName: "ApplicationUserMusicPlaylists");
            RenameTable(name: "dbo.MusicFileMusicPlaylists", newName: "MusicPlaylistMusicFiles");
            DropPrimaryKey("dbo.ApplicationUserMusicPlaylists");
            DropPrimaryKey("dbo.MusicPlaylistMusicFiles");
            AddPrimaryKey("dbo.ApplicationUserMusicPlaylists", new[] { "ApplicationUser_Id", "MusicPlaylist_Id" });
            AddPrimaryKey("dbo.MusicPlaylistMusicFiles", new[] { "MusicPlaylist_Id", "MusicFile_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.MusicPlaylistMusicFiles");
            DropPrimaryKey("dbo.ApplicationUserMusicPlaylists");
            AddPrimaryKey("dbo.MusicPlaylistMusicFiles", new[] { "MusicFile_Id", "MusicPlaylist_Id" });
            AddPrimaryKey("dbo.ApplicationUserMusicPlaylists", new[] { "MusicPlaylist_Id", "ApplicationUser_Id" });
            RenameTable(name: "dbo.MusicPlaylistMusicFiles", newName: "MusicFileMusicPlaylists");
            RenameTable(name: "dbo.ApplicationUserMusicPlaylists", newName: "MusicPlaylistApplicationUsers");
        }
    }
}
