// WRONG SAMPLE
using System.Net.Mail;

public class UserService
{
    public void Register(string email, string password)
    {
        if (!ValidateEmail(email))
            throw new ValidationException("Email is not an email");
        var user = new User(email, password);

        SendEmail(new MailMessage("mysite@nowhere.com", email) { Subject = "HEllo foo" });
    }

    public virtual bool ValidateEmail(string email)
    {
        return email.Contains("@");
    }

    public bool SendEmail(MailMessage message)
    {
        _smtpClient.Send(message);
    }
}

//
// PROBLEM: The SendEmail and ValidateEmail methods have nothing to do with the UserService class.
//

// CORRECT VERSION
public class UserService
{
    EmailService _emailService;
    DbContext _dbContext;
    public UserService(EmailService aEmailService, DbContext aDbContext)
    {
        _emailService = aEmailService;
        _dbContext = aDbContext;
    }

    public void Register(string email, string password)
    {
        if (!_emailService.ValidateEmail(email))
            throw new ValidationException("Email is not an email");
        var user = new User(email, password);
        _dbContext.Save(user);
        emailService.SendEmail(new MailMessage("myname@mydomain.com", email) { Subject = "Hi. How are you!" });

    }
}

public class EmailService
{
    SmtpClient _smtpClient;
    public EmailService(SmtpClient aSmtpClient)
    {
        _smtpClient = aSmtpClient;
    }

    public bool virtual ValidateEmail(string email)
    {
        return email.Contains("@");
    }

    public bool SendEmail(MailMessage message)
    {
        _smtpClient.Send(message);
    }
}