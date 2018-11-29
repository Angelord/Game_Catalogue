using Microsoft.Owin;
using System.Web.Security;
using Microsoft.Web.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;
using GameC.Website.Models;

[assembly: OwinStartupAttribute(typeof(GameC.Website.Startup))]
namespace GameC.Website {
    public partial class Startup {

        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
            CreateRoles();
        }

        private void CreateRoles() {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleMan = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleMan.RoleExists("Admin")) {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleMan.Create(role);
            }

            //context.
        }
    }
}
