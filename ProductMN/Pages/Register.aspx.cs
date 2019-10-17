using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProductMN.Pages
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (val1)
            {
                Response.Redirect("Index.aspx");
            }
        }

        public bool IsValid()
        {
            var pwd = Password.Text;
            var confirm = ConfirmPassword.Text;

            if (!pwd.Equals(confirm))
            {
                return false;
            }
            return true;
        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                // Default UserStore constructor uses the default connection string named: DefaultConnection
                var userStore = new UserStore<IdentityUser>();
                var manager = new UserManager<IdentityUser>(userStore);

                var user = new IdentityUser() { UserName = UserName.Text };
                IdentityResult result = manager.Create(user, Password.Text);

                if (result.Succeeded)
                {
                    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                    var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenticationManager.SignIn(new AuthenticationProperties() { }, userIdentity);
                    Response.Redirect("Index.aspx");
                }
                else
                {
                    StatusMessage.Text = result.Errors.FirstOrDefault();
                }
            }
            else
            {
                StatusMessage.Text = "Password and confirm is not match";
            }
        }
    }
}