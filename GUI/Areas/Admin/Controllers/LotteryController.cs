using GUI.Areas.Admin.Utilities;
using GUI.Utilities;
using Infrastructure.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LotteryController : BaseController
    {
        private ILotteryRepository _lotteryRepository;
        private IWebHostEnvironment _hostEnvironment;

        public LotteryController(ILotteryRepository lotteryRepository, IWebHostEnvironment hostEnvironment)
        {
            _lotteryRepository = lotteryRepository;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Lotteries()
        {
            return View();
        }
        public async Task<IActionResult> LotteryPartial(int PageIndex = 1, int PageSize = 10, string SearchTerm = "")
        {
            return PartialView("_LotteryPartial", await _lotteryRepository.LotteryGetAll(SearchTerm, PageIndex, PageSize));
        }
        [HttpGet]
        public async Task<IActionResult> LotteryAddUpdate(int id)
        {
            if (id > 0)
            {
                var model = await _lotteryRepository.LotteryGetById(id);
                return PartialView("_LotteryAddUpdate", model);
            }
            else
            {
                return PartialView("_LotteryAddUpdate", new LotteryCreateVM());
            }
        }
        [HttpPost]
        public async Task<IActionResult> LotteryAddUpdate(LotteryCreateVM model)
        {
            model.ExpiryDate = DateTime.Now;
            var RootPathForFile = _hostEnvironment.WebRootPath + "\\LotteryFiles\\LotteryImages";
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    if (model.TempLotteryImg != null)
                    {
                        string FileUniqueName = FileHandeling.GetUniqueFileName(model.TempLotteryImg.FileName);
                        model.LotteryImgName = FileUniqueName;
                        var FileUploadPath = Path.Combine(RootPathForFile + "\\" + FileUniqueName);
                        using (FileStream Upload = new FileStream(Path.Combine(FileUploadPath), FileMode.Create))
                        {
                            model.TempLotteryImg.CopyTo(Upload);
                            Upload.Dispose();
                            Upload.Close();
                        }
                    }
                    var resutl = await _lotteryRepository.LotteryUpdate(model);
                    if (resutl == true)
                    {
                        return Json("LotteryPartialButton");
                    }
                    else
                    {
                        return Json("error");
                    }
                }
                else
                {
                    if(model.LotteryImg == null)
                    {
                        model.LotteryImgName = "temp.webp";
                    }
                    else
                    {
                        string FileUniqueName = FileHandeling.GetUniqueFileName(model.LotteryImg.FileName);
                        model.LotteryImgName = FileUniqueName;
                        var FileUploadPath = Path.Combine(RootPathForFile + "\\" + FileUniqueName);
                        using (FileStream Upload = new FileStream(Path.Combine(FileUploadPath), FileMode.Create))
                        {
                            model.LotteryImg.CopyTo(Upload);
                            Upload.Dispose();
                            Upload.Close();
                        }
                    }
                    var resutl = await _lotteryRepository.LotteryCreate(model);
                    if (resutl == true)
                    {
                        return Json("LotteryPartialButton");
                    }
                    else
                    {
                        return Json("error");
                    }
                }
            }
            else
            {
                return Json("");
            }
        }
        [HttpGet]
        public async Task<IActionResult> LotteryDelete(int id)
        {
            var result = await _lotteryRepository.LotteryDelete(id);
            if (result == true)
            {
                return Json("LotteryPartialButton");
            }
            else
            {
                Json("error");
            }
            return Json("");
        }
        #region Lottery Winners
        public IActionResult Winners()
        {
            return View();
        }
        public async Task<IActionResult> WinnersPartial(int PageIndex = 1, int PageSize = 10, string SearchTerm = "")
        {
            return PartialView("_WinnersPartial", await _lotteryRepository.LotteryWinnerLotteryGetAll(SearchTerm, PageIndex, PageSize));
        }
        #endregion

    }
}
