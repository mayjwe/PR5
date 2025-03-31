using Microsoft.VisualStudio.TestTools.UnitTesting;
using PR5.Models;
using PR5.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        // Тест 1
        [TestMethod]
        public void EmptyUser()
        {
            try
            {
                var validator = new Validate();
                var emptyUser = new User
                {
                    Surname = string.Empty,
                    Name = null,
                    Birthday = default
                };
                var result = validator.ValidateUser(emptyUser);

                if (string.IsNullOrEmpty(result))
                {
                    Assert.Fail("Валидатор не вернул ошибки для пустого пользователя");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail($"Тест вызвал исключение: {ex.Message}");
            }
        }

        // Тест 2
        [TestMethod]
        public void PhoneError()
        {
            var validator = new Validate();
            var user = new User
            {
                Surname = "Иванов",
                Name = "Иван",
                Phone = "79abc123",
                Mail = "test@test.com",
                Birthday = new DateTime(1990, 1, 1),
                Password = "ValidPass123",
                Login = "000",
                ID_Post = 1
            };
            var result = validator.ValidateUser(user);
            var expectedError = "Телефон должен содержать только цифры";
            Assert.AreEqual(expectedError, result);
        }

        // Тест 3
        [TestMethod]
        public void Birthday()
        {
            var validator = new Validate();
            var user = new User {
                Birthday = DateTime.Now.AddDays(1),
                Surname = "Иванов",
                Name = "Иван",
                Phone = "79123123",
                Mail = "test@test.com",
                Password = "ValidPass123",
                Login = "000",
                ID_Post = 1
            };
            var result = validator.ValidateUser(user);
            Assert.AreEqual("Дата рождения не может быть в будущем", result);
        }


        // Тест 4

        [TestMethod]
        public void ShortPassword()
        {
            var validator = new Validate();
            var user = new User
            {
                Surname = "Сидоров",
                Name = "Сидор",
                Phone = "79217654321",
                Mail = "valid@email.com",
                Birthday = new DateTime(1970, 12, 25),
                Password = "1", // Short password
                Login = "valid_login",
                ID_Post = 3
            };

            var result = validator.ValidateUser(user);

            // Проверяем, что возвращена ошибка
            Assert.IsNotNull(result, "Валидатор должен вернуть ошибку для короткого пароля");
            Assert.AreNotEqual(string.Empty, result, "Валидатор должен вернуть ошибку для короткого пароля");
        }


        // Тест 5
        [TestMethod]
        public void Email()
        {
            var validator = new Validate();
            var user = new User
            {
                Mail = "invalid-email",
                Surname = "Иванов",
                Name = "Иван",
                Password = "123",
                Birthday = new DateTime(1990, 1, 1),
                Phone = "79123123",
                Login = "000",
                ID_Post = 1
            };

            var result = validator.ValidateUser(user);

            Assert.AreEqual("Некорректный формат email", result);
        }
    }
    // Тест 6
    [TestClass]
    public class RegistrationLogicTests
    {
        [TestMethod]
        public void CheckLogin_WhenLoginExists_ReturnsError()
        {
            // Arrange
            var existingLogins = new HashSet<string> { "existing" };
            var newLogin = "existing";

            // Act
            var error = RegistrationHelper.ValidateLogin(existingLogins, newLogin);

            // Assert
            Assert.AreEqual("Логин 'existing' уже занят", error);
        }
    }

    public static class RegistrationHelper
    {
        public static string ValidateLogin(ICollection<string> existingLogins, string newLogin)
        {
            return existingLogins.Contains(newLogin)
                ? $"Логин '{newLogin}' уже занят"
                : null;
        }
    }
}
