using System;
using System.Linq;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using BlogServerSide.DataBase;
using Microsoft.Extensions.Configuration;

namespace BlogServerSide.Dialogs
{
    public partial class AddCategoryDialog
    {

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        [Inject]
        public IConfiguration Configuration { get; set; }

        [Parameter]
        public Category Category { get; set; } = new Category();
        private void Cancel()
        {
            MudDialog.Cancel();
        }

        public static string RemoveWhitespace( string input)
        {
            return new string (input.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
        }

        private void AddCategory()
        {
            using (var context = new BlogContext(Configuration))
            {
                context.Database.EnsureCreated();
                this.Category.Slug = RemoveWhitespace(this.Category.Title.ToLower());
                this.Category.Id = new Guid(); ;
                context.Categorys.Add(Category);
                context.SaveChanges();
            }

            MudDialog.Close(DialogResult.Ok(Category));
        }
    }
}