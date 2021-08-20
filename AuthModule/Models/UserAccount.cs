using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthModule.Models
{
    public class UserAccount : IEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }

        [NotMapped]
        public string Password { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? InsertDate { get; set; }
        public ICollection<UserToken> UserTokens { get; set; }
    }
}