using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PR5.Models;

namespace PR5.Services
{
    public class Validate
    {
        public string ValidateUser(User user)
        {
            var context = new ValidationContext(user);
            var results = new List<ValidationResult>();
            if (user == null)
                return "Пользователь не может быть null";

            var errors = new List<string>();
            if (!Validator.TryValidateObject(user, context, results, true))
            {
                return string.Join(Environment.NewLine, results.Select(r => r.ErrorMessage));
            }
            if (!string.IsNullOrEmpty(user.Phone) && user.Phone.Any(c => !char.IsDigit(c)))
            {
                return "Телефон должен содержать только цифры";
            }
            if (user.Birthday > DateTime.Now)
            {
                return "Дата рождения не может быть в будущем";
            }

            if (string.IsNullOrEmpty(user.Password) || user.Password.Length < 8)
            {
                errors.Add("Пароль должен содержать минимум 8 символов");
            }

            if (!string.IsNullOrEmpty(user.Mail) && !user.Mail.Contains("@"))
                return "Некорректный формат email";
            return string.Join(Environment.NewLine, errors);
        }

    }
}
