using System;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Input;
using MyOnlineStore.Entities.Models.Users;
using Xamarin.Forms;
using MyOnlineStore.HtmlFile;
using MyOnlineStore.Application.Presentation.Views.LoginScenarios;

namespace MyOnlineStore.Application.Presentation.ViewModels.LoginScenarios
{
    public class ForgotViewModel
    {

        public ForgotViewModel(INavigation navigation)
        {

            Navigation = navigation;
            UserForgotPassword = new ForgotPassword();
            SendCodeToEmailcommand = new Command(() => SendEmail());

            ValidateCodeCommand = new Command(async () => 
         
            await GoChangePasswordPage()
            
            );
        }

        public INavigation Navigation { get; set; }

        public ForgotPassword UserForgotPassword { get; set; }

        public ICommand SendCodeToEmailcommand { get; set; }
        public ICommand ValidateCodeCommand { get; set; }

     

        public void SendEmail()
        {
            UserForgotPassword.Code = GenerateGuid();


            Html html = new Html(UserForgotPassword.Code);

         string htmlFile= html.ReadFileInPackage("EmailCode.html");

          int index= htmlFile.LastIndexOf("</label>");
            string emailHtml = htmlFile.Insert(index++, UserForgotPassword.Code);
            try
            {

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(UserForgotPassword.UserEmail);
                mail.To.Add(UserForgotPassword.UserEmail);
                mail.Subject = "My Online Store New Password ";
                mail.IsBodyHtml = true;
                mail.Body = emailHtml;

                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("est.juanpablotorres@gmail.com", "jp84704tt");

                SmtpServer.Send(mail);
            }
            catch (Exception)
            {
                //("Faild", ex.Message, "OK");
            }
        }

        public async Task GoChangePasswordPage()
        {
            if(ValidateCode())
            {

           await Navigation.PushAsync(new ChangePasswordPage());
            }
        }

        //Hay que enlazar el codigo enviado con la informacion del email entrado
        public string GenerateGuid()
        {
           

           string code = Guid.NewGuid().ToString();
            UserForgotPassword.ExpirationDate = DateTime.Today.AddDays(1);

            //Atachar codigo generado a este email 

            return code.Substring(0,10);
       
        }

        public bool ValidateCode()
        {
            bool isCorrect = false;

            if(UserForgotPassword.VerifyCode==UserForgotPassword.Code && UserForgotPassword.ExpirationDate < DateTime.Today )
            {
                isCorrect = true;
            }else
            {
                isCorrect = false;
            }

            return isCorrect;
        }

        public bool UnableUserAccountOfThis(string email)
        {

            //Find the password of this email and replace it with the code send it to email

            return true;

        }

      
    }
}
