using Core.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthModule.Models
{
    public class UserToken : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpirationDate { get; set; }
        public int Status { get; set; }

        [ForeignKey("UserId")]
        public UserAccount UserAccount { get; set; }
    }
}