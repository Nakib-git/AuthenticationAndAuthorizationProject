﻿namespace Register.WebApi.ViewModel {
    public class UserViewModel {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
