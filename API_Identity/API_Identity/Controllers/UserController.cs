using API_Identity.Data;
using API_Identity.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<Student> userManager;

        public UserController(IConfiguration configuration, UserManager<Student> userManager)   // to access secretKey
        {
            this.configuration = configuration;
            this.userManager = userManager;      /// this service needs to be configure
        }

        [HttpPost]
        [Route("StaticLogin")]
        public ActionResult<TokenDto> LoginStatic(UserDto Credentials)
        {

            if (Credentials.UserName != "Mahmoud" || Credentials.Password != "2111")
            {
                return Unauthorized();   //  tokent will not generated
            }

            #region create claims
            var claimsList = new List<Claim>
            {
                new Claim("Name" , "mahmoud"),
                new Claim(ClaimTypes.Country , "Egypt"),
                new Claim(ClaimTypes.Role , "Admin"),
                new Claim(ClaimTypes.NameIdentifier , Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email , "Mamoud@gmail.com"),

            };
            #endregion

            #region gettingSecretKEy
            var secretKeyString = configuration.GetValue<string>("SecretKey");
            var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKeyString ?? "");
            SymmetricSecurityKey? secretKey = new SymmetricSecurityKey(secretKeyInBytes);
            #endregion

            #region SigningCredential
            var SigningCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            #endregion

            #region generate token
            var expireDate = DateTime.Now.AddDays(1);
            var token = new JwtSecurityToken(claims: claimsList,
                signingCredentials: SigningCredentials,
                expires: expireDate);
            #endregion

            #region convert Tokent to string
            var TokenHandler = new JwtSecurityTokenHandler();
            var TokenString = TokenHandler.WriteToken(token: token);
            #endregion


            return new TokenDto(TokenString , expireDate);          // return token object

        }

        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            #region Create a new user
            var Student = new Student
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                ClassLevel = registerDto.ClassLevel,
                //PasswordHash      I can set password here becouse it is passwordHash
            };
            var Result = await userManager.CreateAsync(Student, registerDto.Password);  // this will take the password , then i will hash it


            if (!Result.Succeeded)
            {
                return BadRequest(error: Result.Errors);
            }

            #endregion

            #region Create a claims for user
            var claimsList = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier , Student.Id), //as student inhiert from IdentityUser
                new Claim(ClaimTypes.Name , Student.UserName),
                new Claim(ClaimTypes.Role , "Admin"),
                new Claim(ClaimTypes.Email , Student.Email),
            };
            await userManager.AddClaimsAsync(Student, claims: claimsList);
            #endregion

            return Ok();

        }



        [HttpPost]
        [Route("RegisterUser")]
        public async Task<ActionResult> RegisterUser(RegisterDto registerDto)
        {
            #region Create a new user
            var Student = new Student
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                ClassLevel = registerDto.ClassLevel,
                //PasswordHash      I can set password here becouse it is passwordHash
            };
            var Result = await userManager.CreateAsync(Student, registerDto.Password);  // this will take the password , then i will hash it


            if (!Result.Succeeded)
            {
                return BadRequest(error: Result.Errors);
            }

            #endregion

            #region Create a claims for user
            var claimsList = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier , Student.Id), //as student inhiert from IdentityUser
                new Claim(ClaimTypes.Name , Student.UserName),
                new Claim(ClaimTypes.Role , "User"),
                new Claim(ClaimTypes.Email , Student.Email),
            };
            await userManager.AddClaimsAsync(Student, claims: claimsList);
            #endregion

            return Ok();

        }




        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<TokenDto>> login(UserDto Credential)
        {
            var user = await userManager.FindByNameAsync(Credential.UserName);
            if (user == null)
            {
                return Unauthorized();
            }

            var password = await userManager.CheckPasswordAsync(user,Credential.Password);
            if (!password)
            {
                return Unauthorized();
            }

            var claimlist = await userManager.GetClaimsAsync(user);


            #region gettingSecretKEy
            var secretKeyString = configuration.GetValue<string>("SecretKey");
            var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKeyString ?? "");
            SymmetricSecurityKey? secretKey = new SymmetricSecurityKey(secretKeyInBytes);
            #endregion

            #region SigningCredential
            var SigningCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            #endregion

            #region generate token
            var expireDate = DateTime.Now.AddDays(1);
            var token = new JwtSecurityToken(claims: claimlist,
                signingCredentials: SigningCredentials,
                expires: expireDate);
            #endregion

            #region convert Tokent to string
            var TokenHandler = new JwtSecurityTokenHandler();
            var TokenString = TokenHandler.WriteToken(token: token);
            #endregion

            return new TokenDto(TokenString, expireDate);


        }


    }
}
