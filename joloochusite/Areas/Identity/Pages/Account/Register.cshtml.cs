using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using joloochusite.Model;
using joloochusite.Data;
using joloochusite.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace joloochusite.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<RegisterModel> _logger;
        private ApplicationDbContext _context;
        IWebHostEnvironment _appEnvironment;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            ApplicationDbContext context,
            IWebHostEnvironment hosting)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _context = context;
            _appEnvironment = hosting;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Фамилия")]
            public string Patronimic { get; set; }

            [Required]
            [Display(Name = "Имя")]
            public string Name { get; set; }

            [Required]
            [Display(Name = "Номер телефона")]
            public string PhoneNumber { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.")]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }

            [Required]
            [Display(Name = "Подтверждение пароля")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Марка машины")]
            public string CarMark { get; set; }

            [Required]
            [Display(Name = "Номер машины")]
            public string CarNumber { get; set; }

            public IFormFile CarPhoto { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                // add car and user
                var appUserId = await AddCarAndUser(Input);

                // register user in site
                var user = new ApplicationUser { UserName = Input.PhoneNumber, PhoneNumber = Input.PhoneNumber, AppUserId = appUserId };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {

                    HttpContext.Session.SetString("_UserId", user.Id);
                    await _signInManager.PasswordSignInAsync(Input.PhoneNumber, Input.Password, false, lockoutOnFailure: true);
                    return LocalRedirect(Url.Content($"{returnUrl}"));

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private async Task<int> AddCarAndUser(InputModel input)
        {
            // add user
            var appUser = new Model.AppUser()
            {
                FirstName = input.Name,
                Patronimyc = input.Patronimic,
                Password = input.Password,
                PhoneNumber = input.PhoneNumber
            };

            _context.AppUsers.Add(appUser);
            _context.SaveChanges();


            // add car
            var car = new Model.Car()
            {
                ImageName = "",
                ImagePath = "",
                CarMark = input.CarMark,
                Number = input.CarNumber,
                UserId = appUser.Id
            };

            _context.Cars.Add(car);
            _context.SaveChanges();

            await SaveFile(input.CarPhoto, car);

            return appUser.Id;
        }

        private async Task SaveFile(IFormFile formFile, Car car)
        {

            if (formFile != null)
            {
                string fileName = "user_" + car.UserId + "_image_" + formFile.FileName;
                string path = "/cars/" + fileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                car.ImageName = formFile.FileName;
                car.ImagePath = _appEnvironment.WebRootPath + path;

                _context.Update(car);
                await _context.SaveChangesAsync();
            }
        }
    }
}
