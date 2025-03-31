//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;

namespace PR5.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public int ID_User { get; set; }
        public int ID_Post { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Длина фамилии должна быть от 3 до 15 символов")]
        public string Surname { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Длина имени должна быть от 3 до 15 символов")]
        public string Name { get; set; }


        [StringLength(15, MinimumLength = 3, ErrorMessage = "Длина отчества должна быть от 3 до 15 символов")] 
        public string Patronymic { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Дата рождения должна иметь правильный вид dd.mm.yyyy")]
        public System.DateTime Birthday { get; set; }


        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Длина логина должна быть от 3 до 15 символов")]
        public string Login { get; set; }


        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Длина пароля должна быть от 3 до 15 символов")]
        public string Password { get; set; }

        [Required]
        [StringLength(13, ErrorMessage = "Длина номера телефона должна быть не более 13 символов")]
        public string Phone { get; set; }

        [Required]
        public string Mail { get; set; }
    }
}
