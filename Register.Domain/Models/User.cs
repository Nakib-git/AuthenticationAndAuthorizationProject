﻿namespace Register.Domain.Models {
    public class User {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
