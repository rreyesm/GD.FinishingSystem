﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace GD.FinishingSystem.Entities
{
    [Table(name: "tblUsers")]
    public class User : BaseEntity
    {
        public User()
        {
            IsDeleted = false;
        }

        public override string ToString()
        {
            return Name + " " + LastName;
        }

        [Key]
        public int UserID { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string GetName { get { return ToString(); } }
        public string UserName { get; set; }
        public string PasswordStored { get; set; }

        [NotMapped]
        public string Password
        {
            set { PasswordStored = GetPasswordSha512(value); }
        }

        public bool PasswordControl(string password)
        {
            string pswhash = GetPasswordSha512(password);
            return (pswhash == PasswordStored);
        }



        public bool IsActive { get; set; }

        public static string GetPasswordSha512(string password)
        {
            using (var sha1 = new SHA512Managed())
            {
                var hash = Encoding.UTF8.GetBytes(password);
                var generatedHash = sha1.ComputeHash(hash);
                var generatedHashString = Convert.ToBase64String(generatedHash);
                return generatedHashString;
            }
        }

        public int AreaID { get; set; }

    }
}
