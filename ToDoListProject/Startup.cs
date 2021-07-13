using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ToDoListProject.Startup))]
namespace ToDoListProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
