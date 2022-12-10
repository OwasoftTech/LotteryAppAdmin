using DbModels;
using GUI.Utilities;
using Infrastructure.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Threading.Tasks;

namespace GUI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class LotteryController : Controller
    {
        private ILotteryRepository _lotteryRepository;

        public LotteryController(ILotteryRepository lotteryRepository)
        {
            _lotteryRepository = lotteryRepository;
        }
        [HttpGet]
        [Route("LotteryGetAll")]
        public async Task<IActionResult> LotteryGetAll()
        {
            var result = await _lotteryRepository.AppLotteryGetAll();
            if (result != null)
            {
                var Response = new ResponseModel()
                {
                    Message = "Available",
                    Status = ReasonPhrases.GetReasonPhrase(StatusCodes.Status200OK),
                    StatusCode = StatusCodes.Status200OK,
                    Data = result
                };
                return new JsonResult(Response);
            }
            else
            {
                var Response = new ResponseModel()
                {
                    Message = "Not Available",
                    Status = ReasonPhrases.GetReasonPhrase(StatusCodes.Status404NotFound),
                    StatusCode = StatusCodes.Status404NotFound,
                };
                return new JsonResult(Response);
            }
        }
        [HttpPost]
        [Route("EnroleLottery")]
        public async Task<IActionResult> EnroleLottery([FromBody] EnroleLotteryVM model)
        {
            if (ModelState.IsValid)
            {
                var candidateCount = await _lotteryRepository.CandidateCount(model.LotteryId);
                if (candidateCount.Difference == 0)
                {
                    var LimitFullResponse = new ResponseModel()
                    {
                        Message = "Token Limit is Full!",
                        Status = ReasonPhrases.GetReasonPhrase(StatusCodes.Status404NotFound),
                        StatusCode = StatusCodes.Status404NotFound,
                    };
                    return new JsonResult(LimitFullResponse);
                }
                else
                {
                    var Entity = new LotteryCandidate()
                    {
                        AppUserId = model.AppUserId,
                        CreatedDate = DateTime.Now,
                        LotteryId = model.LotteryId,
                    };
                    var result = await _lotteryRepository.LotteryCandidateCreate(Entity);
                    if (result == true)
                    {
                        var WinnerCandidateCount = await _lotteryRepository.CandidateCount(model.LotteryId);
                        if (WinnerCandidateCount.Difference == 0)
                        {
                            //    Creating Lottery Winner in Sql                     
                            var LotteryWinnerId = await _lotteryRepository.LotteryCandidateAnnonce(model.LotteryId);
                            //    var CandidatesList = await _lotteryRepository.LotteryCandidateList(model.LotteryId);
                            //    var RandomWinnder = EnumerableExtension.PickRandom<LotteryCandidate>(CandidatesList);
                            //    var LotteryWiner = new LotteryWiner()
                            //    {
                            //        CreatedDate = DateTime.Now,
                            //        LotteryId = model.LotteryId,
                            //        WinerId = 0
                            //    };
                            //    if (RandomWinnder != null && RandomWinnder.Id > 0)
                            //    {
                            //        LotteryWiner.WinerId = RandomWinnder.Id;
                            //    }
                            //    var WinnerCreate = await _lotteryRepository.LotteryWinerCreate(LotteryWiner);
                        }
                        var SuccessResponse = new ResponseModel()
                        {
                            Message = "User Successfully Enrolled!",
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
        [Route("LotteryWinnerGetAll")]
        public async Task<IActionResult> LotteryWinnerGetAll()
        {
            var result = await _lotteryRepository.AppLotteryWinnerGetAll();
            if (result != null)
            {
                var Response = new ResponseModel()
                {
                    Message = "Available",
                    Status = ReasonPhrases.GetReasonPhrase(StatusCodes.Status200OK),
                    StatusCode = StatusCodes.Status200OK,
                    Data = result
                };
                return new JsonResult(Response);
            }
            else
            {
                var Response = new ResponseModel()
                {
                    Message = "Not Available",
                    Status = ReasonPhrases.GetReasonPhrase(StatusCodes.Status404NotFound),
                    StatusCode = StatusCodes.Status404NotFound,
                };
                return new JsonResult(Response);
            }
        }
    }
}
