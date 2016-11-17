namespace WebAppMusic.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebAppMusic.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebAppMusic.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.MusicFiles.AddOrUpdate(
              p => p.Link,
              new Models.MusicFile { Id = 1, Link = "http://mp3.zing.vn/bai-hat/Dem-Ngay-Xa-Em-OnlyC-Lou-Hoang/ZW7UUWC9.html" },
              new Models.MusicFile { Id = 2, Link = "http://mp3.zing.vn/bai-hat/Roi-Nguoi-Da-LAB/ZW7UUUEW.html" },
              new Models.MusicFile { Id = 3, Link = "http://mp3.zing.vn/bai-hat/abc-abc-test-abc/ZW7UUUEW.html" }
            );

            Models.MusicPlaylist pl1 = new Models.MusicPlaylist
            {
                Name = "playlist1",
                CountDislike = 0,
                CountLike = 0,
                CountListen = 1,
                DateCreate = System.DateTime.Now,
                MusicFiles = new Models.MusicFile[]
                {
                    context.MusicFiles.Find(1),
                    context.MusicFiles.Find(2),
                    context.MusicFiles.Find(3)
                }
            };

            Models.MusicPlaylist pl2 = new Models.MusicPlaylist
            {
                Name = "playlist2",
                CountDislike = 0,
                CountLike = 0,
                CountListen = 1,
                DateCreate = System.DateTime.Now,
                MusicFiles = new Models.MusicFile[]
               {
                    context.MusicFiles.Find(2),
                    context.MusicFiles.Find(3)
               }
            };

            Models.MusicPlaylist pl3 = new Models.MusicPlaylist
            {
                Name = "playlist3",
                CountDislike = 0,
                CountLike = 0,
                CountListen = 1,
                DateCreate = System.DateTime.Now,
                MusicFiles = new Models.MusicFile[]
               {
                    context.MusicFiles.Find(1),
                    context.MusicFiles.Find(3)
               }
            };

          
            context.Users.AddOrUpdate(
             pL => pL.UserName,
             new Models.ApplicationUser
             {
                 UserName = "phuongnm",
                 Email = "nguyenphuong.cnbt@gmail.com",
                 Playlists = new Models.MusicPlaylist[]
                 {
                      pl1, pl2
                 }
             },
             new Models.ApplicationUser
             {
                 UserName = "phuongnm1",
                 Email = "p@a.a",
                 Playlists = new Models.MusicPlaylist[]
                 {
                      pl2, pl3
                 }
             }
           );

            context.MusicPlaylists.Add(pl1);
            context.MusicPlaylists.Add(pl2);
            context.MusicPlaylists.Add(pl3);

        }
    }
}
