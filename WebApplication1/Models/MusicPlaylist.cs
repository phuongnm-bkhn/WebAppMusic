using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppMusic.Models
{
    public class MusicPlaylist
    {
        public int Id { get; set; }

        // Ten list nhac
        //[StringLength(128, MinimumLength = 3)]
        public string Name { get; set; }

        // Ngay khoi tao list nhac 
        public DateTime DateCreate { get; set; }

        // Danh gia cua nguoi dung 
        public int CountLike { get; set; }
        public int CountDislike { get; set; }
        public int CountListen { get; set; }

        // List nhac nay tu trang khac vd: mp3.zing.vn
        [StringLength(2000, MinimumLength = 3)]
        public string LinkOtherSite { get; set; }

        // Danh sach nhac cua playlist
        public virtual ICollection<MusicFile> MusicFiles { get; set; }
        public virtual ICollection<ApplicationUser> Uers { get; set; }
    }

}