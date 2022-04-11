using System;
using System.Collections.Generic;
using System.Text;
using AppProjectMVVM.Models;
using System.Threading.Tasks;
using System.Linq;
using Firebase.Database.Query;
using Firebase.Database;

namespace AppProjectMVVM.Services
{
    public class UserService
    {
        FirebaseClient person;
        public UserService()
        {
            person= new FirebaseClient("https://projectmuod-default-rtdb.firebaseio.com/");
        }
        public async Task<bool> IsUserExists(string email)
        {
            var user = (await person.Child(" Users ")
                 .OnceAsync<LoginPageModel>())
                .Where(u => u.Object.Email == email)
                .FirstOrDefault();
            return (user != null);
        }
        public async Task<bool> RegisterUser(string email, string password)
        {
            if (await IsUserExists(email) == false)
            {
                await person.Child(" Users ")
                    .PostAsync(new LoginPageModel()
                    {
                        Email = email,
                        Password = password
                    });
                return true;
            }
            else
            {
                return false;
            }
        }
     /*   public async Task<bool> LoginUser(string email, string password)
        {
            var user = (await person.Child(" Users ")
                .OnceAsync<LoginPageModel>())
                .Where(u => u.Object.Email == email)
                .Where(u => u.Object.Password == password)
                .FirstOrDefault();
            return (user != null);
        }*/
        public async Task<LoginPageModel> LoginUser(string email, string password)
        {
            var GetPerson = (await person
              .Child("Users")
              .OnceAsync<LoginPageModel>()).Where(a => a.Object.Email == email).Where(b => b.Object.Password == password).FirstOrDefault();

            if (GetPerson != null)
            {

                var content = GetPerson.Object as LoginPageModel;
                return content;

            }
            else
            {
                return null;
            }
        }


    }
}
