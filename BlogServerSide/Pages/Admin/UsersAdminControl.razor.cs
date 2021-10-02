using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using BlogServerSide.DataBase;

namespace BlogServerSide.Pages.Admin
{
    public partial class UsersAdminControl : ComponentBase
    {
        [Inject]
        public IConfiguration Configuration { get; set; }

        public List<User> Users { get; set; }

  
        protected override async Task OnInitializedAsync()
        {   
            
            await base.OnInitializedAsync();
            Users = await GetUsers();
      
        }

        public async Task<List<User>> GetUsers()
        {
           return await Task.Run(() => 
            { 
              List<User> users;
                using (var context = new BlogContext(Configuration))
                {
                    context.Database.EnsureCreated();
                    users = context.Users.ToList();
                    return users;
                }
            });
              
        



        }
        public void CommitUpdateUsers(object user)
        {
            var updatedUser = user as User;

            using (var context = new BlogContext(Configuration))
            {
                context.Database.EnsureCreated();
                context.Users.Update(updatedUser);
                context.SaveChanges();
            }
        }

        public void OnDeleteClick(User user)
        {

        }
    }
}