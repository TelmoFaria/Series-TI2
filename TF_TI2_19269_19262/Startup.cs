using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;
using TF_TI2_19269_19262.Models;
using System.Linq;

namespace TF_TI2_19269_19262
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            iniciaAplicacao();
        }

        /// <summary>
        /// cria, caso não existam, as Roles de suporte à aplicação: Agente, Funcionario, Condutor
        /// cria, nesse caso, também, um utilizador...
        /// </summary>
        private void iniciaAplicacao()
        {

            // identifica a base de dados de serviço à aplicação
            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // criar a Role 'Utilizador'
            if (!roleManager.RoleExists("Utilizador"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Utilizador";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Moderador"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Moderador";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Administrador"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Administrador";
                roleManager.Create(role);
            }


            // criar um utilizador 'Utilizador'
            var user = new ApplicationUser();
            user.UserName = "telmo@teste.pt";
            user.Email = "telmo@teste.pt";
            string userPWD = "123_Asd";
            var chkUser = userManager.Create(user, userPWD);
            var getall = db.Utilizadores.ToList();

            //Adicionar o Utilizador à respetiva Role-Utilizador-
            if (chkUser.Succeeded)
            {
                var result1 = userManager.AddToRole(user.Id, "Utilizador");
            }

            // criar um utilizador 'Utilizador'
            user = new ApplicationUser();
            user.UserName = "joao@teste.pt";
            user.Email = "joao@teste.pt";
            userPWD = "123_Asd";
            chkUser = userManager.Create(user, userPWD);
            getall = db.Utilizadores.ToList();

            //Adicionar o Utilizador à respetiva Role-Utilizador-
            if (chkUser.Succeeded)
            {
                var result1 = userManager.AddToRole(user.Id, "Utilizador");
            }

            user = new ApplicationUser();
            user.UserName = "admin@teste.pt";
            user.Email = "admin@teste.pt";
            userPWD = "123_Asd";
            chkUser = userManager.Create(user, userPWD);

            getall = db.Utilizadores.ToList();

            //Adicionar o Utilizador à respetiva Role-Utilizador-
            if (chkUser.Succeeded)
            {
                var result1 = userManager.AddToRole(user.Id, "Administrador");
            }



        }

        // https://code.msdn.microsoft.com/ASPNET-MVC-5-Security-And-44cbdb97




    }
}
