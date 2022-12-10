using GUI.Areas.Admin.Utilities;
using GUI.Utilities;
using Infrastructure.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.IO;
using System.Threading.Tasks;

namespace GUI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AppUserController : Controller
    {
        private IAppUserRepository _appUserRepository;
        private IWebHostEnvironment _hostEnvironment;

        public AppUserController(IAppUserRepository appUserRepository , IWebHostEnvironment hostEnvironment)
        {
            _appUserRepository = appUserRepository;
            _hostEnvironment = hostEnvironment;
        }
        [HttpPost]
        [Route("AppUserSignup")]
        public async Task<IActionResult> AppUserSignUp([FromBody] AppUserSignUpVM model)
        {
            if(ModelState.IsValid)
            {
                var AlreadyExit = await _appUserRepository.AppUserSignIn(new AppUserSignin() {  CountryCode = model.CountryCode , Phone = model.Phone});
                if(AlreadyExit.Id > 0 )
                {
                    var SuccessResponse = new ResponseModel()
                    {
                        Message = "User Already Exit!",
                        Status = ReasonPhrases.GetReasonPhrase(StatusCodes.Status200OK),
                        StatusCode = StatusCodes.Status200OK,
                    };
                    return new JsonResult(SuccessResponse);
                }
                var Result = await _appUserRepository.AppUserSignUp(model);
                if(Result == true)
                {
                    var SuccessResponse = new ResponseModel()
                    {
                        Message = "User Successfully Signup!",
                        Status = ReasonPhrases.GetReasonPhrase(StatusCodes.Status200OK),
                        StatusCode = StatusCodes.Status200OK,
                        Data = model
                    };
                    return new JsonResult(SuccessResponse);
                }
                else
                {
                    var ErrorResponse = new ResponseModel()
                    {
                        Message = "Something Went Wrong!",
                        Status = ReasonPhrases.GetReasonPhrase(StatusCodes.Status400BadRequest),
                        StatusCode = StatusCodes.Status400BadRequest,
                    };
                    return new JsonResult(ErrorResponse);
                }
            }
            var Response = new ResponseModel()
            {
                Message = "Please Provide All Information!",
                Status = ReasonPhrases.GetReasonPhrase(StatusCodes.Status200OK),
                StatusCode = StatusCodes.Status200OK,
            };
            return new JsonResult(Response);
        }
        [HttpPut]
        [Route("AppUserUpdate")]
        public async Task<IActionResult> AppUserUpdate([FromBody] AppUserUpdateTempVM model)
        {
            if(ModelState.IsValid)
            {
                var DbVm = new AppUserUpdateVM
                {
                    Id = model.Id,
                    Name = model.Name,
                };
                if (model.PhotoExtension != null && model.PhotoArry != null && model.PhotoArry.Length >0)
                {
                    var RootPathForFile = _hostEnvironment.WebRootPath + "\\LotteryFiles\\AppUserImages";
                    string TempFileName = @"ProfileImg" + model.PhotoExtension;
                    string FileUniqueName = FileHandeling.GetUniqueFileName(TempFileName);
                    var FileUploadPath = Path.Combine(RootPathForFile + "\\" + FileUniqueName);
                    FileHandeling.SaveFile(FileUploadPath, model.PhotoArry);
                    DbVm.photoName = FileUniqueName;
                }
                var Result = await _appUserRepository.AppUserUpdate(DbVm);
                if(Result == true)
                {
                    var SuccessResponse = new ResponseModel()
                    {
                        Message = "User Successfully Updated!",
                        Status = ReasonPhrases.GetReasonPhrase(StatusCodes.Status200OK),
                        StatusCode = StatusCodes.Status200OK,
                        Data = model
                    };
                    return new JsonResult(SuccessResponse);

                }
                else
                {
                    var ErrorResponse = new ResponseModel()
                    {
                        Message = "Something Went Wrong!",
                        Status = ReasonPhrases.GetReasonPhrase(StatusCodes.Status400BadRequest),
                        StatusCode = StatusCodes.Status400BadRequest,
                    };
                    return new JsonResult(ErrorResponse);
                }
            }
            var Response = new ResponseModel()
            {
                Message = "Please Provide All Information!",
                Status = ReasonPhrases.GetReasonPhrase(StatusCodes.Status200OK),
                StatusCode = StatusCodes.Status200OK,
            };
            return new JsonResult(Response);
        }
        [HttpPost]
        [Route("AppUserSignIn")]
        public async Task<IActionResult> AppUserSignIn([FromBody] AppUserSignin model)
        {
            if(ModelState.IsValid)
            {
                var Result = await _appUserRepository.AppUserSignIn(model);
                if(Result != null && Result.Id >0)
                {
                    var SuccessResponse = new ResponseModel()
                    {
                        Message = "Found!",
                        Status = ReasonPhrases.GetReasonPhrase(StatusCodes.Status200OK),
                        StatusCode = StatusCodes.Status200OK,
                        Data = Result
                    };
                    return new JsonResult(SuccessResponse);
                }
                else
                {
                    var ErrorResponse = new ResponseModel()
                    {
                        Message = "Not Found!",
                        Status = ReasonPhrases.GetReasonPhrase(StatusCodes.Status404NotFound),
                        StatusCode = StatusCodes.Status404NotFound,
                    };
                    return new JsonResult(ErrorResponse);
                }
            }
            var Response = new ResponseModel()
            {
                Message = "Please Provide All Information!",
                Status = ReasonPhrases.GetReasonPhrase(StatusCodes.Status200OK),
                StatusCode = StatusCodes.Status200OK,
            };
            return new JsonResult(Response);
        }
        [HttpGet]
        [Route("AppUserGetById")]
        public async Task<IActionResult> AppUserGetById(int id)
        {
            var Result = await _appUserRepository.AppUserGetById(id);
            if (Result != null && Result.Id > 0)
            {
                var SuccessResponse = new ResponseModel()
                {
                    Message = "Found!",
                    Status = ReasonPhrases.GetReasonPhrase(StatusCodes.Status200OK),
                    StatusCode = StatusCodes.Status200OK,
                    Data = Result
                };
                return new JsonResult(SuccessResponse);
            }
            else
            {
                var ErrorResponse = new ResponseModel()
                {
                    Message = "Not Found!",
                    Status = ReasonPhrases.GetReasonPhrase(StatusCodes.Status404NotFound),
                    StatusCode = StatusCodes.Status404NotFound,
                };
                return new JsonResult(ErrorResponse);
            }
        }
    }
}
