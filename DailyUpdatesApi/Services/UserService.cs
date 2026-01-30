
using DailyUpdates.Data;
using DailyUpdates.Models;



namespace DailyUpdates.Services
{
    public class UserService
    {
        private readonly AppDbcontext _context;

        public UserService(AppDbcontext context)
        {
            _context = context;
        }

  
        public string RegisterUser(string username, string email, string hashedPassword)
        {
            email = email.Trim().ToLower();

            if (_context.Users.Any(u => u.Email == email))
                return "Email already exists";

            var user = new User
            {
                Username = username,
                Email = email,
                Password = hashedPassword,
                CreatedAt = DateTime.UtcNow 
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return "User registered successfully";
        }

  
        public bool ValidateUserCredentials(string email, string password, out User? user)
        {
            email = email.Trim().ToLower();

            user = _context.Users.FirstOrDefault(u => u.Email == email);

      if (user != null)
{
      user.CreatedAt = user.CreatedAt.ToLocalTime();
}

            
      if (user == null) return false;

        

           return BCrypt.Net.BCrypt.Verify(password, user.Password);
           
 }

    
        public void SaveRefreshToken(int userId, string refreshToken, DateTime expiry)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return;

   
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = expiry;

            _context.SaveChanges();
        }

        public bool VerifyRefreshToken(int userId, string refreshToken)
        {
  
    var user = _context.Users.FirstOrDefault(u => u.Id == userId);
    if (user == null || string.IsNullOrEmpty(user.RefreshToken)) return false;

    return user.RefreshToken == refreshToken && 
           user.RefreshTokenExpiry > DateTime.UtcNow;

        }

        internal object GetReportsByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        internal static object AddReportAsync(Report report1, object report2)
        {
            throw new NotImplementedException();
        }
    }
}